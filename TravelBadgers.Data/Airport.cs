using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Data
{
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }

        [Required]
        public string AirportName { get; set; }

        [Required]
        public string AirpotZipCode { get; set; }

        [Required]
        public decimal LocationNorth { get; set; }

        [Required]
        public decimal LocationWest { get; set; }
    }
}