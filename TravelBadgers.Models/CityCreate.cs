using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBadgers.Data;

namespace TravelBadgers.Models
{
    public class CityCreate
    {
        [Required]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "City ZipCode")]
        public string CityZipCode { get; set; }

        [Required]
        public decimal LocationNorth { get; set; }

        [Required]
        public decimal LocationWest { get; set; }

        [Required]
        [Display(Name = "Average Daily Hotel Cost")]
        public decimal AvgHotelDailyCost { get; set; }

        [Required]
        [Display(Name = "Average Daily Entertainment Cost")]
        public decimal AvgEntertainmentDaily { get; set; }

        [Required]
        [Display(Name = "Average Daily Food Cost")]
        public decimal AvgFoodDaily { get; set; }

        [Required]
        public CuisineType Cuisine { get; set; }

        [Required]
        public ClimateType Climate { get; set; }

        [Required]
        public TerrainType Terrain { get; set; }

        [Display(Name = "City Rating")]
        public decimal CityRating { get; set; }
    }
}
