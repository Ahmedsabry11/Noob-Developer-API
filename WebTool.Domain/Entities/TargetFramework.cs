using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    public class TargetFramework:BaseEntity
    {
        public TargetFramework()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FrameworkName { get; set; }

        //navgiation 
        public virtual ICollection<WidgetCodeSnippet> WidgetCodeSnippets { get; set; }
        public virtual ICollection<Project> Projects { get; set; }

    }
}
