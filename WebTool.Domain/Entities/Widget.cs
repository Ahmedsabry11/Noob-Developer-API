using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Attribute = DomainLayer.Entities.Attribute;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.Entities
{
    public class Widget: BaseEntity
    {
        public Widget()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        //[StringLength(500)]
        public string? Description { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
        public string IconPath { get; set; }
        public int? ParentWidgetID  { get; set; }

        public int RelatedAppTypeID { get; set; }
        public bool IsOnlyNested { get; set; }


        // navigation properites (foreign keys)
        [ForeignKey("ParentWidgetID")]
        //public virtual Widget ParentWidget { get; set; }

        public virtual ICollection<Widget> ChildWidgets { get; set; }

        [ForeignKey("RelatedAppTypeID")]
        public virtual AppType AppType { get; set; }
        public virtual ICollection<WidgetProperty> WidgetProperties { get; set; }
        public virtual ICollection<WidgetCodeSnippet> WidgetCodeSnippets { get; set; }
        public virtual ICollection<WidgetAttribute> WidgetAttributes { get; set; }

    }
}
