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
        [ForeignKey(nameof(City))]
        public int ArrivalCityId { get; set; }
        public int DepartCityId { get; set; }
        public virtual City City { get; set; }

        [Required]
        [ForeignKey(nameof(Request))]
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }

        [Required]
        public decimal FlightCost { get; set; }

        [Required]
        public decimal OverallCost { get; set; }
    }
}
