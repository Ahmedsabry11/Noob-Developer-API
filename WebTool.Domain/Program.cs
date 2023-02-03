
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Entities;
using DomainLayer.Data;
using Attribute = DomainLayer.Entities.Attribute;

namespace DomainLayer
{
    
    class Program
    {

        //static IQueryable<T> WidgetCodeFunc<T>(APPDBContext databasecontext)
        //{
        //    IQueryable<T> results = (IQueryable<T>)from w in databasecontext.Widgets
        //                                           join cw in databasecontext.WidgetCodeSnippets
        //                                                     on w.Id equals cw.Widget.Id
        //                                           where w.Title == "Div" && cw.TargetFrameworkID == 1
        //                                           select w;
                                    

        //    return results;
        //}
        static void Main(string [] args)

        {
            //DbContextOptions<APPDBContext> options = new DbContextOptions<APPDBContext>();

            // -------------- configure database connection string ---------------------------
            var contextOptions = new DbContextOptionsBuilder<APPDBContext>()
                .UseSqlServer(@"Data Source=DESKTOP-MIQ99GT\SQLEXPRESS;Initial Catalog=APPDB;Integrated Security=True")
                                .Options;
            APPDBContext databasecontext = new APPDBContext(contextOptions);

            //var results = from w in databasecontext.Widgets
            //              join cw in databasecontext.WidgetCodeSnippets
            //              on w.ID equals cw.Widget.ID
            //              where w.Title == "Div" && cw.TargetFrameworkID == 1
            //              select new
            //              {
            //                  Title = w.Title,
            //                  Code = cw.CoddeSnippet
            //              };
            // var results = WidgetCodeFunc<Widget>(databasecontext);

            //foreach (var result in results)
            //{
            //    Console.WriteLine("Title : " + result.Title);
            //    Console.WriteLine("Code : " + result.Id);
            //}

            // ------------- fill apptype and targetframework tables ----------------
            try
            {
                databasecontext.AppTypes.Add(
                    new AppType
                    {
                        Type = "WEB"
                    }
                );
                databasecontext.TargetFrameworks.Add(
                         new TargetFramework
                         {
                             FrameworkName = "Bootstrap"
                         }
                     );
                databasecontext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // --------------------- fill Widget table --------------------------
            try
            {
                databasecontext.Widgets.Add(
                    new Widget
                    {
                        //ID = 2,
                        //ParentWidgetID = ,
                        Title = "Header",
                        IsOnlyNested = true,
                        RelatedAppTypeID = 1,
                        IconPath = "/Header",
                        Description = ""
                    }
                );
                databasecontext.Widgets.Add(
                    new Widget
                    {
                        Title = "Footer",
                        IsOnlyNested = true,
                        RelatedAppTypeID = 1,
                        IconPath = "/Footer",
                        Description = ""
                    }
                );
                databasecontext.Widgets.Add(
                    new Widget
                    {
                        Title = "Div",
                        IsOnlyNested = true,
                        RelatedAppTypeID = 1,
                        IconPath = "/Div",
                        Description = "normal Div"
                    }
                );

                databasecontext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // --------------------- fill codeSnippet table --------------------------
            try
            {
                databasecontext.WidgetCodeSnippets.Add(
                    new WidgetCodeSnippet
                    {
                        WidgetID = 1,
                        TargetFrameworkID = 1,
                        CodeSnippet = "<header > </header>"
                    }
                    );
                databasecontext.WidgetCodeSnippets.Add(
                    new WidgetCodeSnippet
                    {
                        WidgetID = 2,
                        TargetFrameworkID = 1,
                        CodeSnippet = "<footer > </footer>"
                    }
                );
                databasecontext.WidgetCodeSnippets.Add(
                    new WidgetCodeSnippet
                    {
                        WidgetID = 3,
                        TargetFrameworkID = 1,
                        CodeSnippet = "<div > </div>"
                    }
                );

                databasecontext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Hello, World!");
        }
    }
    

}

