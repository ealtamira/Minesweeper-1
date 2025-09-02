using System;
using System.Drawing;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Windows.Media.Brush;

namespace Minesweeper.Pages
{
    /// <summary>
    /// Interaction logic for GameBoardPage.xaml
    /// </summary>
    public partial class GameBoardPage : Page
    {
        private SoundPlayer click = new SoundPlayer(Properties.Resources.pop);
        private SoundPlayer bgSounds = new SoundPlayer(Properties.Resources.main_menu);
        private SoundPlayer mark = new SoundPlayer(Properties.Resources.click);
        private SoundPlayer boom = new SoundPlayer(Properties.Resources.explosion);
        private SoundPlayer winnar = new SoundPlayer(Properties.Resources.winnar);
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

        // --- Board & Images ---
        private Button[,] buttons;
        int boardSize = (int)Application.Current.Resources["BoardSize"];
        int mineCount = (int)Application.Current.Resources["MineCount"];
        private int tileSize = 50;

        private ImageBrush UnclickedImage;
        private ImageBrush FlagImage;
        private ImageBrush[] TileImages;
        private Frame _mainFrame;

        public GameBoardPage(Frame mainFrame)
        {
            _mainFrame = mainFrame;
            Engine.Init(boardSize, mineCount, tileSize);
            InitializeComponent();
            

            ResetButton.Click += Reset_Click;

            Engine.GameOver += () =>
            {
                boom.Play();
                DisableBoard();
            };

            Engine.GameWon += () =>
            {
                winnar.Play();
                DisableBoard();
            };


            GameBoard.Background = (Brush)Application.Current.Resources["GameBoardBackground"];


            LoadTileImages();
            InitializeBoard();
            HookEngineEvents();
            bgSounds.PlayLooping();
        }

        private void LoadTileImages()
        {
            // Load all your old WinForms resources
            UnclickedImage = ConvertToBrush(Properties.Resources.unclicked);
            FlagImage = ConvertToBrush(Properties.Resources.flag);

            TileImages = new ImageBrush[]
            {
                ConvertToBrush(Properties.Resources._0),
                ConvertToBrush(Properties.Resources._1),
                ConvertToBrush(Properties.Resources._2),
                ConvertToBrush(Properties.Resources._3),
                ConvertToBrush(Properties.Resources._4),
                ConvertToBrush(Properties.Resources._5),
                ConvertToBrush(Properties.Resources._6),
                ConvertToBrush(Properties.Resources._7),
                ConvertToBrush(Properties.Resources._8),
                ConvertToBrush(Properties.Resources.mine),
                ConvertToBrush(Properties.Resources.defusedmine)
            };
        }

        private void InitializeBoard()
        {
            GameBoard.Children.Clear();
            buttons = new Button[boardSize, boardSize];

            int padding = 5;
            int totalWidth = boardSize * tileSize + padding * 2;
            int totalHeight = boardSize * tileSize + padding * 2;

            GameBoard.Width = totalWidth;
            GameBoard.Height = totalHeight;

            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    var btn = new Button
                    {
                        Width = tileSize,
                        Height = tileSize,
                        Background = UnclickedImage,
                        Tag = new System.Windows.Point(r, c),
                    };

                    var template = new ControlTemplate(typeof(Button));
                    var borderFactory = new FrameworkElementFactory(typeof(Border));
                    borderFactory.SetBinding(Border.BackgroundProperty,
                        new System.Windows.Data.Binding("Background") { RelativeSource = RelativeSource.TemplatedParent });
                    template.VisualTree = borderFactory;
                    btn.Template = template;
                    btn.Focusable = false;

                    Canvas.SetLeft(btn, padding + c * tileSize);
                    Canvas.SetTop(btn, padding + r * tileSize);

                    btn.Click += Tile_LeftClick;
                    btn.MouseRightButtonUp += Tile_RightClick;


                    GameBoard.Children.Add(btn);
                    buttons[r, c] = btn;
                }
            }

            TimerText.Text = "000";
            MinesLeftText.Text = mineCount.ToString("000");
        }

        private void HookEngineEvents()
        {
            Engine.TimerTick += (sec) => TimerText.Text = sec.ToString("000");
            Engine.TileRevealed += (r, c, val) => buttons[r, c].Background = TileImages[val];
            Engine.FlagsChanged += (remaining) => MinesLeftText.Text = remaining.ToString("000");
            Engine.GameOver += () => MessageBox.Show("You died", "Game Over");
            Engine.GameWon += () => MessageBox.Show("You win!", "Congratulations");
        }

        private void Tile_LeftClick(object sender, RoutedEventArgs e)
        {
            click.Play();
            var btn = sender as Button;
            if (btn == null) return;

            var pos = (System.Windows.Point)btn.Tag;
            Engine.HandleLeftClick((int)pos.X, (int)pos.Y);
        }

        private void Tile_RightClick(object sender, MouseButtonEventArgs e)
        {
            mark.Play();
            var btn = sender as Button;
            if (btn == null) return;

            var pos = (System.Windows.Point)btn.Tag;
            Engine.HandleRightClick((int)pos.X, (int)pos.Y);

            if (Engine.bts[(int)pos.X, (int)pos.Y].IsFlagged)
            {
                btn.Background = FlagImage; // Use ImageBrush directly
            }
            else
            {
                btn.Background = UnclickedImage; // Use ImageBrush directly
            }
        }

        private void DisableBoard()
        {
            for (int r = 0; r < boardSize; r++)
            {
                for (int c = 0; c < boardSize; c++)
                {
                    buttons[r, c].IsEnabled = false;
                }
            }
        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            GameBoard.Children.Clear();
            Engine.Reset((int)GameBoard.Width / boardSize);
            InitializeBoard();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new MainMenuPage(_mainFrame));
        }
    }
}
