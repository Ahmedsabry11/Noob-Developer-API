using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class PropertyUnitDTO
    {
        public string UnitName { get; set; }
        public bool DefaultOfUnit { get; set; }
        public bool? DefaultUnitOfProperty { get; set; }
        public int? UnitId { get; set; }
        public int? PropertyID { get; set; }

    }
}
