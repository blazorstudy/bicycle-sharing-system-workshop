namespace BicycleSharingSystem.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("SelectChoiceBikePage", typeof(Pages.SelectChoiceBikePage));
            Routing.RegisterRoute("SelectChoiceFeePage", typeof(Pages.SelectChoiceFeePage));
        }
    }
}
