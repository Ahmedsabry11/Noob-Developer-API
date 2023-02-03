using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Project> Projects { get; set; }
    }
}
