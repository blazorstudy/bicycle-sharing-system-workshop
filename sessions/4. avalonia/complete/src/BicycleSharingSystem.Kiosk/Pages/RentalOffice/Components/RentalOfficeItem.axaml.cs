using System.ComponentModel;
using System.Windows.Input;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace BicycleSharingSystem.Kiosk.Pages.RentalOffice.Components;

public class RentalOfficeItem : TemplatedControl
{
    public static readonly AvaloniaProperty CommandProperty =
        AvaloniaProperty.Register<RentalOfficeItem, ICommand?>(nameof(Command));

    public ICommand? Command
    {
        get => (ICommand?)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public RentalOfficeItem()
    {
        this.Tapped += (s, e) =>
        {
            this.Command?.Execute(this.DataContext);
        };
    }
}