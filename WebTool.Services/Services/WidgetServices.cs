using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;
using DomainLayer.Entities;
using DomainLayer.Data;
using RepositoryLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using WebTool.Services.IServices;

namespace WebTool.Services.Services
{
    public class WidgetServices:IWidgetServices
    {
        private readonly IRepository<Widget> _WidgetRepository;
        private readonly APPDBContext _context;

        public WidgetServices(IRepository<Widget> WidgetRepository, APPDBContext context)
        {
            _WidgetRepository = WidgetRepository ??
                throw new ArgumentNullException(nameof(WidgetRepository));
            _context = context ??
                throw new ArgumentNullException(nameof(context));


        }
        


        public  async Task<List<WidgetDTO>> GetAllChildWidgetCodes(int? id)
        {

            var widgets = await (from w in _context.Widgets
                                 join cw in _context.WidgetCodeSnippets
                                 on w.Id equals cw.Widget.Id
                                 join l in _context.Layouts
                                 on cw.LayoutID equals l.Id
                                 where w.ParentWidgetID == id && cw.TargetFrameworkID == 2
                                 orderby l.Id
                                 select new
                                 {
                                     Id = w.Id,
                                     Title = w.Title,
                                     Description = w.Description,
                                     ParentWidgetID = w.ParentWidgetID,
                                     RelatedAppTypeID = w.RelatedAppTypeID,
                                     iconpath = w.IconPath,
                                     CodeSnippet = cw.CodeSnippet,
                                     Design = l.Design,
                                     LayoutDescription = l.Description,
                                     IconPathLayout = l.IconPathLayout
                                 }).ToListAsync();
            if (widgets.Count == 0)
                return null;
            // ------------------------- should use automapper ---------------------------
            List<WidgetDTO> result = new List<WidgetDTO>();
            foreach (var widget in widgets)
            {
                result.Add( new WidgetDTO
                {
                    Id = widget.Id,
                    Title = widget.Title,
                    Description = widget.Description,
                    ParentWidgetID = widget.ParentWidgetID,
                    RelatedAppTypeID = widget.RelatedAppTypeID,
                    IconPath = widget.iconpath,
                    CodeSnippet = widget.CodeSnippet,
                    Design = widget.Design,
                    LayoutDescription= widget.LayoutDescription,
                    LayoutIconPath = widget.IconPathLayout,
                });
                
            }
            return result;
        }
        public async Task<List<AttributesDTO>> GetWidgetAttriubtes(int id)
        {
            Widget widget = await _WidgetRepository.Get(id);
            if (widget == null)
                return null;

            var widgetattributes = await (from  wa in _context.widgetAttributes
                                 join A in _context.Attributes
                                 on wa.AttributeId equals A.Id
                                 where wa.WidgetId == id
                                 select new
                                 {
                                     A.Id,
                                     A.AttributeName,
                                     A.Description
                                 }).ToListAsync();
            List<AttributesDTO> attributes = new List<AttributesDTO>();
            foreach (var widgetattr in widgetattributes)
            {
                attributes.Add(new AttributesDTO
                {
                   Id = widgetattr.Id,
                   AttributeName = widgetattr.AttributeName,
                   Description = widgetattr.Description
                });

            }
            return attributes;
        }
        //public async Task<WidgetATO> GetWidget(int id)
        //{
        //    var widget = await _WidgetRepository.Get(id);
        //    if (widget == null)
        //        return null;
        //    // ------------------------- should use automapper ---------------------------
        //    WidgetATO result = new WidgetATO
        //    {
        //        Description = widget.Description,
        //        //IconPath = widget.IconPath,
        //        Id = widget.Id,
        //        IsOnlyNested = widget.IsOnlyNested,
        //        ParentWidgetID = (int)widget.ParentWidgetID,
        //        RelatedAppTypeID = widget.RelatedAppTypeID,
        //        Title = widget.Title
        //    };
        //    return result;

        //}
        //public async Task<string> GetWidgetIcon(int id)
        //{
        //    //------------------- can use a query that return only Iconpath ------------------
        //    var widget = await _WidgetRepository.Get(id);
        //    if (widget == null)
        //        return null;
        //    string iconPath = widget.IconPath;
        //    return iconPath;

        //}

        //public async Task<List<WidgetATO>> GetChildWidgets(int? id)
        //{
        //    var widgets = await _context.Widgets.Where(w => w.ParentWidgetID == id).ToListAsync();
        //    List<WidgetATO> results = new List<WidgetATO>();

        //    // ------------------------- should use automapper ---------------------------
        //    foreach (var widget in widgets)
        //    {
        //        results.Add(new WidgetATO
        //        {
        //            Description = widget.Description,
        //            //IconPath = widget.IconPath,
        //            Id = widget.Id,
        //            IsOnlyNested = widget.IsOnlyNested,
        //            ParentWidgetID = widget.ParentWidgetID,
        //            RelatedAppTypeID = widget.RelatedAppTypeID,
        //            Title = widget.Title
        //        }
        //        );
        //    }
        //    return results;
        //}
        //public async Task<WidgetATO>GetWidgetCode (int id)
        //{

        //    var widgets = await (from w in _context.Widgets
        //                         join cw in _context.WidgetCodeSnippets
        //                         on w.Id equals cw.Widget.Id
        //                         where w.Id == id && cw.TargetFrameworkID == 2
        //                         select new {Id = w.Id, Title = w.Title,
        //                                     Description = w.Description,
        //                                    ParentWidgetID = w.ParentWidgetID,
        //                                    RelatedAppTypeID = w.RelatedAppTypeID,
        //                                    CodeSnippet = cw.CodeSnippet
        //                                    }  ).ToListAsync();
        //    if (widgets.Count == 0)
        //        return null;
        //    // ------------------------- should use automapper ---------------------------
        //    WidgetATO result = new WidgetATO();
        //    foreach(var widget in widgets)
        //    {
        //        result.Id = widget.Id;
        //        result.Title = widget.Title;
        //        result.Description = widget.Description;
        //        result.ParentWidgetID = widget.ParentWidgetID;
        //        result.RelatedAppTypeID = widget.RelatedAppTypeID;
        //        result.CodeSnippet = widget.CodeSnippet;
        //    }
        //    return result;
        //}
        //public async Task<List<WidgetATO>> GetAllWidgets()
        //{

        //    var widgets = await _WidgetRepository.GetAll();
        //    List<WidgetATO> results = new List<WidgetATO>();

        //    // ------------------------- should use automapper ---------------------------
        //    foreach (var widget in widgets)
        //    {
        //        results.Add(new WidgetATO
        //        {
        //            Description = widget.Description,
        //            //IconPath = widget.IconPath,
        //            Id = widget.Id,
        //            IsOnlyNested = widget.IsOnlyNested,
        //            ParentWidgetID = widget.ParentWidgetID,
        //            RelatedAppTypeID = widget.RelatedAppTypeID,
        //            Title = widget.Title
        //        }
        //        );
        //    }
        //    return results; ;

        //}
    }
}
