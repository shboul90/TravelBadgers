using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Data
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        [Required]
        public bool FromHomeTown { get; set; }

        [Required]
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public decimal OverallBudget { get; set; }


        [Required]
        public DateTime DepartDate { get; set; }

        public DateTime ReturnDate { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
