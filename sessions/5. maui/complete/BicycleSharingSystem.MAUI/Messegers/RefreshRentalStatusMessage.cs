using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.Messegers
{
    public class RefreshRentalStatusMessage
    {
        public string BicycleId { get; }
        public string OfficeId { get; }

        public RefreshRentalStatusMessage(string bicycleId, string officeId)
        {
            if (string.IsNullOrEmpty(bicycleId))
                throw new ArgumentException("BicycleId가 없습니다", nameof(bicycleId));
            if (string.IsNullOrEmpty(officeId))
                throw new ArgumentException("OfficeId가 없습니다", nameof(officeId));

            BicycleId = bicycleId;
            OfficeId = officeId;
        }
    }
}
