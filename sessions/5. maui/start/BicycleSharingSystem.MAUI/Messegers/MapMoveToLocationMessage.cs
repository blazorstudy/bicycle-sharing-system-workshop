using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleSharingSystem.MAUI.Messegers
{
    public class MapMoveToLocationMessage
    {
        public Location Location { get; }

        public MapMoveToLocationMessage(Location location)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location), "Location이 없습니다");
        }
    }
}
