using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class PropertyDTO
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public int? ParentPropertyID { get; set; }
        public string? Description { get; set; }
        //public string Value { get; set; }
        public string DefaultValue { get; set; }
        //public bool IsDefault { get; set; }
        public List<PropertyValueDTO>? Values { get; set; }
        public List<PropertyUnitDTO>? Units { get; set; }
    }
}
