using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class TripOverviewListItem
    {
        public int TripOverviewId { get; set; }

        public int DepartCityId { get; set; }

        public List<int> ArrivalCityId { get; set; }

        public List<decimal> OverallCost { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
    }
}
