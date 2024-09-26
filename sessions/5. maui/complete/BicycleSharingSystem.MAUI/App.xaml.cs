

namespace BicycleSharingSystem.MAUI
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }
        public App(IServiceProvider provider)
        {
            InitializeComponent();
            Services = provider;
            MainPage = new AppShell();
        }
    }
}
