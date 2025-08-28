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
        private DispatcherTimer? gameTimer;
        private int elapsedSeconds;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            StartTimer();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            ResetTimer();
        }

        private void InitializeTimer()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Tick += (s, e) => TimerLabel.Content = $"Time: {++elapsedSeconds:D3}";
        }

        public void StartTimer() => gameTimer.Start();
        public void StopTimer() => gameTimer.Stop();
        public void ResetTimer()
        {
            StopTimer();
            elapsedSeconds = 0;
            TimerLabel.Content = "Time: 000";
            StartTimer();
        }
    }
}