using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Entities
{
    public class UserToken
    {
        UserToken()
        {

        }
        [Key]
        public int ID { get; set; }
        // foreign key
        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }
        public int Token { get; set; }
        public bool IsValidAgent { get; set; }

        // navigation properites (foreign keys)
        public virtual User User { get; set; }
    }
}
