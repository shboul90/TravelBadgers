using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class TripOverviewDetail
    {
        public int TripOverviewId { get; set; }

        public int DepartCityId { get; set; }

        public int RequestId { get; set; }
        
        public List<int> ArrivalCityId { get; set; }

        public List<decimal> FlightCost { get; set; }

        public List<decimal> OverallCost { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
    }
}
