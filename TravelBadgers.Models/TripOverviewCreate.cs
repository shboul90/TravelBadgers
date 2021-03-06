using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TravelBadgers.Models
{
    public class TripOverviewCreate
    {

        [Required]
        [Display(Name = "Request Id")]
        public int RequestId { get; set; }
        public IEnumerable<SelectListItem> Request { get; set; }

    }
}
