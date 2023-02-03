using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class PropertyUnit
    {
        public PropertyUnit() { }
        //[Key,Column(Order =  1)]
        public int PropertyID { set; get; }
        //[Key, Column(Order = 2)]
        public int UnitID { set; get; }
        public bool? isDefault { set; get; }

        // TODO: foreign key & Relations between entities
        virtual public Property Property { set; get; }
        virtual public Unit Unit { set; get; }

    }
}
