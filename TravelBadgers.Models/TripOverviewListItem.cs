using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBadgers.Data;

namespace TravelBadgers.Models
{
    public class TripOverviewListItem
    {
        public int TripOverviewId { get; set; }

        [Display(Name = "Depart City")]
        public int DepartCityId { get; set; }

        [Display(Name = "Arrival City")]
        public ICollection<City> ArrivalCityId { get; set; }

        [Display(Name = "Date Create")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
