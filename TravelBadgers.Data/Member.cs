using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBadgers.Data
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public string StreetAddress { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string ZipCode { get; set; }
         
        public string PhoneNumber { get; set; }

        public DateTime DateJoined { get; set; }
    }
}
