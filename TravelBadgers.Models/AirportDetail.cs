using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class AirportDetail
    {
        public int AirportId { get; set; }

        public string AirportName { get; set; }

        public string AirpotZipCode { get; set; }

        public decimal LocationNorth { get; set; }

        public decimal LocationWest { get; set; }
    }
}
