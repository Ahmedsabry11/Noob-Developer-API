using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Property:BaseEntity
    {

        public Property() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        [Required]
        public string PropertyName { get; set; }
        public int? ParentPropertyID { get; set; }
        public bool IsOnlyNested { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }

        // navigation properties 
        // TODO: foreign key & Relations between entities
        virtual public Property Parent { get; set; }

        virtual public ICollection<WidgetProperty> WidgetProperties { get; set; }
        virtual public ICollection<PropertyUnit> PropertyUnits { get; set; }
        virtual public ICollection<PropertyValue> PropertyValues { get; set; }

    }
}
