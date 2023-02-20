using InfoCardsClient.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace InfoCardsClient
{
    public class DialogService : IDialogService
    {
        public static int incrementId = 1;
        public int ID { get; set; }
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public InformationCard OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            InformationCard informationCard = new InformationCard();

            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;

                FileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);

                if(FormatValidation(FilePath))
                {
                    informationCard = new InformationCard()
                    {
                        ID = incrementId,
                        Name = FileName,
                        Path = FilePath
                    };

                    incrementId++;

                    return informationCard;
                }
                else
                {
                    MessageBox.Show("Select the appropriate image format (.png, .jpg)!");
                }
               
            }

            return null;
        }

        public bool FormatValidation(string path)
        {
            string ext = path.Substring(path.LastIndexOf('.'));

            if (ext.Equals(".png") || ext.Equals(".jpg"))
            {
                return true;
            }

            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
