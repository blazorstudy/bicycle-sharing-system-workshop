using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.Models
{
    public class BicycleModel
    {
        [JsonProperty("bicycleId")]
        public string BicycleId { get; set; }

        [JsonProperty("rentalOfficeId")]
        public string RentalOfficeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("startRentalTime")]
        public DateTime? StartRentalTime { get; set; }

        [JsonProperty("expireRentalTime")]
        public DateTime? ExpireRentalTime { get; set; }

        [JsonIgnore]  // JSON 변환에서 무시되도록 지정
        public bool IsRented => StartRentalTime.HasValue;
    }
}
