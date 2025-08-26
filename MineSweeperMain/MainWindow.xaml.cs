using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MineSweeperMain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private int elapsedTime = 0;

        private BitmapImage flag = new BitmapImage(new Uri("@../../../../Images/flag.png", UriKind.Relative));
        private BitmapImage unclicked = new BitmapImage(new Uri("@../../../../Images/unclicked.jpg", UriKind.Relative));
        private BitmapImage[] images;

        public MainWindow()
        {
            InitializeComponent();

            LoadImages();

            Engine.Init(this, 16, 40);
        }

        private void LoadImages()
        {
            images = new BitmapImage[]
            {
                new BitmapImage(new Uri(@"../../../Images/0.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/1.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/2.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/3.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/4.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/5.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/6.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/7.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/8.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/mine.jpg", UriKind.Relative)),
                new BitmapImage(new Uri(@"../../../Images/defusedmine.jpg", UriKind.Relative))
            };
        }
    }
}