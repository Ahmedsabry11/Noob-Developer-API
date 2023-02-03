using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DomainLayer.Entities
{
    public class WidgetCodeSnippet
    {
        public WidgetCodeSnippet()
        {

        }
        public int WidgetID { get; set; }

        public int TargetFrameworkID { get; set; }

        public int LayoutID { get; set; }
        [Required]
        //[StringLength(700)]
        public string CodeSnippet { get; set; }

        // navigation properites (foreign keys)
        public virtual Widget Widget { get; set; }
        public virtual TargetFramework TargetFramework{ get; set; }
        public virtual Layout Layout { get; set; }

    }
}
