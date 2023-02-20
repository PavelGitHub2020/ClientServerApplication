using Newtonsoft.Json;
using System.Drawing;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InfoCards.Models
{
    public class InformationCard
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
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
            }
        }
        public InformationCard() { }
        public InformationCard(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}
