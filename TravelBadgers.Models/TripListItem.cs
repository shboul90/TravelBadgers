using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class TripListItem
    {
        public int TripId { get; set; }

        [Display(Name = "Request ID")]
        public int RequestId { get; set; }

        [Display(Name = "Depart City")]
        public string DepartCityName{ get; set; }

        [Display(Name = "Arrival City")]
        public string ArrivalCityName{ get; set; }

        [Display(Name = "Overall Trip Cost")]
        public decimal OverallCost { get; set; }

        [Display(Name = "Date Create")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
