using InfoCardsClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace InfoCardsClient
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        HttpClient client = new HttpClient();

        private InformationCard selectedInformationCard;

        private ObservableCollection<InformationCard> informationCards;

        IDialogService dialogService;

        public ObservableCollection<InformationCard> InformationCards
        { 
            get { return informationCards; }
            set
            {
                informationCards = value;
                OnPropertyChanged("InformationCards");
            }
        }

        public InformationCard SelectedInformationCard
        {
            get { return selectedInformationCard; }
            set
            {
                selectedInformationCard = value;
                OnPropertyChanged("SelectedInformationCard");
            }
        }

        public ApplicationViewModel(IDialogService dialogService)
        {
            try
            {
                this.dialogService = dialogService;

                client.BaseAddress = new Uri("https://localhost:5001/Main/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                    );


                GetAllInformationCards();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetMaxId()
        {
            int maxId = 0;

            foreach (var id in InformationCards)
            {
                if (id.ID > maxId)
                {
                    maxId = id.ID;
                }
            }

            return maxId;
        }

        private async void GetAllInformationCards()
        {
            try
            {
                var response = await client.GetStringAsync("GetAll");

                var informationCards = JsonConvert.DeserializeObject<ObservableCollection<InformationCard>>(response);

                InformationCards = new ObservableCollection<InformationCard>();

                foreach (var r in informationCards)
                {
                    InformationCards.Add(new InformationCard()
                    {
                        ID = r.ID,
                        Name = r.Name,
                        Path = r.Path
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void AddAndSaveInformationCard(InformationCard informationCard)
        {
            try
            {
                informationCard.ID = GetMaxId() + 1;

                InformationCards.Insert(0, informationCard);

                SelectedInformationCard = informationCard;

                await client.PostAsJsonAsync("file/create", InformationCards);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void CreateInformationCard()
        {
            try
            {
                bool condition = true;

                InformationCard informationCard = dialogService.OpenFileDialog();

                if (InformationCards.Count >= 1)
                {
                    for (int i = 0; i < InformationCards.Count; i++)
                    {
                        if (informationCard.Name.Equals(Path.GetFileNameWithoutExtension(InformationCards[i].Name)))
                        {
                            MessageBox.Show("A file with that name already exists!");

                            condition = false;
                        }
                    }

                    if (condition)
                    {
                        AddAndSaveInformationCard(informationCard);
                    }

                }
                else
                {
                    AddAndSaveInformationCard(informationCard);
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message);
            }
            
        }

        private Command addCommand;
        public Command AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new Command(obj =>
                    {
                        CreateInformationCard();
                    }
                    ));
            }
        }

        private async void UpdateInformationCard()
        {
            try
            {
                if (!SelectedInformationCard.Name.Equals(String.Empty))
                {
                    InformationCard informationCard = SelectedInformationCard;

                    SelectedInformationCard = null;

                    await client.PutAsJsonAsync("file/update", informationCard);

                    GetAllInformationCards();
                }
                else
                {
                    MessageBox.Show("The name cannot be empty!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Command updateCommand;
        public Command UpdateCommand
        {
            get
            {
                return updateCommand ??
                    (updateCommand = new Command(obj =>
                    {
                        try
                        {
                            UpdateInformationCard();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }));
            }
        }

        private async void RemoveInformationCard()
        {
            try
            {
                InformationCard informationCard = SelectedInformationCard;

                InformationCards.Remove(InformationCards.Where(i => i.ID == informationCard.ID).FirstOrDefault());

                SelectedInformationCard = null;

                await client.DeleteAsync($"{informationCard.ID}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Command removeCommand;
        public Command RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new Command(obj =>
                    {
                        try
                        {
                            RemoveInformationCard();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }));
            }
        }

        private Command sortingByNameCommand;
        public Command SortingByNameCommand
        {
            get
            {
                return sortingByNameCommand ??
                    (sortingByNameCommand = new Command(obj =>
                    {
                        try
                        {
                            InformationCards = new ObservableCollection<InformationCard>(InformationCards.OrderBy(i => i.Name));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
