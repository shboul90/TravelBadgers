using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class TripOverviewCreate
    {
        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public int ArrivalCityId { get; set; }

        [Required]
        public int DepartCityId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public decimal FlightCost { get; set; }

        [Required]
        public decimal OverallCost { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
