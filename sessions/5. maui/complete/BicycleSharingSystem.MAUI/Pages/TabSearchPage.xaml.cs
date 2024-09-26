using BicycleSharingSystem.MAUI.Messegers;
using BicycleSharingSystem.MAUI.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace BicycleSharingSystem.MAUI.Pages;

public partial class TabSearchPage : ContentView
{
	public TabSearchPage()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<MapMoveToLocationMessage>(this, (r, message) =>
        {
            map.Pins.Clear(); // ���� �ɵ��� ����
            foreach (var office in (this.BindingContext as TabSearchPageViewModel).RentalOffices)
            {
                var pin = new Pin
                {
                    Label = office.Name,
                    Address = office.Region,
                    Type = PinType.Place,
                    Location = office.OfficeLocation
                };
                map.Pins.Add(pin);
            }

            // �޽����� ������ ������ �ش� ��ġ�� �̵�
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Location(message.Location.Latitude, message.Location.Longitude),
                Distance.FromKilometers(0.5))); // �� ���� ����
        });
    }
}