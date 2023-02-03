using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    public class Project:BaseEntity
    {
        public Project()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public DateTime CreationDate{ get; set; }
        [ForeignKey("ApplicationUser")]
        public string? UserID{ get; set; }
        public int AppTypeID { get; set; }
        public int TargetFrameworkID { get; set; }
        [StringLength(200)]
        public string? GeneratedAppPath { get; set; }
        public string Widgets { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }

        // navigation properites (foreign keys)
        public virtual AppType AppType { get; set; }
        //public virtual User User { get; set; }
        public virtual ApplicationUser  ApplicationUser  { get; set; }
        public virtual TargetFramework TargetFramework { get; set; }

    }
}
