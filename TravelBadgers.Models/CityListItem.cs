using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBadgers.Data;

namespace TravelBadgers.Models
{
    public class CityListItem
    {
        public int CityId { get; set; }

        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Display(Name = "Average Daily Hotel Cost")]
        public decimal AvgHotelDailyCost { get; set; }

        [Display(Name = "Average Daily Entertainment Cost")]
        public decimal AvgEntertainmentDaily { get; set; }

        [Display(Name = "Average Daily Food Cost")]
        public decimal AvgFoodDaily { get; set; }

        public CuisineType Cuisine { get; set; }

        public ClimateType Climate { get; set; }

        public TerrainType Terrain { get; set; }

        [Display(Name = "City Rating")]
        public decimal CityRating { get; set; }
    }
}
