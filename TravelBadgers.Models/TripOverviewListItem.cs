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

        public int ArrivalCityId { get; set; }

        public int DepartCityId { get; set; }

        public decimal OverallCost { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
    }
}
