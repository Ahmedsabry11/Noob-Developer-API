using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Entities
{
    [Index(nameof(User.Email), IsUnique = true)]
    public class User
    {
        User()
        { }
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        // should have fixed size
        [Required]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        public int NoPassword { get; set; }
        public string SubscriptionDate { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsActive { get; set; }


        // navigation properites (foreign keys)
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}
