using System;
using System.Media;
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
        SoundPlayer bgm = new SoundPlayer(Properties.Resources.main_menu);

        public MainMenuPage(Frame mainFrame)
        {
            InitializeComponent();
            _mainFrame = mainFrame;
            bgm.PlayLooping();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new GameBoardPage(_mainFrame));
            bgm.Stop();
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
