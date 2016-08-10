using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadExcel.DataClasses
{
    public class RoomJoin
    {
        public string HotelCode { get; set; }
        public string RoomReference { get; set; }
        public string MappedTo { get; set; }
        public string Description { get; set; }
    }
}
