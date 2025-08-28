using System.Windows;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Custom startup code
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            // Cleanup code
        }
    }
}
