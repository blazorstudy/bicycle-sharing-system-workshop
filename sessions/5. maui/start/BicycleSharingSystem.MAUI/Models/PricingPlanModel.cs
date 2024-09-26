using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.Models
{
    public class PricingPlanModel
    {
        /// <summary>
        /// // 요금제 이름
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 적용 시간(분)
        /// </summary>
        public int time { get; set; }
        /// <summary>
        /// 요금제 설명
        /// </summary>
        public int price { get; set; }

        public string description { get; set; }
        /// <summary>
        /// 가장 많은 회원들이 선택한 요금제인지 여부
        /// </summary>
        public bool isMostPopular { get; set; }

        public string planTitle => $"{name} ({price}/{time}분)";
    }
}
