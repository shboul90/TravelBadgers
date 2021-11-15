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
        [Display(Name = "Request Id")]
        public int RequestId { get; set; }

        [Required]
        [Display(Name = "Depart City")]
        public int DepartCityId { get; set; }

        [Required]
        [Display(Name = "Arrival City")]
        public List<int> ArrivalCityId { get; set; }

        [Required]
        [Display(Name = "Flight Cost")]
        public List<decimal> FlightCost { get; set; }

        [Required]
        [Display(Name = "Overall Trip Cost")]
        public List<decimal> OverallCost { get; set; }

        [Required]
        [Display(Name = "Date Create")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
