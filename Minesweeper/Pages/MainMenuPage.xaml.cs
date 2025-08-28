using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;


namespace Minesweeper.Pages
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        private Frame _mainFrame;
        public MainMenuPage(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new GameBoardPage(_mainFrame));
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new SettingsPage(_mainFrame));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
