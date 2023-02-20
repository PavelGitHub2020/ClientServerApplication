using System.Windows;
namespace InfoCardsClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationViewModel(new DialogService());
        }
    }
}
