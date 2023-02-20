using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace InfoCardsClient.Models
{
    public class InformationCard : INotifyPropertyChanged
    {
        int id;

        private string name;

        private string path;

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        public InformationCard() { }
        public InformationCard(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
