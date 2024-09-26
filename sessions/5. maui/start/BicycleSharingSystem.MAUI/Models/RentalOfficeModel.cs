using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.Models
{
    public class RentalOfficeModel
    {
        [JsonProperty("officeId")]
        public string OfficeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("region")]
        public string Region { get; set; } = string.Empty;

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("bicycles")]
        public List<BicycleModel>? Bicycles { get; set; }


        [JsonIgnore]  // JSON 변환에서 무시되도록 지정
                      // 전체 자전거 수
        public int TotalBicycles => Bicycles?.Count ?? 0;

        [JsonIgnore]  // JSON 변환에서 무시되도록 지정
                      // 대여 가능한 자전거 수 (isRental이 false인 자전거 수)
        public int AvailableBicycles => Bicycles?.Count(b => !b.IsRented) ?? 0;

        [JsonIgnore]  // JSON 변환에서 무시되도록 지정
                      // 대여 가능 여부 (대여 가능한 자전거가 1대 이상인 경우 true)
        public bool IsAvailable => AvailableBicycles > 0; 

        [JsonIgnore]  // JSON 변환에서 무시되도록 지정
                      // 대여 가능 여부 (대여 가능한 자전거가 1대 이상인 경우 true)
        public string IsAvailableText => AvailableBicycles > 0 ? "대여 가능" : "대여 불가";


        // Location 객체 (위도와 경도가 있는 경우에만 생성)
        public Location? OfficeLocation => (Latitude.HasValue && Longitude.HasValue)
            ? new Location(Latitude.Value, Longitude.Value)
            : new Location(37.554722, 126.970833);
    }

   
}
