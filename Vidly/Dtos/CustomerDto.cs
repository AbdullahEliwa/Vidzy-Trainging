using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public int MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
        
        public DateTime? Birthdate { get; set; }
    }
}