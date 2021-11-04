using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Data
{
    public enum CuisineType { Southern, EastCoast, WestCoast, Northern, MidWest }
    public enum ClimateType { Tropical, Dry, Temperate, Continental, Polar }
    public enum TerrainType { Canyon, Desert, Forest, Glacier, Hill, Marsh, Mountain, Oasis, Ocean, Open, River, Swamp, Tundra, Valley }
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

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
