using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBadgers.Data;

namespace TravelBadgers.Models
{
    public class CityListItem
    {
        public string CityName { get; set; }

        public decimal AvgHotelDailyCost { get; set; }

        public decimal AvgEntertainmentDaily { get; set; }

        public decimal AvgFoodDaily { get; set; }

        public CuisineType Cuisine { get; set; }

        public ClimateType Climate { get; set; }

        public TerrainType Terrain { get; set; }

        public decimal CityRating { get; set; }
    }
}
