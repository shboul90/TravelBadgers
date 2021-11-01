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
        public string CityName { get; set; }

        [Required]
        public string CityZipCode { get; set; }

        [Required]
        public decimal LocationNorth { get; set; }

        [Required]
        public decimal LocationWest { get; set; }

        [Required]
        public decimal AvgHotelDailyCost { get; set; }

        [Required]
        public decimal AvgEntertainmentDaily { get; set; }

        [Required]
        public decimal AvgFoodDaily { get; set; }

        [Required]
        public CuisineType Cuisine { get; set; }

        [Required]
        public ClimateType Climate { get; set; }

        [Required]
        public TerrainType Terrain { get; set; }


        public decimal CityRating { get; set; }
    }
}
