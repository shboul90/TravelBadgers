using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Models
{
    public class RequestDetail
    {
        public int RequestId { get; set; }

        public bool FromHomeTown { get; set; }

        public int CityId { get; set; }

        public decimal OverallBudget { get; set; }

        public DateTime DepartDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
