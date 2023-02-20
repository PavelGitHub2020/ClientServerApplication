using InfoCardsClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoCardsClient
{
    public interface IDialogService
    {
        void ShowMessage(string message);

        string FilePath { get; set; }   

        string FileName { get; set; }

        InformationCard OpenFileDialog(); 

        bool FormatValidation(string path);
    }
}
