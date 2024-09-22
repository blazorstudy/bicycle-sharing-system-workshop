using BicycleSharingSystem.Kiosk.Pages.RentalOffice.Models;

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BicycleSharingSystem.Kiosk.ViewModels.MessengerModel;

public class SelectRentalOfficeChangeMessage : ValueChangedMessage<RentalOfficeModel>
{
    public SelectRentalOfficeChangeMessage(RentalOfficeModel user) : base(user)
    {        
    }
}