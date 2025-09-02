using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper.Pages
{
    public partial class SettingsPage : Page
    {
        private Frame mainFrame;
        private ImageBrush[] bgImages;

        // Constructor takes the Frame so we can navigate back
        public SettingsPage(Frame frame)
        {
            InitializeComponent();
            LoadBackground();
            mainFrame = frame;
        }
        // --- Convert WinForms Image to WPF ImageBrush ---
        private ImageBrush ConvertToBrush(System.Drawing.Image img)
        {
            var bitmap = new Bitmap(img); // Ensure it’s a Bitmap
            var hBitmap = bitmap.GetHbitmap(); // Get HBITMAP handle
            var wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            DeleteObject(hBitmap);
            return new ImageBrush(wpfBitmap);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private void LoadBackground()
        {

            bgImages = new ImageBrush[]
            {
                ConvertToBrush(Properties.Resources.defaultbg),
                ConvertToBrush(Properties.Resources.bg1),
                ConvertToBrush(Properties.Resources.bg2),
                ConvertToBrush(Properties.Resources.bg3),
                ConvertToBrush(Properties.Resources.bg4)
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush selectedBg = Application.Current.Resources["AppBackground"] as ImageBrush;

            if (Default.IsChecked == true) selectedBg = bgImages[0];
            else if (Bg1Radio.IsChecked == true) selectedBg = bgImages[1];
            else if (Bg2Radio.IsChecked == true) selectedBg = bgImages[2];
            else if (Bg3Radio.IsChecked == true) selectedBg = bgImages[3];
            else if (Bg4Radio.IsChecked == true) selectedBg = bgImages[4];

            selectedBg.Stretch = Stretch.UniformToFill;

            // Set global background
            Application.Current.Resources["AppBackground"] = selectedBg;

            // Save board size and mine count
            if (int.TryParse(BoardSizeBox.Text, out int boardSize))
                Application.Current.Resources["BoardSize"] = boardSize;

            if (int.TryParse(MineCountBox.Text, out int mineCount))
                Application.Current.Resources["MineCount"] = mineCount;

            // Navigate back to main menu
            mainFrame.Navigate(new Pages.MainMenuPage(mainFrame));
        }

        private void BackgroundRadio_Checked(object sender, RoutedEventArgs e)
        {
            if (bgImages == null) return;

            if (Default.IsChecked == true)
                PreviewArea.Background = bgImages[0];
            else if (Bg1Radio.IsChecked == true)
                PreviewArea.Background = bgImages[1];
            else if (Bg2Radio.IsChecked == true)
                PreviewArea.Background = bgImages[2];
            else if (Bg3Radio.IsChecked == true)
                PreviewArea.Background = bgImages[3];
            else if (Bg4Radio.IsChecked == true)
                PreviewArea.Background = bgImages[4];
        }

    }
}
