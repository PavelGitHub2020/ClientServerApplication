using InfoCards.Repositories.Interfaces;
using System.Collections.Generic;
using InfoCards.Models;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System;

namespace InfoCards.Repositories.Implimentations
{
    public class BaseRepository : IBaseRepository<InformationCard>
    {
        private const string PathToAllJsonFiles = @"B:\учебный проект\InfoCards\InfoCards\InfoCards\bin\Debug\AllFilesJson\";
        public InformationCard Create(ObservableCollection<InformationCard> informationCards)
        {
            if (informationCards.Count > 0)
            {
                using (StreamWriter file = File.CreateText($"{PathToAllJsonFiles}{informationCards}.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();

                    serializer.Serialize(file, informationCards);

                    file.Close();
                }

                return informationCards[informationCards.Count - 1];
            }
            else
            {
                if (File.Exists($"{PathToAllJsonFiles}{informationCards}.json"))
                {
                    File.Delete($"{PathToAllJsonFiles}{informationCards}.json");
                }
            }

            return null;
        }

        public ObservableCollection<InformationCard> Delete(int id)
        {
            ObservableCollection<InformationCard> InformationCards = new ObservableCollection<InformationCard>();

            try
            {
                var json = new DirectoryInfo(PathToAllJsonFiles);

                var text = string.Empty;

                foreach (FileInfo file in json.GetFiles())
                {
                    text = File.ReadAllText($"{PathToAllJsonFiles}/{file.Name}");
                }

                var result = JsonConvert.DeserializeObject<IList<InformationCard>>(text);

                InformationCard informationCard = new InformationCard();

                foreach (var r in result)
                {
                    if (r.ID != id)
                    {
                        informationCard = new InformationCard()
                        {
                            ID = r.ID,
                            Name = r.Name,
                            Path = r.Path
                        };

                        InformationCards.Add(informationCard);
                    }
                }

                Create(InformationCards);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return InformationCards;
        }

        public IList<InformationCard> GetAll()
        {
            IList<InformationCard> list = new List<InformationCard>();

            try
            {
                InformationCard card = new InformationCard();

                var json = new DirectoryInfo(PathToAllJsonFiles);

                var text = string.Empty;

                foreach (FileInfo file in json.GetFiles())
                {
                    text = File.ReadAllText($"{PathToAllJsonFiles}/{file.Name}");
                }

                dynamic array = JsonConvert.DeserializeObject<IList<InformationCard>>(text);

                foreach (var obj in (IEnumerable<dynamic>)array)
                {
                    card = new InformationCard()
                    {
                        ID = obj.ID,
                        Name = obj.Name,
                        Path = obj.Path
                    };

                    list.Add(card);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public InformationCard Update(InformationCard informationCard)
        {
            try
            {
                ObservableCollection<InformationCard> InformationCards = new ObservableCollection<InformationCard>();

                InformationCards = Delete(informationCard.ID);

                InformationCards.Add(informationCard);

                Create(InformationCards);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return informationCard;
        }
    }
}
