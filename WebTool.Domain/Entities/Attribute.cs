using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    public class Attribute:BaseEntity
    {
        public Attribute()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string AttributeName { get; set; }
        //[StringLength(50)]
        //public string Value { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }

        // navigation properties 
        // TODO: foreign key & Relations between entities
        public virtual ICollection<WidgetAttribute> WidgetAttributes { get; set; }

    }
}
