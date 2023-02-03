using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Attribute = DomainLayer.Entities.Attribute;

namespace DomainLayer.Entities
{
    public class WidgetAttribute
    {
        public WidgetAttribute ()
        {

        }
        //[Key, Column(Order = 1)]
        public int WidgetId { get; set; }
        //[Key, Column(Order = 2)]
        public int AttributeId { get; set; }
        //[StringLength(50)]
        //public string DefaultValue { get; set; }

        // navigation properties 
        // TODO: foreign key & Relations between entities
        public virtual Widget Widget { get; set; }
        public virtual Attribute Attribute { get; set; }

    }
}
