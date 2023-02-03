using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Layout:BaseEntity
    {
        public Layout()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Design { get; set; }
        public string? Description { get; set; }
        public string? IconPathLayout { get; set; }
        public ICollection<WidgetCodeSnippet> widgetCodeSnippets { get; set; }
    }
}
