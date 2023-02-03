using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Entities;
using Attribute = DomainLayer.Entities.Attribute;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DomainLayer.Data
{
    public class APPDBContext : IdentityDbContext<ApplicationUser>
    {
        public APPDBContext(DbContextOptions<APPDBContext> options)
            : base(options)
        {

        }
        public DbSet<Widget> Widgets { get; set; }
        public DbSet<WidgetCodeSnippet> WidgetCodeSnippets { get; set; }
        public DbSet<WidgetProperty> WidgetProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyUnit> PropertyUnits { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TargetFramework> TargetFrameworks { get; set; }
        public DbSet<AppType> AppTypes { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }
        public DbSet<WidgetAttribute> widgetAttributes { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Layout> Layouts { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MIQ99GT\\SQLEXPRESS;Initial Catalog=APPDB;Integrated Security=True");
        //    return;
        //}

        //--------------------- Fluent API for defining Database ------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //------------------Map entity to table----------------------
            modelBuilder.Entity<Widget>().ToTable("Widget");
            modelBuilder.Entity<WidgetCodeSnippet>().ToTable("WidgetCodeSnippet");
            modelBuilder.Entity<WidgetProperty>().ToTable("WidgetProperty");
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<PropertyUnit>().ToTable("PropertyUnit");
            modelBuilder.Entity<Unit>().ToTable("Unit");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<TargetFramework>().ToTable("TargetFramework");
            modelBuilder.Entity<AppType>().ToTable("AppType");
            modelBuilder.Entity<PropertyValue>().ToTable("PropertyValue");
            modelBuilder.Entity<WidgetAttribute>().ToTable("WidgetAttribute");
            modelBuilder.Entity<Attribute>().ToTable("Attribute");
            modelBuilder.Entity<Layout>().ToTable("Layout");



            // ----------------- configure composite primary keys -------------------- 
            modelBuilder.Entity<PropertyUnit>().HasKey(table => new { table.PropertyID, table.UnitID });
            modelBuilder.Entity<WidgetAttribute>().HasKey(table => new { table.WidgetId, table.AttributeId });
            modelBuilder.Entity<WidgetProperty>().HasKey(table => new { table.WidgetID, table.PropertyID });
            modelBuilder.Entity<WidgetCodeSnippet>().HasKey(table => new { table.WidgetID, table.TargetFrameworkID, table.LayoutID });



            //------------------ configure foreign key constraints --------------------------

            // ------------------ WidgetProperty Relation---------------------
            modelBuilder.Entity<WidgetProperty>()
                 .HasOne(wp => wp.Property)
                 .WithMany(p => p.WidgetProperties)
                 .HasForeignKey(wp => wp.PropertyID)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_WidgetProperty_Property");


            modelBuilder.Entity<WidgetProperty>()
                 .HasOne(wp => wp.Widget)
                 .WithMany(w => w.WidgetProperties)
                 .HasForeignKey(wp => wp.WidgetID)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_WidgetProperty_Widget");


            // ------------------ WidgetAttribute Relation---------------------
            modelBuilder.Entity<WidgetAttribute>()
                 .HasOne(wa => wa.Attribute)
                 .WithMany(a => a.WidgetAttributes)
                 .HasForeignKey(wa => wa.AttributeId)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_WidgetAttribute_Attribute");


            modelBuilder.Entity<WidgetAttribute>()
                 .HasOne(wa => wa.Widget)
                 .WithMany(w => w.WidgetAttributes)
                 .HasForeignKey(wa => wa.WidgetId)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_WidgetAttribute_Widget");


            // ------------------ WidgetCodeSnippet Relation---------------------
            modelBuilder.Entity<WidgetCodeSnippet>()
                 .HasOne(wcs => wcs.TargetFramework)
                 .WithMany(t => t.WidgetCodeSnippets)
                 .HasForeignKey(wcs => wcs.TargetFrameworkID)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_WidgetCodeSnippet_TargetFramework");


            modelBuilder.Entity<WidgetCodeSnippet>()
                 .HasOne(wcs => wcs.Widget)
                 .WithMany(w => w.WidgetCodeSnippets)
                 .HasForeignKey(wcs => wcs.WidgetID)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_WidgetCodeSnippet_Widget");

            modelBuilder.Entity<WidgetCodeSnippet>()
                 .HasOne(wcs => wcs.Layout)
                 .WithMany(l => l.widgetCodeSnippets)
                 .HasForeignKey(wcs => wcs.LayoutID)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_WidgetCodeSnippet_Layout");


            // ------------------ PropertyUnit Relation---------------------
            modelBuilder.Entity<PropertyUnit>()
                 .HasOne(up => up.Unit)
                 .WithMany(u => u.PropertyUnits)
                 .HasForeignKey(up => up.UnitID)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_PropertyUnit_Unit");


            modelBuilder.Entity<PropertyUnit>()
                 .HasOne(up => up.Property)
                 .WithMany(p => p.PropertyUnits)
                 .HasForeignKey(up => up.PropertyID)
                 .IsRequired(true)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("ForeignKey_PropertyUnit_Property");
            // ------------------ Property Relation ---------------------
            modelBuilder.Entity<Property>()
                 .HasOne(p => p.Parent)
                 .WithOne()
                 .HasForeignKey<Property>(ch => ch.ParentPropertyID)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.NoAction)
                 .HasConstraintName("ForeignKey_Property_ParentPropery");

            // ------------------Widget Relation-------------------- -
            //modelBuilder.Entity<Widget>()
            //     .HasOne(w => w.ParentWidget)
            //     .WithOne()
            //     .HasForeignKey<Widget>(w => w.ParentWidgetID)
            //     .IsRequired(false)
            //     .OnDelete(DeleteBehavior.NoAction)
            //     .HasConstraintName("ForeignKey_Widget_ParentWidget").IsRequired(false);

            // modelBuilder.Entity<Widget>()
            //     .HasOne(w => w.ParentWidget)
            //     .WithMany(cw => cw.ChildWidgets)
            //     .HasForeignKey(cw => cw.ParentWidgetID)
            //     .IsRequired(false)
            //     .OnDelete(DeleteBehavior.NoAction)
            //     .HasConstraintName("ForeignKey_Widget_ParentWidget");

            modelBuilder.Entity<Widget>()
                .HasMany(cw => cw.ChildWidgets)
                .WithOne()
                .HasForeignKey(cw => cw.ParentWidgetID)
                .IsRequired(false);


            //modelBuilder.Entity<Widget>().Property(w => w.Description).IsRequired(false);
            //modelBuilder.Entity<Widget>().Property(w => w.IconPath).IsRequired(false);


            //-------------------- Unique Constraints ------------------------------

            //-----------    Table Widget     -----------------
            modelBuilder.Entity<Widget>().HasIndex(cw => cw.Title).IsUnique();
            modelBuilder.Entity<Widget>().HasIndex(cw => cw.IconPath).IsUnique();
            //-----------  Table TargetFramework --------------
            modelBuilder.Entity<TargetFramework>().HasIndex(cw => cw.FrameworkName).IsUnique();
            //-----------    Table AppType     -----------------
            modelBuilder.Entity<AppType>().HasIndex(cw => cw.Type).IsUnique();
            //-----------    Table Property    -----------------
            modelBuilder.Entity<Property>().HasIndex(cw => cw.PropertyName).IsUnique();
            //-----------    Table Unit        ------------------
            modelBuilder.Entity<Unit>().HasIndex(cw => cw.UnitName).IsUnique();
            //-----------    Table Project      -----------------
            modelBuilder.Entity<Project>().HasIndex(p => new { p.UserID, p.Title }).IsUnique();




            //-------------------- seeding database ---------------------------------
            // ########### not efficient way to seed database here ##########
            modelBuilder.Entity<ApplicationUser>()
                .HasData(
                new ApplicationUser
                {
                    Id = "1",
                    EmailConfirmed = false,
                    LockoutEnabled = false,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                }
                );
            modelBuilder.Entity<AppType>()
               .HasData(
                       new AppType
                       {
                           Id = 1,
                           Type = "WEB"
                       },
                       new AppType
                       {
                           Id = 2,
                           Type = "MOBILE"
                       }
                   );
            modelBuilder.Entity<TargetFramework>()
               .HasData(
                       new TargetFramework
                       {
                           Id = 1,
                           FrameworkName = "Bootstrap"
                       },
                       new TargetFramework
                       {
                           Id = 2,
                           FrameworkName = "html"
                       }
                   );

            modelBuilder.Entity<Widget>()
                .HasData(
                    new Widget
                    {
                        Id = 1,
                        Title = "div",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "div.png",
                        Description = "The <div> tag defines a division or a section in an HTML document." +
                                      " The <div> tag is used as a container for HTML elements - which is then styled with CSS or manipulated with JavaScript." +
                                      " The <div> tag is easily styled by using the class or id attribute." +
                                      " Any sort of content can be put inside the <div> tag!"
                    },
                    new Widget
                    {
                        Id = 2,
                        Title = "span",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "span.png",
                        Description =
                            "The <span> tag is an inline container used to mark up a part of a text, or a part of a document." +
                            " The <span> tag is easily styled by CSS or manipulated with JavaScript using the class or id attribute"
                    },
                    new Widget
                    {
                        Id = 3,
                        Title = "section",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "section.png",
                        Description =
                            "Section tag defines the section of documents such as chapters, headers, footers or any other sections." +
                            " The section tag divides the content into section and subsections." +
                            " The section tag is used when requirements of two headers or footers or any other section of documents needed."
                    },
                    new Widget
                    {
                        Id = 4,
                        Title = "header",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "header.png",
                        Description =
                            "The <header> tag in HTML is used to define the header for a document or a section as it contains the information related to the title and heading of the related content." +
                            " The <header> element is intended to usually contain the section’s heading (an h1-h6 element or an <hgroup> element), but this is not required." +
                            " It can also be used to wrap a section’s table of contents, a search form, or any relevant logos." +
                            " The <header> tag is a new tag in HTML5 and it is a container tag ie.," +
                            " it contains a starting tag, content & the end tag." +
                            " There can be several <header> elements in one document." +
                            " This tag cannot be placed within a <footer>, <address> or another <header> element."
                    }
                    ,
                    new Widget
                    {
                        Id = 5,
                        Title = "footer",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "footer.png",
                        Description = "The <footer> tag in HTML is used to define a footer of HTML document." +
                                      " This section contains the footer information (author information," +
                                      " copyright information, carriers, etc). The footer tag is used within the body tag." +
                                      " The <footer> tag is new in the HTML5. The footer elements require a start tag as well as an end tag."
                    }
                    ,
                    new Widget
                    {
                        Id = 6,
                        Title = "p",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "p.png",
                        Description =
                            "The < p > HTML element represents a paragraph." +
                            "Paragraphs are usually represented in visual media as " +
                            "blocks of text separated from adjacent blocks by blank lines " +
                            "and / or first - line indentation."
                    },
                    new Widget
                    {
                        Id = 7,
                        Title = "navbar",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "navbar.png",
                        Description =
                            "The <nav> HTML element represents a section of a page whose purpose is to provide navigation links," +
                            " either within the current document or to other documents." +
                            " Common examples of navigation sections are menus, tables of contents, and indexes."
                    },
                    new Widget
                    {
                        Id = 9,
                        Title = "carousel",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "carousel.png",
                        Description =
                            "Carousel is a slide show for cycling through a series of content, built with CSS 3D transforms and a bit of JavaScript. " +
                            "It works with a series of images, text, or custom markup." +
                            " It also includes support for previous/next controls and indicators."
                    },
                    new Widget
                    {
                        Id = 8,
                        Title = "img",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "image.png",
                        Description = "The <img> tag creates a holding space for the referenced image. " +
                                      "The <img> tag has two required attributes: src - Specifies the path to the image." +
                                      " alt - Specifies an alternate text for the image, " +
                                      "if the image for some reason cannot be displayed."
                    },
                    new Widget
                    {
                        Id = 10,
                        Title = "button",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "button.png",
                        Description =
                        "The <button> HTML element is an interactive element activated by a user with a mouse," +
                        " keyboard, finger, voice command, or other assistive technology. Once activated," +
                        " it then performs a programmable action, such as submitting a form or opening a dialog"
                    },
                    new Widget
                    {
                        Id = 11,
                        Title = "buttongroup",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "buttongroup.png",
                        Description =
                        "The <button> HTML element is an interactive element activated by a user with a mouse," +
                        " keyboard, finger, voice command, or other assistive technology. Once activated," +
                        " it then performs a programmable action, such as submitting a form or opening a dialog"
                    },
                    new Widget
                    {
                        Id = 14,
                        Title = "Page",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "page.png",
                        Description = "A meta description is an HTML element that provides a brief summary of a web page"
                    },
                    new Widget
                    {
                        Id = 13,
                        Title = "Header1",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "header1.png",
                        Description = "The h1 should describe the topic of your page and its content. It’s possible that the h1 tag is similar to your title tag. Usually the h1 tag is the title of your post or blog post. Normally," +
                                      " the h1 tag gives the reader an idea of the content of a web page."
                    },
                    new Widget
                    {
                        Id = 12,
                        Title = "footer_layout",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "footer_layout.png",
                        Description = "A website's footer is an area located at the bottom of every page on a website, below the main body content." +
                                      " The term “footer” comes from the print world, in which the “footer” is a consistent design element that is seen across all pages of a document."
                    },
                    new Widget
                    {
                        Id = 15,
                        Title = "form",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "form.png",
                        Description = "A web form, also called an HTML form, is an online page that allows for user input. " +
                                      "It is an interactive page that mimics a paper document or form, where users fill out particular fields." +
                                      " Web forms can be rendered in modern browsers using HTML and related web-oriented languages."
                    },
                    new Widget
                    {
                        Id = 16,
                        Title = "Sign-In",
                        IsOnlyNested = false,RelatedAppTypeID = 1,
                        IconPath = "sign-in.png",
                        Description = "A website page which views to users when signing in"
                    },
                    new Widget
                    {
                        Id = 17,
                        Title = "Dashboard",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "dashboard.png",
                        Description = "Dashboards are business intelligence (BI) reporting tools that aggregate and display critical metrics and key performance indicators (KPIs) in a single screen," +
                                      " enabling users to monitor and examine business performance at a glance"
                    },
                    new Widget
                    {
                        Id = 18,
                        Title = "Product",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "product.png",
                        Description = "A product is the item offered for sale. A product can be a service or an item." +
                                      "It can be physical or in virtual or cyber form. Every product is made at a cost and each is sold at a price"
                    },
                    new Widget
                    {
                        Id = 19,
                        Title = "Blog",
                        IsOnlyNested = false,
                        RelatedAppTypeID = 1,
                        IconPath = "blog.png",
                        Description = "is a personal online journal that is frequently updated and intended for general public consumption. Blogs are defined by their format: " +
                                      "a series of entries posted to a single page in reverse-chronological order"
                    }
                );
            modelBuilder.Entity<Layout>().HasData(
                 new Layout
                 {
                     Id = 1,
                     Design = "Basic", //div ,btn ,footer
                     Description = "Simple widgets with few basic styling like div, span, etc...",
                     IconPathLayout = "LayoutBasic.png"
                 },
                 new Layout
                 {
                     Id = 2,
                     Design = "Layout", // carousel
                     Description = "Complex widgets with amazing styling like carousel, button group, etc...",
                     IconPathLayout = "LayoutLayout.png"
                 },
                 new Layout
                 {
                     Id = 3,
                     Design = "Page",
                     Description = "A complete page with styling ready to use",
                     IconPathLayout = "LayoutPage.png"
                 }
            );
            modelBuilder.Entity<WidgetCodeSnippet>().HasData(
                new WidgetCodeSnippet
                {
                    WidgetID = 1,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet =
                    "{ \"name\": \"div\"," +
                    "\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, " +
                    "\"children\": [],\"text\": \"Div (container)\"}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 2,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet =
                    "{ \"name\": \"span\"," +
                    "\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, " +
                    "\"children\": [],\"text\": \"Span\"}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 3,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet =
                    "{ \"name\": \"section\"," +
                    "\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, " +
                    "\"children\": [],\"text\": \"Section\"}"
                }
                ,
                new WidgetCodeSnippet
                {
                    WidgetID = 4,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet =
                    "{ \"name\": \"header\"," +
                    "\"style\": { \"bootstrap\": \"border border-primary text-center\" }," +
                    "\"children\": [],\"text\": \"Header\"}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 5,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet =
                    "{ \"name\": \"footer\"," +
                    "\"style\": { \"bootstrap\": \"border border-primary text-center\" }," +
                    "\"children\": [],\"text\": \"Footer\"}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 6,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet =
                    "{ \"name\": \"p\"," +
                    "\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, " +
                    "\"children\": [],\"text\": \"Paragraph\"}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 7,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet = "{ \"name\": \"nav\",\"style\": {\"bootstrap\": \"navbar navbar-expand-lg navbar-light bg-light\"}," +
                                  "\"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"navbar-brand\"}," + "\"children\": [],\"text\": \"Navbar\"}," +
                                  "{\"name\": \"button\",\"style\": {\"bootstrap\": \"navbar-toggler\"}," +
                                  "\"children\": [{\"name\": \"span\"" +
                                  ",\"style\": {\"bootstrap\": \"navbar-toggler-icon\"}, " +
                                  "\"children\": [],\"text\": \"\"}],\"text\": \"\"}, {\"name\": \"div\"," +
                                  "\"style\": {\"bootstrap\": \"collapse navbar-collapse\"}," +
                                  " \"children\": [{\"name\": \"ul\",\"style\": {\"bootstrap\": \"navbar-nav mr-auto\"}, " +
                                  "\"children\": [{\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item active\"},\"children\": [{\"name\": \"a\"," +
                                  "\"style\": {\"bootstrap\": \"nav-link\"}, \"children\": [],\"text\": \"Home\"}]," +
                                  " \"text\": \"\"}, {\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item\"}," +
                                  " \"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"nav-link\"}, " +
                                  "\"children\": [],\"text\": \"Link\"}],\"text\": \"\"}," +
                                  "{\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item dropdown\"}," +
                                  " \"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"nav-link dropdown-toggle\"}," +
                                  " \"children\": [],\"text\": \"Dropdown\"}," +
                                  "{\"name\": \"div\",\"style\": {\"bootstrap\": \"dropdown-menu\"}," +
                                  " \"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"dropdown-item\"}, \"children\": [],\"text\": \"Action\"}," +
                                  "{\"name\": \"a\",\"style\": {\"bootstrap\": \"dropdown-item\"}," +
                                  " \"children\": [],\"text\": \"Another action\"},{\"name\": \"div\"," +
                                  "\"style\": {\"bootstrap\": \"dropdown-divider\"}," +
                                  " \"children\": [],\"text\": \"\"},{\"name\": \"a\",\"style\": {\"bootstrap\": \"dropdown-item\"}," +
                                  " \"children\": [],\"text\": \"Something else here\"}]," +
                                  " \"text\": \"\"}], \"text\": \"\"},{\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item\"}," +
                                  "\"children\": [{\"name\": \"a\"," +
                                  "\"style\": {\"bootstrap\": \"nav-link disabled\"}, " +
                                  "\"children\": [],\"text\": \"Disabled\"}],\"text\": \"\"}]," +
                                  " \"text\": \"\"}]," +
                                  " \"text\": \"\"}], \"text\": \"\"}"

                },
                new WidgetCodeSnippet
                {
                    WidgetID = 9,
                    TargetFrameworkID = 2,
                    LayoutID = 2,
                    CodeSnippet = "{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel slide\"}," +
                        "\"children\":[{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-inner\"}," +
                        "\"children\":[{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-item active\"}," +
                        "\"children\":[{\"name\":\"img\",\"style\":{\"bootstrap\":\"d-block w-100\"}," +
                        "\"children\":[],\"text\":null," +
                        "\"attributes\":{\"alt\":\"First slide\"," +
                        "\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}]," +
                        "\"text\":\"\",\"attributes\":{}},{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-item\"}," +
                        "\"children\":[{\"name\":\"img\",\"style\":{\"bootstrap\":\"d-block w-100\"},\"children\":[],\"text\":null," +
                        "\"attributes\":{\"alt\":\"First slide\"," +
                        "\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}],\"text\":\"\"," +
                        "\"attributes\":{}},{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-item\"},\"children\":[{\"name\":\"img\"," +
                        "\"style\":{\"bootstrap\":\"d-block w-100\"},\"children\":[],\"text\":null,\"attributes\":{\"alt\":\"First slide\"," +
                        "\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}]," +
                        "\"text\":\"\",\"attributes\":{}}],\"text\":\"\",\"attributes\":{}},{\"name\":\"a\",\"style\":{\"bootstrap\":\"carousel-control-prev\"}," +
                        "\"children\":[{\"name\":\"span\",\"style\":{\"bootstrap\":\"carousel-control-prev-icon\"},\"children\":[],\"text\":\"\"," +
                        "\"attributes\":{\"aria-hidden\":\"true\"}},{\"name\":\"span\",\"style\":{\"bootstrap\":\"sr-only\"}," +
                        "\"children\":[],\"text\":\"Previous\",\"attributes\":{}}],\"text\":\"\",\"attributes\":{\"href\":\"#carouselExampleControls\"," +
                        "\"role\":\"button\",\"data-slide\":\"prev\"}},{\"name\":\"a\",\"style\":{\"bootstrap\":\"carousel-control-next\"}," +
                        "\"children\":[{\"name\":\"span\",\"style\":{\"bootstrap\":\"carousel-control-next-icon\"},\"children\":[],\"text\":\"\"," +
                        "\"attributes\":{\"aria-hidden\":\"true\"}},{\"name\":\"span\",\"style\":{\"bootstrap\":\"sr-only\"},\"children\":[],\"text\":\"Next\"," +
                        "\"attributes\":{}}],\"text\":\"\",\"attributes\":{\"href\":\"#carouselExampleControls\",\"role\":\"button\",\"data-slide\":\"next\"}}]," +
                        "\"text\":\"\",\"attributes\":{\"id\":\"carouselExampleControls\",\"data-ride\":\"carousel\"}}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 8,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet = "{\"name\":\"img\",\"style\":{\"width\":\"100px\",\"height\":\"100px\",\"backgroundColor\":\"red\"},\"children\":[],\"text\":null,\"attributes\":{\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 10,
                    TargetFrameworkID = 2,
                    LayoutID = 1,
                    CodeSnippet = "{ \"name\": \"button\"," + "\"style\": {\"bootstrap\": \"btn btn-primary\"}," + "\"children\": []," +
                                  "" + "\"text\": \"button\"}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 11,
                    TargetFrameworkID = 2,
                    LayoutID = 2,
                    CodeSnippet =
                    "{ \"name\": \"div\"," +
                    "\"style\": {\"bootstrap\": \"btn-group\"},\"children\": [{\"name\": \"button\"," +
                    "\"style\": {\"bootstrap\": \"btn btn-secondary\"}," +
                    "\"children\": [],\"text\": \"button\"},{\"name\": \"button\"," +
                    "\"style\": {\"bootstrap\": \"btn btn-primary\"},\"children\": [],\"text\": \"button\" }," +
                    "{\"name\": \"button\"," +
                    "\"style\": {\"bootstrap\": \"btn btn-success\"},\"children\": [],\"text\": \"button\"}],\"text\": \"\"}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 12,
                    TargetFrameworkID = 2,
                    LayoutID = 2,
                    CodeSnippet = "{\"name\":\"footer\",\"text\":\"\",\"style\":{\"bootstrap\":\"text-center text-lg-start bg-light text-muted\"}," +
                                  "\"attributes\":{},\"children\":[{\"name\":\"section\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-flex justify-content-center justify-content-lg-between p-4 border-bottom\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-5 d-none d-lg-block\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Get connected with us on social networks:\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-facebook-f\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-twitter\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-google\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-instagram\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-linkedin\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-github\"},\"attributes\":{},\"children\":[]}]}]}]},{\"name\":\"section\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"container text-center text-md-start mt-5\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"row mt-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-3 col-lg-4 col-xl-3 mx-auto mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Company name\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-gem me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Here you can use rows and columns to organize your footer content. Lorem ipsum                                dolor sit amet, consectetur adipisicing elit.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-2 col-lg-2 col-xl-2 mx-auto mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Products\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Angular\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"React\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Vue\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Laravel\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-3 col-lg-2 col-xl-2 mx-auto mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Useful links\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Settings\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Orders\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Help\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-4 col-lg-3 col-xl-3 mx-auto mb-md-0 mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Contact\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"New York, NY 10012, US\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-home me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"info@example.com\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-envelope me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"+ 01 234 567 88\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-phone me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"+ 01 234 567 89\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-print me-3\"},\"attributes\":{},\"children\":[]}]}]}]}]}]},{\"name\":\"div\",\"text\":\"© 2021 Copyright:\",\"style\":{\"bootstrap\":\"text-center p-4\"},\"attributes\":{\"name\":\"background-color: rgba(0, 0, 0, 0.05);\"},\"children\":[{\"name\":\"a\",\"text\":\"MDBootstrap.com\",\"style\":{\"bootstrap\":\"text-reset fw-bold\"}," +
                                  "\"attributes\":{\"name\":\"https://mdbootstrap.com/\"},\"children\":[]}]}]}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 13,
                    TargetFrameworkID = 2,
                    LayoutID = 2,
                    CodeSnippet = "{\"name\":\"header\",\"text\":\"\",\"style\":{\"bootstrap\":\"p-3 bg-dark text-white\"}," +
                                  "\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"container\"}," +
                                  "\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-flex align-items-center mb-2 mb-lg-0 text-white text-decoration-none\"},\"attributes\":{\"name\":\"/\"},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Home\",\"style\":{\"bootstrap\":\"nav-link px-2 text-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Features\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"FAQs\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"About\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"form\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-12 col-lg-auto mb-3 mb-lg-0 me-lg-3\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control form-control-dark\"},\"attributes\":{\"name\":\"Search\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"text-end\"},\"attributes\":{},\"children\":[{\"name\":\"button\",\"text\":\"Login\",\"style\":{\"bootstrap\":\"btn btn-outline-light me-2\"},\"attributes\":{\"name\":\"button\"},\"children\":[]},{\"name\":\"button\",\"text\":\"Sign-up\",\"style\":{\"bootstrap\":\"btn btn-warning\"},\"attributes\":{\"name\":\"button\"},\"children\":[]}]}]}]}]}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 14,
                    TargetFrameworkID = 2,
                    LayoutID = 3,
                    CodeSnippet = "{\"name\":\"div\",\"text\":\"\",\"style\":{},\"attributes\"" +
                                  ":{},\"children\":[{\"name\":\"nav\",\"text\":\"\",\"style\":{\"bootstrap\":\"site-header sticky-top py-1\"}," +
                                  "\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"container d-flex flex-column flex-md-row justify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"py-2\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Tour\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Product\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Features\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Enterprise\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Support\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Cart\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"position-relative overflow-hidden p-3 p-md-5 m-md-3 text-center bg-light\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-5 p-lg-5 mx-auto my-5\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Punny headline\",\"style\":{\"bootstrap\":\"display-4 font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading to boot. Jumpstart your marketing\\n                        efforts with\\n                        this example based on Apple's marketing pages.\",\"style\":{\"bootstrap\":\"lead font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Coming soon\",\"style\":{\"bootstrap\":\"btn btn-outline-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"product-device box-shadow d-none d-md-block\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"product-device product-device-2 box-shadow d-none d-md-block\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-dark mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center text-white overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-dark box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-dark box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-primary mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center text-white overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"footer\",\"text\":\"\",\"style\":{\"bootstrap\":\"container py-5\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-12 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"small\",\"text\":\"\u00a9 2017-2018\",\"style\":{\"bootstrap\":\"d-block mb-3 text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Features\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Cool stuff\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Random feature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Team feature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Stuff for developers\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Another one\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Last time\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resource name\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Another resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Final resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Business\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Education\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Government\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Gaming\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"About\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Team\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Locations\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Privacy\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Terms\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]}]}]}]}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 15,
                    TargetFrameworkID = 2,
                    LayoutID = 2,
                    CodeSnippet = "{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"container\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"py-5 text-center\"},\"attributes\":{},\"children\":[{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"d-block mx-auto mb-4\"},\"attributes\":{\"name\":\"72\"},\"children\":[]},{\"name\":\"h2\",\"text\":\"Checkout form\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Below is an example form built entirely with Bootstrap's form controls. Each required form group has a validation state that can be triggered by attempting to submit the form without completing it.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-4 order-md-2 mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":null,\"style\":{\"bootstrap\":\"d-flex justify-content-between align-items-center mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Your cart\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"span\",\"text\":\"3\",\"style\":{\"bootstrap\":\"badge badge-secondary badge-pill\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-group mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between lh-condensed\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Product name\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"Brief description\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"$12\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between lh-condensed\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Second product\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"Brief description\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"$8\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between lh-condensed\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Third item\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"Brief description\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"$5\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between bg-light\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"text-success\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Promo code\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"EXAMPLECODE\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"-$5\",\"style\":{\"bootstrap\":\"text-success\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Total (USD)\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"strong\",\"text\":\"$20\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"form\",\"text\":null,\"style\":{\"bootstrap\":\"card p-2\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"Promo code\"},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group-append\"},\"attributes\":{},\"children\":[{\"name\":\"button\",\"text\":\"Redeem\",\"style\":{\"bootstrap\":\"btn btn-secondary\"},\"attributes\":{\"name\":\"submit\"},\"children\":[]}]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-8 order-md-1\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"Billing address\",\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[]},{\"name\":\"form\",\"text\":null,\"style\":{\"bootstrap\":\"needs-validation\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"First name\",\"style\":{},\"attributes\":{\"name\":\"firstName\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Valid first name is required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Last name\",\"style\":{},\"attributes\":{\"name\":\"lastName\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Valid last name is required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Username\",\"style\":{},\"attributes\":{\"name\":\"username\"},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group-prepend\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"@\",\"style\":{\"bootstrap\":\"input-group-text\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Your username is required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{\"name\":\"width: 100%;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Email\",\"style\":{},\"attributes\":{\"name\":\"email\"},\"children\":[{\"name\":\"span\",\"text\":\"(Optional)\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"you@example.com\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Please enter a valid email address for shipping updates.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Address\",\"style\":{},\"attributes\":{\"name\":\"address\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Please enter your shipping address.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Address 2\",\"style\":{},\"attributes\":{\"name\":\"address2\"},\"children\":[{\"name\":\"span\",\"text\":\"(Optional)\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"Apartment or suite\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-5 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Country\",\"style\":{},\"attributes\":{\"name\":\"country\"},\"children\":[]},{\"name\":\"select\",\"text\":null,\"style\":{\"bootstrap\":\"custom-select d-block w-100\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"option\",\"text\":\"Choose...\",\"style\":{},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"option\",\"text\":\"United States\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Please select a valid country.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-4 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"State\",\"style\":{},\"attributes\":{\"name\":\"state\"},\"children\":[]},{\"name\":\"select\",\"text\":null,\"style\":{\"bootstrap\":\"custom-select d-block w-100\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"option\",\"text\":\"Choose...\",\"style\":{},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"option\",\"text\":\"California\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Please provide a valid state.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-3 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Zip\",\"style\":{},\"attributes\":{\"name\":\"zip\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Zip code required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"hr\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-checkbox\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"same-address\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Shipping address is the same as my billing address\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"same-address\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-checkbox\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"save-info\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Save this information for next time\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"save-info\"},\"children\":[]}]},{\"name\":\"hr\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"h4\",\"text\":\"Payment\",\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-block my-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-radio\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Credit card\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"credit\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-radio\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Debit card\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"debit\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-radio\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Paypal\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"paypal\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Name on card\",\"style\":{},\"attributes\":{\"name\":\"cc-name\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"small\",\"text\":\"Full name as displayed on card\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":\"Name on card is required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Credit card number\",\"style\":{},\"attributes\":{\"name\":\"cc-number\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Credit card number is required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-3 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Expiration\",\"style\":{},\"attributes\":{\"name\":\"cc-expiration\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Expiration date required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-3 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"CVV\",\"style\":{},\"attributes\":{\"name\":\"cc-expiration\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Security code required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"hr\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"button\",\"text\":\"Continue to checkout\",\"style\":{\"bootstrap\":\"btn btn-primary btn-lg btn-block\"},\"attributes\":{\"name\":\"submit\"},\"children\":[]}]}]}]},{\"name\":\"footer\",\"text\":null,\"style\":{\"bootstrap\":\"my-5 pt-5 text-muted text-center text-small\"},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"\u00a9 2017-2018 Company Name\",\"style\":{\"bootstrap\":\"mb-1\"},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Privacy\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Terms\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Support\",\"style\":{},\"attributes\":{\"name\":\"#\"}," +
                                  "\"children\":[]}]}]}]}]}"

                },
                new WidgetCodeSnippet
                {
                    WidgetID = 16,
                    TargetFrameworkID = 2,
                    LayoutID = 3,
                    CodeSnippet = "{\"name\":\"form\",\"text\":null,\"style\":{\"bootstrap\":\"form-signin\"},\"attributes\":{},\"children\":[{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{\"name\":\"72\"},\"children\":[]},{\"name\":\"h1\",\"text\":\"Please sign in\",\"style\":{\"bootstrap\":\"h3 mb-3 font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"label\",\"text\":\"Email address\",\"style\":{\"bootstrap\":\"sr-only\"},\"attributes\":{\"name\":\"inputEmail\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Password\",\"style\":{\"bootstrap\":\"sr-only\"},\"attributes\":{\"name\":\"inputPassword\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"checkbox mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Remember me\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"remember-me\"},\"children\":[]}]}]},{\"name\":\"button\",\"text\":\"Sign in\",\"style\":{\"bootstrap\":\"btn btn-lg btn-primary btn-block\"},\"attributes\":{\"name\":\"submit\"},\"children\":[]},{\"name\":\"p\",\"text\":\"\u00a9 2017-2018\",\"style\":{\"bootstrap\":\"mt-5 mb-3 text-muted\"}," +
                                  "\"attributes\":{},\"children\":[]}]}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 17,
                    TargetFrameworkID = 2,
                    LayoutID = 3,
                    CodeSnippet = "{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"navbarnavbar-darksticky-topbg-darkflex-md-nowrapp-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Companyname\",\"style\":{\"bootstrap\":\"navbar-brandcol-sm-3col-md-2mr-0\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-controlform-control-darkw-100\"},\"attributes\":{\"name\":\"Search\"},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"navbar-navpx-3\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-itemtext-nowrap\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Signout\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"container-fluid\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-2d-noned-md-blockbg-lightsidebar\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"sidebar-sticky\"},\"attributes\":{},\"children\":[{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"navflex-column\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Dashboard\",\"style\":{\"bootstrap\":\"nav-linkactive\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-home\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M39l9-797v11a22001-22H5a22001-2-2z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"92291215121522\"},\"children\":[]}]},{\"name\":\"span\",\"text\":\"(current)\",\"style\":{\"bootstrap\":\"sr-only\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Orders\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M132H6a22000-22v16a2200022h12a220002-2V9z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"132139209\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Products\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-shopping-cart\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1\"},\"children\":[]},{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1\"},\"children\":[]},{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M11h4l2.6813.39a2200021.61h9.72a220002-1.61L236H6\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Customers\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-users\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M1721v-2a44000-4-4H5a44000-44v2\"},\"children\":[]},{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"4\"},\"children\":[]},{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M2321v-2a44000-3-3.87\"},\"children\":[]},{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M163.13a4400107.75\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Reports\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-bar-chart-2\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"4\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"14\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Integrations\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-layers\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"polygon\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"122271212227122\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21712222217\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21212172212\"},\"children\":[]}]}]}]}]},{\"name\":\"h6\",\"text\":null,\"style\":{\"bootstrap\":\"sidebar-headingd-flexjustify-content-betweenalign-items-centerpx-3mt-4mb-1text-muted\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Savedreports\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":null,\"style\":{\"bootstrap\":\"d-flexalign-items-centertext-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-plus-circle\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"16\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"12\"},\"children\":[]}]}]}]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"navflex-columnmb-2\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Currentmonth\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Lastquarter\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Socialengagement\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Year-endsale\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]}]}]}]},{\"name\":\"main\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-9ml-sm-autocol-lg-10pt-3px-4\"},\"attributes\":{\"name\":\"main\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"chartjs-size-monitor\"},\"attributes\":{\"name\":\"position:absolute;inset:0px;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"chartjs-size-monitor-expand\"},\"attributes\":{\"name\":\"position:absolute;left:0;top:0;right:0;bottom:0;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"position:absolute;width:1000000px;height:1000000px;left:0;top:0\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"chartjs-size-monitor-shrink\"},\"attributes\":{\"name\":\"position:absolute;left:0;top:0;right:0;bottom:0;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"position:absolute;width:200%;height:200%;left:0;top:0\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-flexjustify-content-betweenflex-wrapflex-md-nowrapalign-items-centerpb-2mb-3border-bottom\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Dashboard\",\"style\":{\"bootstrap\":\"h2\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"btn-toolbarmb-2mb-md-0\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"btn-groupmr-2\"},\"attributes\":{},\"children\":[{\"name\":\"button\",\"text\":\"Share\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondary\"},\"attributes\":{},\"children\":[]},{\"name\":\"button\",\"text\":\"Export\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondary\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"button\",\"text\":\"Thisweek\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondarydropdown-toggle\"},\"attributes\":{},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-calendar\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"rect\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"2\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]}]}]}]}]},{\"name\":\"canvas\",\"text\":null,\"style\":{\"bootstrap\":\"my-4chartjs-render-monitor\"},\"attributes\":{\"name\":\"display:block;height:276px;width:655px;\"},\"children\":[]},{\"name\":\"h2\",\"text\":\"Sectiontitle\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"table-responsive\"},\"attributes\":{},\"children\":[{\"name\":\"table\",\"text\":null,\"style\":{\"bootstrap\":\"tabletable-stripedtable-sm\"},\"attributes\":{},\"children\":[{\"name\":\"thead\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"th\",\"text\":\"#\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"tbody\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,001\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Lorem\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ipsum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"dolor\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sit\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,002\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"amet\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"consectetur\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"adipiscing\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"elit\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,003\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Integer\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nec\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"odio\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Praesent\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,003\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"libero\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Sed\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"cursus\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ante\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,004\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"dapibus\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"diam\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Sed\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nisi\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,005\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Nulla\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"quis\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sem\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"at\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,006\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nibh\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"elementum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"imperdiet\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Duis\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,007\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sagittis\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ipsum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Praesent\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"mauris\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,008\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Fusce\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nec\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"tellus\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sed\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,009\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"augue\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"semper\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"porta\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Mauris\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,010\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"massa\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Vestibulum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"lacinia\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"arcu\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,011\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"eget\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nulla\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Class\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"aptent\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,012\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"taciti\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sociosqu\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ad\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"litora\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,013\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"torquent\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"per\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"conubia\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nostra\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,014\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"per\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"inceptos\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"himenaeos\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Curabitur\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,015\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sodales\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ligula\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"in\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"libero\",\"style\":{},\"attributes\":{},\"children\":[]}]}]}]}]}]}]}]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"anonymous\"},\"children\":[]},{\"name\":\"script\",\"text\":\"window.jQuery||document.write('<scriptsrc=\\\"../../assets/js/vendor/jquery-slim.min.js\\\"><\\\\/script>')\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/popper.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../dist/js/bootstrap.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"https://unpkg.com/feather-icons/dist/feather.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"feather.replace()\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"https://cdn.jsdelivr.net/npm/chart.js@2.7.1/dist/Chart.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"varctx=document.getElementById(\\\"myChart\\\");\\nvarmyChart=newChart(ctx,{\\ntype:'line',\\ndata:{\\nlabels:[\\\"Sunday\\\",\\\"Monday\\\",\\\"Tuesday\\\",\\\"Wednesday\\\",\\\"Thursday\\\",\\\"Friday\\\",\\\"Saturday\\\"],\\ndatasets:[{\\ndata:[15339,21345,18483,24003,23489,24092,12034],\\nlineTension:0,\\nbackgroundColor:'transparent',\\nborderColor:'#007bff',\\nborderWidth:4,\\npointBackgroundColor:'#007bff'\\n}]\\n},\\noptions:{\\nscales:{\\nyAxes:[{\\nticks:{\\nbeginAtZero:false\\n}\\n}]\\n},\\nlegend:{\\ndisplay:false,\\n}\\n}\\n});\",\"style\":{},\"attributes\":{}," +
                                  "\"children\":[]}]}"
                },
                new WidgetCodeSnippet
                {
                    WidgetID = 18,
                    TargetFrameworkID = 2,
                    LayoutID = 2,
                    CodeSnippet = "{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"site-headersticky-toppy-1\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"containerd-flexflex-columnflex-md-rowjustify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":null,\"style\":{\"bootstrap\":\"py-2\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"d-blockmx-auto\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17.94\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"8\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"2.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"16\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21.94\"},\"children\":[]}]}]},{\"name\":\"a\",\"text\":\"Tour\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Product\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Features\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Enterprise\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Support\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Cart\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"position-relativeoverflow-hiddenp-3p-md-5m-md-3text-centerbg-light\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-5p-lg-5mx-automy-5\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Punnyheadline\",\"style\":{\"bootstrap\":\"display-4font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheadingtoboot.JumpstartyourmarketingeffortswiththisexamplebasedonApple'smarketingpages.\",\"style\":{\"bootstrap\":\"leadfont-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Comingsoon\",\"style\":{\"bootstrap\":\"btnbtn-outline-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"product-devicebox-shadowd-noned-md-block\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"product-deviceproduct-device-2box-shadowd-noned-md-block\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-darkmr-md-3pt-3px-3pt-md-5px-md-5text-centertext-whiteoverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-darkbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-darkbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-primarymr-md-3pt-3px-3pt-md-5px-md-5text-centertext-whiteoverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"footer\",\"text\":null,\"style\":{\"bootstrap\":\"containerpy-5\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-12col-md\"},\"attributes\":{},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"d-blockmb-2\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17.94\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"8\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"2.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"16\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21.94\"},\"children\":[]}]},{\"name\":\"small\",\"text\":\"©2017-2018\",\"style\":{\"bootstrap\":\"d-blockmb-3text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Features\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Coolstuff\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Randomfeature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Teamfeature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Stufffordevelopers\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Anotherone\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Lasttime\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resourcename\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Anotherresource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Finalresource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Business\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Education\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Government\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Gaming\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"About\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Team\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Locations\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Privacy\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Terms\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]}]}]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"anonymous\"},\"children\":[]},{\"name\":\"script\",\"text\":\"window.jQuery||document.write('<scriptsrc=\\\"../../assets/js/vendor/jquery-slim.min.js\\\"><\\\\/script>')\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/popper.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../dist/js/bootstrap.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/holder.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"Holder.addTheme('thumb',{\\nbg:'#55595c',\\nfg:'#eceeef'," +
                                  "\\ntext:'Thumbnail'\\n});\",\"style\":{},\"attributes\":{},\"children\":[]}]}"
                }
                , new WidgetCodeSnippet
                {
                    WidgetID = 19,
                    TargetFrameworkID = 2,
                    LayoutID = 3,
                    CodeSnippet = "{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"container\"},\"attributes\":{},\"children\":[{\"name\":\"header\",\"text\":null,\"style\":{\"bootstrap\":\"blog-headerpy-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"rowflex-nowrapjustify-content-betweenalign-items-center\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-4pt-1\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Subscribe\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-4text-center\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Large\",\"style\":{\"bootstrap\":\"blog-header-logotext-dark\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-4d-flexjustify-content-endalign-items-center\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":null,\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"mx-3\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"7.5\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"15.8\"},\"children\":[]}]}]},{\"name\":\"a\",\"text\":\"Signup\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"nav-scrollerpy-1mb-2\"},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"navd-flexjustify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"World\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"U.S.\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Technology\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Design\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Culture\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Business\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Politics\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Opinion\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Science\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Health\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Style\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Travel\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"jumbotronp-3p-md-5text-whiteroundedbg-dark\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6px-0\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Titleofalongerfeaturedblogpost\",\"style\":{\"bootstrap\":\"display-4font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Multiplelinesoftextthatformthelede,informingnewreadersquicklyandefficientlyaboutwhat'smostinterestinginthispost'scontents.\",\"style\":{\"bootstrap\":\"leadmy-3\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":null,\"style\":{\"bootstrap\":\"leadmb-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Continuereading...\",\"style\":{\"bootstrap\":\"text-whitefont-weight-bold\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"rowmb-2\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"cardflex-md-rowmb-4box-shadowh-md-250\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"card-bodyd-flexflex-columnalign-items-start\"},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"World\",\"style\":{\"bootstrap\":\"d-inline-blockmb-2text-primary\"},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":null,\"style\":{\"bootstrap\":\"mb-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Featuredpost\",\"style\":{\"bootstrap\":\"text-dark\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Nov12\",\"style\":{\"bootstrap\":\"mb-1text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Thisisawidercardwithsupportingtextbelowasanaturallead-intoadditionalcontent.\",\"style\":{\"bootstrap\":\"card-textmb-auto\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Continuereading\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"card-img-rightflex-autod-noned-md-block\"},\"attributes\":{\"name\":\"true\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"cardflex-md-rowmb-4box-shadowh-md-250\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"card-bodyd-flexflex-columnalign-items-start\"},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"Design\",\"style\":{\"bootstrap\":\"d-inline-blockmb-2text-success\"},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":null,\"style\":{\"bootstrap\":\"mb-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Posttitle\",\"style\":{\"bootstrap\":\"text-dark\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Nov11\",\"style\":{\"bootstrap\":\"mb-1text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Thisisawidercardwithsupportingtextbelowasanaturallead-intoadditionalcontent.\",\"style\":{\"bootstrap\":\"card-textmb-auto\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Continuereading\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"card-img-rightflex-autod-noned-md-block\"},\"attributes\":{\"name\":\"width:200px;height:250px;\"},\"children\":[]}]}]}]}]},{\"name\":\"main\",\"text\":null,\"style\":{\"bootstrap\":\"container\"},\"attributes\":{\"name\":\"main\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-8blog-main\"},\"attributes\":{},\"children\":[{\"name\":\"h3\",\"text\":\"FromtheFirehose\",\"style\":{\"bootstrap\":\"pb-3mb-4font-italicborder-bottom\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"blog-post\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Sampleblogpost\",\"style\":{\"bootstrap\":\"blog-post-title\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"January1,2014by\",\"style\":{\"bootstrap\":\"blog-post-meta\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Mark\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Thisblogpostshowsafewdifferenttypesofcontentthat'ssupportedandstyledwithBootstrap.Basictypography,images,andcodeareallsupported.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"hr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnis,nasceturridiculusmus.Aeneaneuleoquam.Pellentesqueornaresemlaciniaquamvenenatisvestibulum.Sedposuereconsecteturestatlobortis.Crasmattisconsecteturpurussitametfermentum.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"disparturientmontes\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"blockquote\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"Curabiturblandittempusporttitor.ornareveleuleo.Nullamiddoloridnibhultriciesvehiculautidelit.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"Nullamquisrisusegeturnamollis\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"h2\",\"text\":\"Heading\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Vivamussagittislacusvelauguelaoreetrutrumfaucibusdolorauctor.Duismollis,estnoncommodoluctus,nisieratporttitorligula,egetlaciniaodiosemnecelit.Morbileorisus,portaacconsecteturac,vestibulumateros.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":\"Sub-heading\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"pre\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"code\",\"text\":\"Examplecodeblock\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Aeneanlaciniabibendumnullasedconsectetur.Etiamportasemmalesuadamagnamolliseuismod.Fuscedapibus,tellusaccursuscommodo,tortormauriscondimentumnibh,utfermentummassa.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":\"Sub-heading\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.Aeneanlaciniabibendumnullasedconsectetur.Etiamportasemmalesuadamagnamolliseuismod.Fuscedapibus,tellusaccursuscommodo,tortormauriscondimentumnibh,utfermentummassajustositametrisus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"Praesentcommodocursusmagna,velscelerisquenislconsecteturet.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Donecidelitnonmiportagravidaategetmetus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Donecullamcorpernullanonmetusauctorfringilla.Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ol\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"Vestibulumidligulaportafeliseuismodsemper.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Maecenasseddiamegetrisusvariusblanditsitametnonmagna.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Crasmattisconsecteturpurussitametfermentum.Sedposuereconsecteturestatlobortis.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"blog-post\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherblogpost\",\"style\":{\"bootstrap\":\"blog-post-title\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"December23,2013by\",\"style\":{\"bootstrap\":\"blog-post-meta\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Jacob\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnis,nasceturridiculusmus.Aeneaneuleoquam.Pellentesqueornaresemlaciniaquamvenenatisvestibulum.Sedposuereconsecteturestatlobortis.Crasmattisconsecteturpurussitametfermentum.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"disparturientmontes\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"blockquote\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"Curabiturblandittempusporttitor.ornareveleuleo.Nullamiddoloridnibhultriciesvehiculautidelit.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"Nullamquisrisusegeturnamollis\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Vivamussagittislacusvelauguelaoreetrutrumfaucibusdolorauctor.Duismollis,estnoncommodoluctus,nisieratporttitorligula,egetlaciniaodiosemnecelit.Morbileorisus,portaacconsecteturac,vestibulumateros.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"blog-post\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Newfeature\",\"style\":{\"bootstrap\":\"blog-post-title\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"December14,2013by\",\"style\":{\"bootstrap\":\"blog-post-meta\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Chris\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.Aeneanlaciniabibendumnullasedconsectetur.Etiamportasemmalesuadamagnamolliseuismod.Fuscedapibus,tellusaccursuscommodo,tortormauriscondimentumnibh,utfermentummassajustositametrisus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"Praesentcommodocursusmagna,velscelerisquenislconsecteturet.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Donecidelitnonmiportagravidaategetmetus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Donecullamcorpernullanonmetusauctorfringilla.Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"blog-pagination\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Older\",\"style\":{\"bootstrap\":\"btnbtn-outline-primary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Newer\",\"style\":{\"bootstrap\":\"btnbtn-outline-secondarydisabled\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"aside\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-4blog-sidebar\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"p-3mb-3bg-lightrounded\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"About\",\"style\":{\"bootstrap\":\"font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{\"bootstrap\":\"mb-0\"},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"Archives\",\"style\":{\"bootstrap\":\"font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"ol\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledmb-0\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"March2014\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"February2014\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"January2014\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"December2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"November2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"October2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"September2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"August2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"July2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"June2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"May2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"April2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"Elsewhere\",\"style\":{\"bootstrap\":\"font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"ol\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyled\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"GitHub\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Twitter\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Facebook\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]}]}]}]},{\"name\":\"footer\",\"text\":null,\"style\":{\"bootstrap\":\"blog-footer\"},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"Blogtemplatebuiltforby.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Bootstrap\",\"style\":{},\"attributes\":{\"name\":\"https://getbootstrap.com/\"},\"children\":[]},{\"name\":\"a\",\"text\":\"@mdo\",\"style\":{},\"attributes\":{\"name\":\"https://twitter.com/mdo\"},\"children\":[]}]},{\"name\":\"p\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Backtotop\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"anonymous\"},\"children\":[]},{\"name\":\"script\",\"text\":\"window.jQuery||document.write('<scriptsrc=\\\"../../assets/js/vendor/jquery-slim.min.js\\\"><\\\\/script>')\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/popper.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../dist/js/bootstrap.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/holder.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"Holder.addTheme('thumb',{\\nbg:'#55595c'," +
                                  "\\nfg:'#eceeef',\\ntext:'Thumbnail'\\n});\",\"style\":{},\"attributes\":{},\"children\":[]}]}"
                }
            );

            modelBuilder.Entity<Property>()
                   .HasData(
                       new Property
                       {
                           Id = 1,
                           PropertyName = "fontColor",
                           IsOnlyNested = true,
                           Description = "color of written text"

                       },
                       //new Property
                       //{
                       //    Id = 2,
                       //    PropertyName = "border",
                       //    IsOnlyNested = false,
                       //    Description = "outside border of certain html element"
                       //},
                       new Property
                       {
                           Id = 2,
                           PropertyName = "borderColor",
                           IsOnlyNested = true,
                           Description = "color of html elements border"
                       },
                       new Property
                       {
                           Id = 3,
                           PropertyName = "backgroundColor",
                           IsOnlyNested = true,
                           Description = "background color of html element"
                       },
                       new Property
                       {
                           Id = 4,
                           PropertyName = "fontSize",
                           IsOnlyNested = true,
                           Description = "no description"
                       },
                       new Property
                       {
                           Id = 5,
                           PropertyName = "textAlign",
                           IsOnlyNested = true,
                           Description = "text-align description"
                       },
                       new Property
                       {
                           Id = 6,
                           PropertyName = "border",
                           IsOnlyNested = false,
                           Description = "outside border of certain html element"

                       },
                       new Property
                       {
                           Id = 7,
                           PropertyName = "borderRadius",
                           IsOnlyNested = true,
                           Description = "borderRadius-Desc."
                       },
                       new Property
                       {
                           Id = 8,
                           PropertyName = "margin",
                           IsOnlyNested = false,
                           Description = "margin-desc."
                       },
                       new Property
                       {
                           Id = 9,
                           PropertyName = "padding",
                           IsOnlyNested = false,
                           Description = "padding-desc."
                       },
                       new Property
                       {
                           Id = 10,
                           PropertyName = "display",
                           IsOnlyNested = false,
                           Description = "display-desc"
                       },
                       new Property
                       {
                           Id = 11,
                           PropertyName = "boxShadow",
                           IsOnlyNested = false,
                           Description = "boxShadow-desc"
                       },
                       new Property
                       {
                           Id = 12,
                           PropertyName = "width",
                           IsOnlyNested = false,
                           Description = "width-desc"
                       },
                       new Property
                       {
                           Id = 13,
                           PropertyName = "height",
                           IsOnlyNested = false,
                           Description = "height-desc"
                       }
                   );

            modelBuilder.Entity<WidgetProperty>()
                .HasData
                (
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 1,
                        DefaultValue = "primary"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 2,
                        DefaultValue = "primary"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 3,
                        DefaultValue = "primary"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 4,
                        DefaultValue = "3"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 5,
                        DefaultValue = "text-left"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 6,
                        DefaultValue = "border border-1"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 7,
                        DefaultValue = "rounded-1"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 8,
                        DefaultValue = "m-1"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 9,
                        DefaultValue = "p-0"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 10,
                        DefaultValue = "d-block"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 11,
                        DefaultValue = "shadow-none"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 12,
                        DefaultValue = "100"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 1,
                        PropertyID = 13,
                        DefaultValue = "100"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 2,
                        PropertyID = 1,
                        DefaultValue = "primary"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 2,
                        PropertyID = 2,
                        DefaultValue = "primary"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 2,
                        PropertyID = 3,
                        DefaultValue = "primary"
                    },
                    new WidgetProperty
                    {
                        WidgetID = 2,
                        PropertyID = 4,
                        DefaultValue = "3"
                    }
                );
            modelBuilder.Entity<Unit>()
                .HasData(
                    new Unit
                    {
                        Id = 1,
                        UnitName = "em",
                        isDefault = false,
                    },
                    new Unit
                    {
                        Id = 2,
                        UnitName = "%",
                        isDefault = false
                    },
                    new Unit
                    {
                        Id = 3,
                        UnitName = "px",
                        isDefault = true,
                    },
                    new Unit
                    {
                        Id = 4,
                        UnitName = "rem",
                        isDefault = false
                    }
                );
            modelBuilder.Entity<PropertyUnit>()
                .HasData(
                    new PropertyUnit
                    {
                        PropertyID = 12,
                        UnitID = 1,
                        isDefault = false,
                    },
                    new PropertyUnit
                    {
                        PropertyID = 12,
                        UnitID = 2,
                        isDefault = false,
                    },
                    new PropertyUnit
                    {
                        PropertyID = 12,
                        UnitID = 3,
                        isDefault = true,
                    },
                    new PropertyUnit
                    {
                        PropertyID = 12,
                        UnitID = 4,
                        isDefault = false,
                    },
                    new PropertyUnit
                    {
                        PropertyID = 13,
                        UnitID = 1,
                        isDefault = false,
                    },
                    new PropertyUnit
                    {
                        PropertyID = 13,
                        UnitID = 2,
                        isDefault = false,
                    },
                    new PropertyUnit
                    {
                        PropertyID = 13,
                        UnitID = 3,
                        isDefault = true,
                    },
                    new PropertyUnit
                    {
                        PropertyID = 13,
                        UnitID = 4,
                        isDefault = false,
                    }
                );
            modelBuilder.Entity<PropertyValue>()
                .HasData(
                    /////////////////////////FontColor Property////////////////////
                    new PropertyValue
                    {
                        Id = 1,
                        PropertyID = 1,
                        Value = "text-primary",
                        IsDefault = true
                    },
                    new PropertyValue
                    {
                        Id = 2,
                        PropertyID = 1,
                        Value = "text-danger",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 3,
                        PropertyID = 1,
                        Value = "text-success",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 4,
                        PropertyID = 1,
                        Value = "text-warning",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 5,
                        PropertyID = 1,
                        Value = "text-secondary",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 6,
                        PropertyID = 1,
                        Value = "text-info",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 7,
                        PropertyID = 1,
                        Value = "text-light",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 8,
                        PropertyID = 1,
                        Value = "text-dark",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 9,
                        PropertyID = 1,
                        Value = "text-body",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 10,
                        PropertyID = 1,
                        Value = "text-transparent",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 11,
                        PropertyID = 1,
                        Value = "text-muted",
                        IsDefault = false
                    },
                    /////////////////////////borderColor Property////////////////////
                    new PropertyValue
                    {
                        Id = 12,
                        PropertyID = 2,
                        Value = "border-success",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 13,
                        PropertyID = 2,
                        Value = "border-light",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 14,
                        PropertyID = 2,
                        Value = "border-white",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 15,
                        PropertyID = 2,
                        Value = "border-primary",
                        IsDefault = true
                    },
                    new PropertyValue
                    {
                        Id = 16,
                        PropertyID = 2,
                        Value = "border-danger",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 17,
                        PropertyID = 2,
                        Value = "border-muted",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 18,
                        PropertyID = 2,
                        Value = "border-warning",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 19,
                        PropertyID = 2,
                        Value = "border-secondary",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 20,
                        PropertyID = 2,
                        Value = "border-info",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 21,
                        PropertyID = 2,
                        Value = "border-dark",
                        IsDefault = false
                    },
                    /////////////////////////backgroundColor Property////////////////////
                    new PropertyValue
                    {
                        Id = 22,
                        PropertyID = 3,
                        Value = "bg-dark",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 23,
                        PropertyID = 3,
                        Value = "bg-body",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 24,
                        PropertyID = 3,
                        Value = "bg-transparent",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 25,
                        PropertyID = 3,
                        Value = "bg-danger",
                        IsDefault = false,
                    },
                    new PropertyValue
                    {
                        Id = 26,
                        PropertyID = 3,
                        Value = "bg-secondary",
                        IsDefault = false,
                    },
                    new PropertyValue
                    {
                        Id = 27,
                        PropertyID = 3,
                        Value = "bg-primary",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 28,
                        PropertyID = 3,
                        Value = "bg-success",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 29,
                        PropertyID = 3,
                        Value = "bg-warning",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 30,
                        PropertyID = 3,
                        Value = "bg-info",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 31,
                        PropertyID = 3,
                        Value = "bg-light",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 32,
                        PropertyID = 3,
                        Value = "bg-white",
                        IsDefault = false
                    },
                    /////////////////////////FontSize Property////////////////////
                    new PropertyValue
                    {
                        Id = 33,
                        PropertyID = 4,
                        Value = "display-1",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 34,
                        PropertyID = 4,
                        Value = "display-2",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 35,
                        PropertyID = 4,
                        Value = "display-3",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 36,
                        PropertyID = 4,
                        Value = "h1",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 37,
                        PropertyID = 4,
                        Value = "h2",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 38,
                        PropertyID = 4,
                        Value = "h3",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 39,
                        PropertyID = 4,
                        Value = "h4",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 40,
                        PropertyID = 4,
                        Value = "h5",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 41,
                        PropertyID = 4,
                        Value = "h6",
                        IsDefault = false
                    },
                    /////////////////////////text-align Property////////////////////
                    new PropertyValue
                    {
                        Id = 42,
                        PropertyID = 5,
                        Value = "text-left",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 43,
                        PropertyID = 5,
                        Value = "text-center",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 44,
                        PropertyID = 5,
                        Value = "text-right",
                        IsDefault = false
                    },
                    /////////////////////////border Property////////////////////
                    new PropertyValue
                    {
                        Id = 45,
                        PropertyID = 6,
                        Value = "border border-1",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 46,
                        PropertyID = 6,
                        Value = "border border-2",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 47,
                        PropertyID = 6,
                        Value = "border border-3",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 48,
                        PropertyID = 6,
                        Value = "border border-4",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 49,
                        PropertyID = 6,
                        Value = "border border-5",
                        IsDefault = false
                    }
                    /////////////////////////borderRadius Property///////////////////
                    ,
                    new PropertyValue
                    {
                        Id = 50,
                        PropertyID = 7,
                        Value = "rounded-0",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 51,
                        PropertyID = 7,
                        Value = "rounded-1",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 52,
                        PropertyID = 7,
                        Value = "rounded-2",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 53,
                        PropertyID = 7,
                        Value = "rounded-3",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 54,
                        PropertyID = 7,
                        Value = "rounded-4",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 55,
                        PropertyID = 7,
                        Value = "rounded-5",
                        IsDefault = false
                    }
                    ,
                    /////////////// margin-property-value
                    new PropertyValue
                    {
                        Id = 56,
                        PropertyID = 8,
                        Value = "m-0",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 57,
                        PropertyID = 8,
                        Value = "m-1",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 58,
                        PropertyID = 8,
                        Value = "m-2",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 59,
                        PropertyID = 8,
                        Value = "m-3",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 60,
                        PropertyID = 8,
                        Value = "m-4",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 61,
                        PropertyID = 8,
                        Value = "m-5",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 62,
                        PropertyID = 8,
                        Value = "m-auto",
                        IsDefault = false
                    }
                    ,
                    ////////////////padding////////////////
                    new PropertyValue
                    {
                        Id = 63,
                        PropertyID = 9,
                        Value = "p-0",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 64,
                        PropertyID = 9,
                        Value = "p-1",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 65,
                        PropertyID = 9,
                        Value = "p-2",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 66,
                        PropertyID = 9,
                        Value = "p-3",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 67,
                        PropertyID = 9,
                        Value = "p-4",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 68,
                        PropertyID = 9,
                        Value = "p-5",
                        IsDefault = false
                    }
                    ,
                    /////////////display////////////////
                    new PropertyValue
                    {
                        Id = 69,
                        PropertyID = 10,
                        Value = "d-block",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 70,
                        PropertyID = 10,
                        Value = "d-inline",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 71,
                        PropertyID = 10,
                        Value = "d-inline-block",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 72,
                        PropertyID = 10,
                        Value = "d-flex",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 73,
                        PropertyID = 10,
                        Value = "d-inline-flex",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 74,
                        PropertyID = 11,
                        Value = "shadow",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 75,
                        PropertyID = 11,
                        Value = "shadow-none",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 86,
                        PropertyID = 11,
                        Value = "",
                        IsDefault = false
                    },
                    new PropertyValue
                    {
                        Id = 87,
                        PropertyID = 10,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 88,
                        PropertyID = 9,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 89,
                        PropertyID = 8,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 90,
                        PropertyID = 7,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 91,
                        PropertyID = 6,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 92,
                        PropertyID = 5,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 93,
                        PropertyID = 4,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 94,
                        PropertyID = 3,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 95,
                        PropertyID = 2,
                        Value = "",
                        IsDefault = false
                    }
                    ,
                    new PropertyValue
                    {
                        Id = 96,
                        PropertyID = 1,
                        Value = "",
                        IsDefault = false
                    }
                );
            modelBuilder.Entity<Attribute>()
                .HasData(
                new Attribute
                {
                    Id = 1,
                    AttributeName = "src",
                    Description = "The src attribute specifies the path to the image to be displayed"
                },
                new Attribute
                {
                    Id = 2,
                    AttributeName = "onClick",
                    Description = "The onclick event occurs when the user clicks on an element."
                }
                );
            modelBuilder.Entity<WidgetAttribute>()
                .HasData(
                new WidgetAttribute
                {
                    WidgetId = 8,
                    AttributeId = 1
                },
                new WidgetAttribute
                {
                    WidgetId = 10,
                    AttributeId = 2
                }
                );


        }
    }
}
