using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace BicycleSharingSystem.Kiosk.Pages.RentalOffice.Components;

public class RentalOfficeItem : Button
{
    public RentalOfficeItem()
    {
        this.Click += (s, e) =>
        {
            this.Command.Execute(this.DataContext);
        };
    }
}