using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class PropertyValueDTO
    {
        //public string valueName { get; set; }
        public bool isDefault { get; set; }
        public string value { get; set; }
        public int Id { get; set; }
        public int? propertyID { get; set; }
    }
}
