using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    public class AppType:BaseEntity
    {
        public AppType()
        { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        // should be enum or like enum not string
        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        // navgiation properities
        public virtual ICollection<Widget> Widgets { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
