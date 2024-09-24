using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace BicycleSharingSystem.Kiosk.Pages.Bicycle.Components;

public class BicyclePanel : ListBox
{
    protected override Control CreateContainerForItemOverride(object? item, int index, object? recycleKey)
    {
        return new BicycleItem();
    }
}