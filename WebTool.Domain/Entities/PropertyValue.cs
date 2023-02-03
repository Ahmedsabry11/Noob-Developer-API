using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    public class PropertyValue:BaseEntity
    {
        public PropertyValue(){}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
        public int PropertyID { get; set; }
        public bool IsDefault { get; set; }
        
        // TODO: foreign key & Relations between entities
        public virtual Property Property { get; set; }
       

    }
}
