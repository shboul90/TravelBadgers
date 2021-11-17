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
    public class TripCreate
    {
        [Display(Name = "Request Id")]
        public int RequestId { get; set; }
        public IEnumerable<SelectListItem> Request { get; set; }
    }
}
