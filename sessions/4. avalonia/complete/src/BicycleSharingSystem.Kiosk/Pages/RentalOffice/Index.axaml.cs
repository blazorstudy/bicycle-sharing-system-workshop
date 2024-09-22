using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using BicycleSharingSystem.Kiosk.ViewModels;

namespace BicycleSharingSystem.Kiosk.Pages.RentalOffice;

public partial class Index : UserControl
{
    public Index()
    {
        InitializeComponent();
        this.Loaded += (s, e) =>
        {
            ((ViewModelBase)this.DataContext).Load();
        };
    }
}