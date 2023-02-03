using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class WidgetProperty
    {
        public WidgetProperty() { }
        public int WidgetID { get; set; }
        public int PropertyID { get; set; }
        [StringLength(50)]
        public string DefaultValue { get; set; }

        // Navigation properties 
        // TODO: foreign key & Relations between entities
        virtual public Property Property { get; set; }
        virtual public Widget Widget { get; set; }
    }
}
