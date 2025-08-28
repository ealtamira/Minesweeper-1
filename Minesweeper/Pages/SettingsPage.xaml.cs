using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private Frame _mainFrame;
        public SettingsPage(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BoardSizeBox.Text, out int newSize) &&
                int.TryParse(MineCountBox.Text, out int newMines))
            {
                Engine.size = newSize;
                Engine.mines = newMines;
            }

            _mainFrame.Navigate(new MainMenuPage(_mainFrame));
        }
    }
}
