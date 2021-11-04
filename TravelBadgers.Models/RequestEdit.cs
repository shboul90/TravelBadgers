using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TravelBadgers.Models
{
    public class RequestEdit
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public bool FromHomeTown { get; set; }

        public int CityId { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }

        [Required]
        public decimal OverallBudget { get; set; }

        [Required]
        public DateTime DepartDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
