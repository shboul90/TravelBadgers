using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TravelBadgers.Data;

namespace TravelBadgers.Models
{
    public class RequestCreate
    {
        [Display(Name = "Departing City")]
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }

        [Required]
        [Display(Name = "Trip Budget")]
        public decimal OverallBudget { get; set; }

        [Required]
        [Display(Name = "Depart Date")]
        public DateTime DepartDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

    }
}
