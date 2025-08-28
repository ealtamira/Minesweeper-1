using System.Windows;
using Minesweeper.Pages;

namespace Minesweeper
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainMenuPage(MainFrame));
        }
    }
}
