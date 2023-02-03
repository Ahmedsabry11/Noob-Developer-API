using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Entities
{
    public class Unit:BaseEntity
    {
        public Unit(){}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitName { get; set; }
        public  bool isDefault { get; set; }

        // TODO: foreign key & Relations between entities
        public virtual ICollection <PropertyUnit> PropertyUnits { get; set; }
    }
}
