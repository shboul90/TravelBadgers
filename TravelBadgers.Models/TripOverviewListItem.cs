using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class TripOverviewListItem
    {
        public int TripOverviewId { get; set; }

        [Display(Name = "Depart City")]
        public int DepartCityId { get; set; }

        [Display(Name = "Arrival City")]
        public List<int> ArrivalCityId { get; set; }

        [Display(Name = "Overall Trip Cost")]
        public List<decimal> OverallCost { get; set; }

        [Display(Name = "Date Create")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
