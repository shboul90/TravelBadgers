using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Data
{
    public class Trip
    {
        public int TripId { get; set; }

        public Guid OwnerId { get; set; }

        public City ArrivalCity { get; set; }

        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }

        public decimal FlightCost { get; set; }

        public decimal OverallCost { get; set; }

    }
}
