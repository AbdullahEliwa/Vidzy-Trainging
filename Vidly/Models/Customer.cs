using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public MembershipType MembershipType { get; set; }

        [Required]
        [Display(Name= "Memebership type")]
        public int MembershipTypeId { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name= "Date of birth")]
        public DateTime? Birthdate { get; set; }
    }
}