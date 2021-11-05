using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Data
{
    public class TripOverview
    {
        [Key]
        public int TripOverviewId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }

        [Required]
        [ForeignKey(nameof(City))]
        public List<int> ArrivalCityId { get; set; }
        public virtual City ArrivalCity { get; set; }

        [Required]
        public List<decimal> FlightCost { get; set; }

        [Required]
        public List<decimal> OverallCost { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
