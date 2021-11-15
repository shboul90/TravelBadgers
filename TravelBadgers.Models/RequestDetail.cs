using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class RequestDetail
    {
        public int RequestId { get; set; }

        [Display(Name = "Departing City")]
        public int CityId { get; set; }

        [Display(Name = "Trip Budget")]
        public decimal OverallBudget { get; set; }

        [Display(Name = "Depart Date")]
        public DateTime DepartDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Date Request Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Date Request Last Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
