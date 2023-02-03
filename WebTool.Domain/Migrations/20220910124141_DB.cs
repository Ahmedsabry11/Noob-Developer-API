using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainLayer.Migrations
{
    public partial class DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppType",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Layout",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Design = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconPathLayout = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Layout", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentPropertyID = table.Column<int>(type: "int", nullable: true),
                    IsOnlyNested = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.ID);
                    table.ForeignKey(
                        name: "ForeignKey_Property_ParentPropery",
                        column: x => x.ParentPropertyID,
                        principalTable: "Property",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TargetFramework",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FrameworkName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetFramework", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    isDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Widget",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentWidgetID = table.Column<int>(type: "int", nullable: true),
                    RelatedAppTypeID = table.Column<int>(type: "int", nullable: false),
                    IsOnlyNested = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widget", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Widget_AppType_RelatedAppTypeID",
                        column: x => x.RelatedAppTypeID,
                        principalTable: "AppType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Widget_Widget_ParentWidgetID",
                        column: x => x.ParentWidgetID,
                        principalTable: "Widget",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValue",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValue", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyValue_Property_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppTypeID = table.Column<int>(type: "int", nullable: false),
                    TargetFrameworkID = table.Column<int>(type: "int", nullable: false),
                    GeneratedAppPath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Widgets = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Project_AppType_AppTypeID",
                        column: x => x.AppTypeID,
                        principalTable: "AppType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Project_TargetFramework_TargetFrameworkID",
                        column: x => x.TargetFrameworkID,
                        principalTable: "TargetFramework",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyUnit",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    isDefault = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyUnit", x => new { x.PropertyID, x.UnitID });
                    table.ForeignKey(
                        name: "ForeignKey_PropertyUnit_Property",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_PropertyUnit_Unit",
                        column: x => x.UnitID,
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetAttribute",
                columns: table => new
                {
                    WidgetId = table.Column<int>(type: "int", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetAttribute", x => new { x.WidgetId, x.AttributeId });
                    table.ForeignKey(
                        name: "ForeignKey_WidgetAttribute_Attribute",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_WidgetAttribute_Widget",
                        column: x => x.WidgetId,
                        principalTable: "Widget",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetCodeSnippet",
                columns: table => new
                {
                    WidgetID = table.Column<int>(type: "int", nullable: false),
                    TargetFrameworkID = table.Column<int>(type: "int", nullable: false),
                    LayoutID = table.Column<int>(type: "int", nullable: false),
                    CodeSnippet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetCodeSnippet", x => new { x.WidgetID, x.TargetFrameworkID, x.LayoutID });
                    table.ForeignKey(
                        name: "ForeignKey_WidgetCodeSnippet_Layout",
                        column: x => x.LayoutID,
                        principalTable: "Layout",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_WidgetCodeSnippet_TargetFramework",
                        column: x => x.TargetFrameworkID,
                        principalTable: "TargetFramework",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_WidgetCodeSnippet_Widget",
                        column: x => x.WidgetID,
                        principalTable: "Widget",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WidgetProperty",
                columns: table => new
                {
                    WidgetID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WidgetProperty", x => new { x.WidgetID, x.PropertyID });
                    table.ForeignKey(
                        name: "ForeignKey_WidgetProperty_Property",
                        column: x => x.PropertyID,
                        principalTable: "Property",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_WidgetProperty_Widget",
                        column: x => x.WidgetID,
                        principalTable: "Widget",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppType",
                columns: new[] { "ID", "Type" },
                values: new object[,]
                {
                    { 1, "WEB" },
                    { 2, "MOBILE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "e36d2d29-1009-43f1-8c72-4500e3428e96", null, false, false, null, null, null, null, null, false, "1594be92-62e7-4148-9b5e-c9a45ebcff88", false, null });

            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "ID", "AttributeName", "Description" },
                values: new object[,]
                {
                    { 1, "src", "The src attribute specifies the path to the image to be displayed" },
                    { 2, "onClick", "The onclick event occurs when the user clicks on an element." }
                });

            migrationBuilder.InsertData(
                table: "Layout",
                columns: new[] { "ID", "Description", "Design", "IconPathLayout" },
                values: new object[,]
                {
                    { 1, "Simple widgets with few basic styling like div, span, etc...", "Basic", "LayoutBasic.png" },
                    { 2, "Complex widgets with amazing styling like carousel, button group, etc...", "Layout", "LayoutLayout.png" },
                    { 3, "A complete page with styling ready to use", "Page", "LayoutPage.png" }
                });

            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "ID", "Description", "IsOnlyNested", "ParentPropertyID", "PropertyName" },
                values: new object[,]
                {
                    { 1, "color of written text", true, null, "fontColor" },
                    { 2, "color of html elements border", true, null, "borderColor" },
                    { 3, "background color of html element", true, null, "backgroundColor" },
                    { 4, "no description", true, null, "fontSize" },
                    { 5, "text-align description", true, null, "textAlign" },
                    { 6, "outside border of certain html element", false, null, "border" },
                    { 7, "borderRadius-Desc.", true, null, "borderRadius" },
                    { 8, "margin-desc.", false, null, "margin" },
                    { 9, "padding-desc.", false, null, "padding" },
                    { 10, "display-desc", false, null, "display" },
                    { 11, "boxShadow-desc", false, null, "boxShadow" },
                    { 12, "width-desc", false, null, "width" },
                    { 13, "height-desc", false, null, "height" }
                });

            migrationBuilder.InsertData(
                table: "TargetFramework",
                columns: new[] { "ID", "FrameworkName" },
                values: new object[,]
                {
                    { 1, "Bootstrap" },
                    { 2, "html" }
                });

            migrationBuilder.InsertData(
                table: "Unit",
                columns: new[] { "ID", "UnitName", "isDefault" },
                values: new object[,]
                {
                    { 1, "em", false },
                    { 2, "%", false },
                    { 3, "px", true },
                    { 4, "rem", false }
                });

            migrationBuilder.InsertData(
                table: "PropertyUnit",
                columns: new[] { "PropertyID", "UnitID", "isDefault" },
                values: new object[,]
                {
                    { 12, 1, false },
                    { 12, 2, false },
                    { 12, 3, true },
                    { 12, 4, false },
                    { 13, 1, false },
                    { 13, 2, false },
                    { 13, 3, true },
                    { 13, 4, false }
                });

            migrationBuilder.InsertData(
                table: "PropertyValue",
                columns: new[] { "ID", "IsDefault", "PropertyID", "Value" },
                values: new object[,]
                {
                    { 1, true, 1, "text-primary" },
                    { 2, false, 1, "text-danger" },
                    { 3, false, 1, "text-success" },
                    { 4, false, 1, "text-warning" },
                    { 5, false, 1, "text-secondary" },
                    { 6, false, 1, "text-info" },
                    { 7, false, 1, "text-light" },
                    { 8, false, 1, "text-dark" },
                    { 9, false, 1, "text-body" },
                    { 10, false, 1, "text-transparent" },
                    { 11, false, 1, "text-muted" },
                    { 12, false, 2, "border-success" },
                    { 13, false, 2, "border-light" },
                    { 14, false, 2, "border-white" },
                    { 15, true, 2, "border-primary" },
                    { 16, false, 2, "border-danger" },
                    { 17, false, 2, "border-muted" },
                    { 18, false, 2, "border-warning" },
                    { 19, false, 2, "border-secondary" },
                    { 20, false, 2, "border-info" },
                    { 21, false, 2, "border-dark" },
                    { 22, false, 3, "bg-dark" },
                    { 23, false, 3, "bg-body" },
                    { 24, false, 3, "bg-transparent" },
                    { 25, false, 3, "bg-danger" },
                    { 26, false, 3, "bg-secondary" },
                    { 27, false, 3, "bg-primary" },
                    { 28, false, 3, "bg-success" },
                    { 29, false, 3, "bg-warning" },
                    { 30, false, 3, "bg-info" },
                    { 31, false, 3, "bg-light" },
                    { 32, false, 3, "bg-white" },
                    { 33, false, 4, "display-1" },
                    { 34, false, 4, "display-2" }
                });

            migrationBuilder.InsertData(
                table: "PropertyValue",
                columns: new[] { "ID", "IsDefault", "PropertyID", "Value" },
                values: new object[,]
                {
                    { 35, false, 4, "display-3" },
                    { 36, false, 4, "h1" },
                    { 37, false, 4, "h2" },
                    { 38, false, 4, "h3" },
                    { 39, false, 4, "h4" },
                    { 40, false, 4, "h5" },
                    { 41, false, 4, "h6" },
                    { 42, false, 5, "text-left" },
                    { 43, false, 5, "text-center" },
                    { 44, false, 5, "text-right" },
                    { 45, false, 6, "border border-1" },
                    { 46, false, 6, "border border-2" },
                    { 47, false, 6, "border border-3" },
                    { 48, false, 6, "border border-4" },
                    { 49, false, 6, "border border-5" },
                    { 50, false, 7, "rounded-0" },
                    { 51, false, 7, "rounded-1" },
                    { 52, false, 7, "rounded-2" },
                    { 53, false, 7, "rounded-3" },
                    { 54, false, 7, "rounded-4" },
                    { 55, false, 7, "rounded-5" },
                    { 56, false, 8, "m-0" },
                    { 57, false, 8, "m-1" },
                    { 58, false, 8, "m-2" },
                    { 59, false, 8, "m-3" },
                    { 60, false, 8, "m-4" },
                    { 61, false, 8, "m-5" },
                    { 62, false, 8, "m-auto" },
                    { 63, false, 9, "p-0" },
                    { 64, false, 9, "p-1" },
                    { 65, false, 9, "p-2" },
                    { 66, false, 9, "p-3" },
                    { 67, false, 9, "p-4" },
                    { 68, false, 9, "p-5" },
                    { 69, false, 10, "d-block" },
                    { 70, false, 10, "d-inline" },
                    { 71, false, 10, "d-inline-block" },
                    { 72, false, 10, "d-flex" },
                    { 73, false, 10, "d-inline-flex" },
                    { 74, false, 11, "shadow" },
                    { 75, false, 11, "shadow-none" },
                    { 86, false, 11, "" }
                });

            migrationBuilder.InsertData(
                table: "PropertyValue",
                columns: new[] { "ID", "IsDefault", "PropertyID", "Value" },
                values: new object[,]
                {
                    { 87, false, 10, "" },
                    { 88, false, 9, "" },
                    { 89, false, 8, "" },
                    { 90, false, 7, "" },
                    { 91, false, 6, "" },
                    { 92, false, 5, "" },
                    { 93, false, 4, "" },
                    { 94, false, 3, "" },
                    { 95, false, 2, "" },
                    { 96, false, 1, "" }
                });

            migrationBuilder.InsertData(
                table: "Widget",
                columns: new[] { "ID", "Description", "IconPath", "IsOnlyNested", "ParentWidgetID", "RelatedAppTypeID", "Title" },
                values: new object[,]
                {
                    { 1, "The <div> tag defines a division or a section in an HTML document. The <div> tag is used as a container for HTML elements - which is then styled with CSS or manipulated with JavaScript. The <div> tag is easily styled by using the class or id attribute. Any sort of content can be put inside the <div> tag!", "div.png", false, null, 1, "div" },
                    { 2, "The <span> tag is an inline container used to mark up a part of a text, or a part of a document. The <span> tag is easily styled by CSS or manipulated with JavaScript using the class or id attribute", "span.png", false, null, 1, "span" },
                    { 3, "Section tag defines the section of documents such as chapters, headers, footers or any other sections. The section tag divides the content into section and subsections. The section tag is used when requirements of two headers or footers or any other section of documents needed.", "section.png", false, null, 1, "section" },
                    { 4, "The <header> tag in HTML is used to define the header for a document or a section as it contains the information related to the title and heading of the related content. The <header> element is intended to usually contain the section’s heading (an h1-h6 element or an <hgroup> element), but this is not required. It can also be used to wrap a section’s table of contents, a search form, or any relevant logos. The <header> tag is a new tag in HTML5 and it is a container tag ie., it contains a starting tag, content & the end tag. There can be several <header> elements in one document. This tag cannot be placed within a <footer>, <address> or another <header> element.", "header.png", false, null, 1, "header" },
                    { 5, "The <footer> tag in HTML is used to define a footer of HTML document. This section contains the footer information (author information, copyright information, carriers, etc). The footer tag is used within the body tag. The <footer> tag is new in the HTML5. The footer elements require a start tag as well as an end tag.", "footer.png", false, null, 1, "footer" },
                    { 6, "The < p > HTML element represents a paragraph.Paragraphs are usually represented in visual media as blocks of text separated from adjacent blocks by blank lines and / or first - line indentation.", "p.png", false, null, 1, "p" },
                    { 7, "The <nav> HTML element represents a section of a page whose purpose is to provide navigation links, either within the current document or to other documents. Common examples of navigation sections are menus, tables of contents, and indexes.", "navbar.png", false, null, 1, "navbar" },
                    { 8, "The <img> tag creates a holding space for the referenced image. The <img> tag has two required attributes: src - Specifies the path to the image. alt - Specifies an alternate text for the image, if the image for some reason cannot be displayed.", "image.png", false, null, 1, "img" },
                    { 9, "Carousel is a slide show for cycling through a series of content, built with CSS 3D transforms and a bit of JavaScript. It works with a series of images, text, or custom markup. It also includes support for previous/next controls and indicators.", "carousel.png", false, null, 1, "carousel" },
                    { 10, "The <button> HTML element is an interactive element activated by a user with a mouse, keyboard, finger, voice command, or other assistive technology. Once activated, it then performs a programmable action, such as submitting a form or opening a dialog", "button.png", false, null, 1, "button" },
                    { 11, "The <button> HTML element is an interactive element activated by a user with a mouse, keyboard, finger, voice command, or other assistive technology. Once activated, it then performs a programmable action, such as submitting a form or opening a dialog", "buttongroup.png", false, null, 1, "buttongroup" },
                    { 12, "A website's footer is an area located at the bottom of every page on a website, below the main body content. The term “footer” comes from the print world, in which the “footer” is a consistent design element that is seen across all pages of a document.", "footer_layout.png", false, null, 1, "footer_layout" },
                    { 13, "The h1 should describe the topic of your page and its content. It’s possible that the h1 tag is similar to your title tag. Usually the h1 tag is the title of your post or blog post. Normally, the h1 tag gives the reader an idea of the content of a web page.", "header1.png", false, null, 1, "Header1" },
                    { 14, "A meta description is an HTML element that provides a brief summary of a web page", "page.png", false, null, 1, "Page" },
                    { 15, "A web form, also called an HTML form, is an online page that allows for user input. It is an interactive page that mimics a paper document or form, where users fill out particular fields. Web forms can be rendered in modern browsers using HTML and related web-oriented languages.", "form.png", false, null, 1, "form" },
                    { 16, "A website page which views to users when signing in", "sign-in.png", false, null, 1, "Sign-In" },
                    { 17, "Dashboards are business intelligence (BI) reporting tools that aggregate and display critical metrics and key performance indicators (KPIs) in a single screen, enabling users to monitor and examine business performance at a glance", "dashboard.png", false, null, 1, "Dashboard" },
                    { 18, "A product is the item offered for sale. A product can be a service or an item.It can be physical or in virtual or cyber form. Every product is made at a cost and each is sold at a price", "product.png", false, null, 1, "Product" },
                    { 19, "is a personal online journal that is frequently updated and intended for general public consumption. Blogs are defined by their format: a series of entries posted to a single page in reverse-chronological order", "blog.png", false, null, 1, "Blog" }
                });

            migrationBuilder.InsertData(
                table: "WidgetAttribute",
                columns: new[] { "AttributeId", "WidgetId" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 2, 10 }
                });

            migrationBuilder.InsertData(
                table: "WidgetCodeSnippet",
                columns: new[] { "LayoutID", "TargetFrameworkID", "WidgetID", "CodeSnippet" },
                values: new object[,]
                {
                    { 1, 2, 1, "{ \"name\": \"div\",\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, \"children\": [],\"text\": \"Div (container)\"}" },
                    { 1, 2, 2, "{ \"name\": \"span\",\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, \"children\": [],\"text\": \"Span\"}" },
                    { 1, 2, 3, "{ \"name\": \"section\",\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, \"children\": [],\"text\": \"Section\"}" },
                    { 1, 2, 4, "{ \"name\": \"header\",\"style\": { \"bootstrap\": \"border border-primary text-center\" },\"children\": [],\"text\": \"Header\"}" },
                    { 1, 2, 5, "{ \"name\": \"footer\",\"style\": { \"bootstrap\": \"border border-primary text-center\" },\"children\": [],\"text\": \"Footer\"}" },
                    { 1, 2, 6, "{ \"name\": \"p\",\"style\": { \"border\": \"border border-primary\",\"textAlign\":\"text-center\"}, \"children\": [],\"text\": \"Paragraph\"}" },
                    { 1, 2, 7, "{ \"name\": \"nav\",\"style\": {\"bootstrap\": \"navbar navbar-expand-lg navbar-light bg-light\"},\"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"navbar-brand\"},\"children\": [],\"text\": \"Navbar\"},{\"name\": \"button\",\"style\": {\"bootstrap\": \"navbar-toggler\"},\"children\": [{\"name\": \"span\",\"style\": {\"bootstrap\": \"navbar-toggler-icon\"}, \"children\": [],\"text\": \"\"}],\"text\": \"\"}, {\"name\": \"div\",\"style\": {\"bootstrap\": \"collapse navbar-collapse\"}, \"children\": [{\"name\": \"ul\",\"style\": {\"bootstrap\": \"navbar-nav mr-auto\"}, \"children\": [{\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item active\"},\"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"nav-link\"}, \"children\": [],\"text\": \"Home\"}], \"text\": \"\"}, {\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item\"}, \"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"nav-link\"}, \"children\": [],\"text\": \"Link\"}],\"text\": \"\"},{\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item dropdown\"}, \"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"nav-link dropdown-toggle\"}, \"children\": [],\"text\": \"Dropdown\"},{\"name\": \"div\",\"style\": {\"bootstrap\": \"dropdown-menu\"}, \"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"dropdown-item\"}, \"children\": [],\"text\": \"Action\"},{\"name\": \"a\",\"style\": {\"bootstrap\": \"dropdown-item\"}, \"children\": [],\"text\": \"Another action\"},{\"name\": \"div\",\"style\": {\"bootstrap\": \"dropdown-divider\"}, \"children\": [],\"text\": \"\"},{\"name\": \"a\",\"style\": {\"bootstrap\": \"dropdown-item\"}, \"children\": [],\"text\": \"Something else here\"}], \"text\": \"\"}], \"text\": \"\"},{\"name\": \"li\",\"style\": {\"bootstrap\": \"nav-item\"},\"children\": [{\"name\": \"a\",\"style\": {\"bootstrap\": \"nav-link disabled\"}, \"children\": [],\"text\": \"Disabled\"}],\"text\": \"\"}], \"text\": \"\"}], \"text\": \"\"}], \"text\": \"\"}" },
                    { 1, 2, 8, "{\"name\":\"img\",\"style\":{\"width\":\"100px\",\"height\":\"100px\",\"backgroundColor\":\"red\"},\"children\":[],\"text\":null,\"attributes\":{\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}" },
                    { 2, 2, 9, "{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel slide\"},\"children\":[{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-inner\"},\"children\":[{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-item active\"},\"children\":[{\"name\":\"img\",\"style\":{\"bootstrap\":\"d-block w-100\"},\"children\":[],\"text\":null,\"attributes\":{\"alt\":\"First slide\",\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}],\"text\":\"\",\"attributes\":{}},{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-item\"},\"children\":[{\"name\":\"img\",\"style\":{\"bootstrap\":\"d-block w-100\"},\"children\":[],\"text\":null,\"attributes\":{\"alt\":\"First slide\",\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}],\"text\":\"\",\"attributes\":{}},{\"name\":\"div\",\"style\":{\"bootstrap\":\"carousel-item\"},\"children\":[{\"name\":\"img\",\"style\":{\"bootstrap\":\"d-block w-100\"},\"children\":[],\"text\":null,\"attributes\":{\"alt\":\"First slide\",\"src\":\"https://cdn.pixabay.com/photo/2016/12/27/22/31/converse-1935027_960_720.jpg\"}}],\"text\":\"\",\"attributes\":{}}],\"text\":\"\",\"attributes\":{}},{\"name\":\"a\",\"style\":{\"bootstrap\":\"carousel-control-prev\"},\"children\":[{\"name\":\"span\",\"style\":{\"bootstrap\":\"carousel-control-prev-icon\"},\"children\":[],\"text\":\"\",\"attributes\":{\"aria-hidden\":\"true\"}},{\"name\":\"span\",\"style\":{\"bootstrap\":\"sr-only\"},\"children\":[],\"text\":\"Previous\",\"attributes\":{}}],\"text\":\"\",\"attributes\":{\"href\":\"#carouselExampleControls\",\"role\":\"button\",\"data-slide\":\"prev\"}},{\"name\":\"a\",\"style\":{\"bootstrap\":\"carousel-control-next\"},\"children\":[{\"name\":\"span\",\"style\":{\"bootstrap\":\"carousel-control-next-icon\"},\"children\":[],\"text\":\"\",\"attributes\":{\"aria-hidden\":\"true\"}},{\"name\":\"span\",\"style\":{\"bootstrap\":\"sr-only\"},\"children\":[],\"text\":\"Next\",\"attributes\":{}}],\"text\":\"\",\"attributes\":{\"href\":\"#carouselExampleControls\",\"role\":\"button\",\"data-slide\":\"next\"}}],\"text\":\"\",\"attributes\":{\"id\":\"carouselExampleControls\",\"data-ride\":\"carousel\"}}" },
                    { 1, 2, 10, "{ \"name\": \"button\",\"style\": {\"bootstrap\": \"btn btn-primary\"},\"children\": [],\"text\": \"button\"}" },
                    { 2, 2, 11, "{ \"name\": \"div\",\"style\": {\"bootstrap\": \"btn-group\"},\"children\": [{\"name\": \"button\",\"style\": {\"bootstrap\": \"btn btn-secondary\"},\"children\": [],\"text\": \"button\"},{\"name\": \"button\",\"style\": {\"bootstrap\": \"btn btn-primary\"},\"children\": [],\"text\": \"button\" },{\"name\": \"button\",\"style\": {\"bootstrap\": \"btn btn-success\"},\"children\": [],\"text\": \"button\"}],\"text\": \"\"}" },
                    { 2, 2, 12, "{\"name\":\"footer\",\"text\":\"\",\"style\":{\"bootstrap\":\"text-center text-lg-start bg-light text-muted\"},\"attributes\":{},\"children\":[{\"name\":\"section\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-flex justify-content-center justify-content-lg-between p-4 border-bottom\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-5 d-none d-lg-block\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Get connected with us on social networks:\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-facebook-f\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-twitter\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-google\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-instagram\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-linkedin\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"me-4 text-reset\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fab fa-github\"},\"attributes\":{},\"children\":[]}]}]}]},{\"name\":\"section\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"container text-center text-md-start mt-5\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"row mt-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-3 col-lg-4 col-xl-3 mx-auto mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Company name\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-gem me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Here you can use rows and columns to organize your footer content. Lorem ipsum                                dolor sit amet, consectetur adipisicing elit.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-2 col-lg-2 col-xl-2 mx-auto mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Products\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Angular\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"React\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Vue\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Laravel\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-3 col-lg-2 col-xl-2 mx-auto mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Useful links\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Settings\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Orders\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Help\",\"style\":{\"bootstrap\":\"text-reset\"},\"attributes\":{\"name\":\"#!\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-4 col-lg-3 col-xl-3 mx-auto mb-md-0 mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Contact\",\"style\":{\"bootstrap\":\"text-uppercase fw-bold mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"New York, NY 10012, US\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-home me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"info@example.com\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-envelope me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"+ 01 234 567 88\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-phone me-3\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"+ 01 234 567 89\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"i\",\"text\":\"\",\"style\":{\"bootstrap\":\"fas fa-print me-3\"},\"attributes\":{},\"children\":[]}]}]}]}]}]},{\"name\":\"div\",\"text\":\"© 2021 Copyright:\",\"style\":{\"bootstrap\":\"text-center p-4\"},\"attributes\":{\"name\":\"background-color: rgba(0, 0, 0, 0.05);\"},\"children\":[{\"name\":\"a\",\"text\":\"MDBootstrap.com\",\"style\":{\"bootstrap\":\"text-reset fw-bold\"},\"attributes\":{\"name\":\"https://mdbootstrap.com/\"},\"children\":[]}]}]}" },
                    { 2, 2, 13, "{\"name\":\"header\",\"text\":\"\",\"style\":{\"bootstrap\":\"p-3 bg-dark text-white\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"container\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-flex flex-wrap align-items-center justify-content-center justify-content-lg-start\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-flex align-items-center mb-2 mb-lg-0 text-white text-decoration-none\"},\"attributes\":{\"name\":\"/\"},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"nav col-12 col-lg-auto me-lg-auto mb-2 justify-content-center mb-md-0\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Home\",\"style\":{\"bootstrap\":\"nav-link px-2 text-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Features\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"FAQs\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"About\",\"style\":{\"bootstrap\":\"nav-link px-2 text-white\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"form\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-12 col-lg-auto mb-3 mb-lg-0 me-lg-3\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control form-control-dark\"},\"attributes\":{\"name\":\"Search\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"text-end\"},\"attributes\":{},\"children\":[{\"name\":\"button\",\"text\":\"Login\",\"style\":{\"bootstrap\":\"btn btn-outline-light me-2\"},\"attributes\":{\"name\":\"button\"},\"children\":[]},{\"name\":\"button\",\"text\":\"Sign-up\",\"style\":{\"bootstrap\":\"btn btn-warning\"},\"attributes\":{\"name\":\"button\"},\"children\":[]}]}]}]}]}" },
                    { 3, 2, 14, "{\"name\":\"div\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":\"\",\"style\":{\"bootstrap\":\"site-header sticky-top py-1\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"container d-flex flex-column flex-md-row justify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"\",\"style\":{\"bootstrap\":\"py-2\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Tour\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Product\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Features\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Enterprise\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Support\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Cart\",\"style\":{\"bootstrap\":\"py-2 d-none d-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"position-relative overflow-hidden p-3 p-md-5 m-md-3 text-center bg-light\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-md-5 p-lg-5 mx-auto my-5\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Punny headline\",\"style\":{\"bootstrap\":\"display-4 font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading to boot. Jumpstart your marketing\\n                        efforts with\\n                        this example based on Apple's marketing pages.\",\"style\":{\"bootstrap\":\"lead font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Coming soon\",\"style\":{\"bootstrap\":\"btn btn-outline-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"product-device box-shadow d-none d-md-block\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"product-device product-device-2 box-shadow d-none d-md-block\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-dark mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center text-white overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-dark box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-dark box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-primary mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center text-white overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"d-md-flex flex-md-equal w-100 my-md-3 pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-light mr-md-3 pt-3 px-3 pt-md-5 px-md-5 text-center overflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"my-3 py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Another headline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"And an even wittier subheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"bg-white box-shadow mx-auto\"},\"attributes\":{\"name\":\"width: 80%; height: 300px; border-radius: 21px 21px 0 0;\"},\"children\":[]}]}]},{\"name\":\"footer\",\"text\":\"\",\"style\":{\"bootstrap\":\"container py-5\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-12 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"small\",\"text\":\"© 2017-2018\",\"style\":{\"bootstrap\":\"d-block mb-3 text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Features\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Cool stuff\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Random feature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Team feature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Stuff for developers\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Another one\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Last time\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resource name\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Another resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Final resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Business\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Education\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Government\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Gaming\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":\"\",\"style\":{\"bootstrap\":\"col-6 col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"About\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":\"\",\"style\":{\"bootstrap\":\"list-unstyled text-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Team\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Locations\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Privacy\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":\"\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Terms\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]}]}]}]}" },
                    { 2, 2, 15, "{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"container\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"py-5 text-center\"},\"attributes\":{},\"children\":[{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"d-block mx-auto mb-4\"},\"attributes\":{\"name\":\"72\"},\"children\":[]},{\"name\":\"h2\",\"text\":\"Checkout form\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Below is an example form built entirely with Bootstrap's form controls. Each required form group has a validation state that can be triggered by attempting to submit the form without completing it.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-4 order-md-2 mb-4\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":null,\"style\":{\"bootstrap\":\"d-flex justify-content-between align-items-center mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Your cart\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"span\",\"text\":\"3\",\"style\":{\"bootstrap\":\"badge badge-secondary badge-pill\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-group mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between lh-condensed\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Product name\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"Brief description\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"$12\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between lh-condensed\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Second product\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"Brief description\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"$8\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between lh-condensed\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Third item\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"Brief description\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"$5\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between bg-light\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"text-success\"},\"attributes\":{},\"children\":[{\"name\":\"h6\",\"text\":\"Promo code\",\"style\":{\"bootstrap\":\"my-0\"},\"attributes\":{},\"children\":[]},{\"name\":\"small\",\"text\":\"EXAMPLECODE\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"span\",\"text\":\"-$5\",\"style\":{\"bootstrap\":\"text-success\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-group-item d-flex justify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Total (USD)\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"strong\",\"text\":\"$20\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"form\",\"text\":null,\"style\":{\"bootstrap\":\"card p-2\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"Promo code\"},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group-append\"},\"attributes\":{},\"children\":[{\"name\":\"button\",\"text\":\"Redeem\",\"style\":{\"bootstrap\":\"btn btn-secondary\"},\"attributes\":{\"name\":\"submit\"},\"children\":[]}]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-8 order-md-1\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"Billing address\",\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[]},{\"name\":\"form\",\"text\":null,\"style\":{\"bootstrap\":\"needs-validation\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"First name\",\"style\":{},\"attributes\":{\"name\":\"firstName\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Valid first name is required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Last name\",\"style\":{},\"attributes\":{\"name\":\"lastName\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Valid last name is required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Username\",\"style\":{},\"attributes\":{\"name\":\"username\"},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"input-group-prepend\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"@\",\"style\":{\"bootstrap\":\"input-group-text\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Your username is required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{\"name\":\"width: 100%;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Email\",\"style\":{},\"attributes\":{\"name\":\"email\"},\"children\":[{\"name\":\"span\",\"text\":\"(Optional)\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"you@example.com\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Please enter a valid email address for shipping updates.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Address\",\"style\":{},\"attributes\":{\"name\":\"address\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Please enter your shipping address.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Address 2\",\"style\":{},\"attributes\":{\"name\":\"address2\"},\"children\":[{\"name\":\"span\",\"text\":\"(Optional)\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"Apartment or suite\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-5 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Country\",\"style\":{},\"attributes\":{\"name\":\"country\"},\"children\":[]},{\"name\":\"select\",\"text\":null,\"style\":{\"bootstrap\":\"custom-select d-block w-100\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"option\",\"text\":\"Choose...\",\"style\":{},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"option\",\"text\":\"United States\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Please select a valid country.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-4 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"State\",\"style\":{},\"attributes\":{\"name\":\"state\"},\"children\":[]},{\"name\":\"select\",\"text\":null,\"style\":{\"bootstrap\":\"custom-select d-block w-100\"},\"attributes\":{\"name\":\"\"},\"children\":[{\"name\":\"option\",\"text\":\"Choose...\",\"style\":{},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"option\",\"text\":\"California\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Please provide a valid state.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-3 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Zip\",\"style\":{},\"attributes\":{\"name\":\"zip\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Zip code required.\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"hr\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-checkbox\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"same-address\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Shipping address is the same as my billing address\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"same-address\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-checkbox\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"save-info\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Save this information for next time\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"save-info\"},\"children\":[]}]},{\"name\":\"hr\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"h4\",\"text\":\"Payment\",\"style\":{\"bootstrap\":\"mb-3\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-block my-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-radio\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Credit card\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"credit\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-radio\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Debit card\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"debit\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control custom-radio\"},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"custom-control-input\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Paypal\",\"style\":{\"bootstrap\":\"custom-control-label\"},\"attributes\":{\"name\":\"paypal\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Name on card\",\"style\":{},\"attributes\":{\"name\":\"cc-name\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"small\",\"text\":\"Full name as displayed on card\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":\"Name on card is required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Credit card number\",\"style\":{},\"attributes\":{\"name\":\"cc-number\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Credit card number is required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-3 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Expiration\",\"style\":{},\"attributes\":{\"name\":\"cc-expiration\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Expiration date required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-3 mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"CVV\",\"style\":{},\"attributes\":{\"name\":\"cc-expiration\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":\"Security code required\",\"style\":{\"bootstrap\":\"invalid-feedback\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"hr\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{},\"children\":[]},{\"name\":\"button\",\"text\":\"Continue to checkout\",\"style\":{\"bootstrap\":\"btn btn-primary btn-lg btn-block\"},\"attributes\":{\"name\":\"submit\"},\"children\":[]}]}]}]},{\"name\":\"footer\",\"text\":null,\"style\":{\"bootstrap\":\"my-5 pt-5 text-muted text-center text-small\"},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"© 2017-2018 Company Name\",\"style\":{\"bootstrap\":\"mb-1\"},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Privacy\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Terms\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"list-inline-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Support\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]}]}" },
                    { 3, 2, 16, "{\"name\":\"form\",\"text\":null,\"style\":{\"bootstrap\":\"form-signin\"},\"attributes\":{},\"children\":[{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"mb-4\"},\"attributes\":{\"name\":\"72\"},\"children\":[]},{\"name\":\"h1\",\"text\":\"Please sign in\",\"style\":{\"bootstrap\":\"h3 mb-3 font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"label\",\"text\":\"Email address\",\"style\":{\"bootstrap\":\"sr-only\"},\"attributes\":{\"name\":\"inputEmail\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"label\",\"text\":\"Password\",\"style\":{\"bootstrap\":\"sr-only\"},\"attributes\":{\"name\":\"inputPassword\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-control\"},\"attributes\":{\"name\":\"\"},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"checkbox mb-3\"},\"attributes\":{},\"children\":[{\"name\":\"label\",\"text\":\"Remember me\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"input\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"remember-me\"},\"children\":[]}]}]},{\"name\":\"button\",\"text\":\"Sign in\",\"style\":{\"bootstrap\":\"btn btn-lg btn-primary btn-block\"},\"attributes\":{\"name\":\"submit\"},\"children\":[]},{\"name\":\"p\",\"text\":\"© 2017-2018\",\"style\":{\"bootstrap\":\"mt-5 mb-3 text-muted\"},\"attributes\":{},\"children\":[]}]}" },
                    { 3, 2, 17, "{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"navbarnavbar-darksticky-topbg-darkflex-md-nowrapp-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Companyname\",\"style\":{\"bootstrap\":\"navbar-brandcol-sm-3col-md-2mr-0\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"input\",\"text\":null,\"style\":{\"bootstrap\":\"form-controlform-control-darkw-100\"},\"attributes\":{\"name\":\"Search\"},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"navbar-navpx-3\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-itemtext-nowrap\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Signout\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"container-fluid\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-2d-noned-md-blockbg-lightsidebar\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"sidebar-sticky\"},\"attributes\":{},\"children\":[{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"navflex-column\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Dashboard\",\"style\":{\"bootstrap\":\"nav-linkactive\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-home\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M39l9-797v11a22001-22H5a22001-2-2z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"92291215121522\"},\"children\":[]}]},{\"name\":\"span\",\"text\":\"(current)\",\"style\":{\"bootstrap\":\"sr-only\"},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Orders\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M132H6a22000-22v16a2200022h12a220002-2V9z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"132139209\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Products\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-shopping-cart\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1\"},\"children\":[]},{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1\"},\"children\":[]},{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M11h4l2.6813.39a2200021.61h9.72a220002-1.61L236H6\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Customers\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-users\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M1721v-2a44000-4-4H5a44000-44v2\"},\"children\":[]},{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"4\"},\"children\":[]},{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M2321v-2a44000-3-3.87\"},\"children\":[]},{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M163.13a4400107.75\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Reports\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-bar-chart-2\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"4\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"14\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Integrations\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-layers\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"polygon\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"122271212227122\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21712222217\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21212172212\"},\"children\":[]}]}]}]}]},{\"name\":\"h6\",\"text\":null,\"style\":{\"bootstrap\":\"sidebar-headingd-flexjustify-content-betweenalign-items-centerpx-3mt-4mb-1text-muted\"},\"attributes\":{},\"children\":[{\"name\":\"span\",\"text\":\"Savedreports\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":null,\"style\":{\"bootstrap\":\"d-flexalign-items-centertext-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-plus-circle\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"16\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"12\"},\"children\":[]}]}]}]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"navflex-columnmb-2\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Currentmonth\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Lastquarter\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Socialengagement\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]},{\"name\":\"li\",\"text\":null,\"style\":{\"bootstrap\":\"nav-item\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Year-endsale\",\"style\":{\"bootstrap\":\"nav-link\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-file-text\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"path\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"M142H6a22000-22v16a2200022h12a220002-2V8z\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"142148208\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"13\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17\"},\"children\":[]},{\"name\":\"polyline\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"1099989\"},\"children\":[]}]}]}]}]}]}]},{\"name\":\"main\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-9ml-sm-autocol-lg-10pt-3px-4\"},\"attributes\":{\"name\":\"main\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"chartjs-size-monitor\"},\"attributes\":{\"name\":\"position:absolute;inset:0px;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"chartjs-size-monitor-expand\"},\"attributes\":{\"name\":\"position:absolute;left:0;top:0;right:0;bottom:0;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"position:absolute;width:1000000px;height:1000000px;left:0;top:0\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"chartjs-size-monitor-shrink\"},\"attributes\":{\"name\":\"position:absolute;left:0;top:0;right:0;bottom:0;overflow:hidden;pointer-events:none;visibility:hidden;z-index:-1;\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"position:absolute;width:200%;height:200%;left:0;top:0\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-flexjustify-content-betweenflex-wrapflex-md-nowrapalign-items-centerpb-2mb-3border-bottom\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Dashboard\",\"style\":{\"bootstrap\":\"h2\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"btn-toolbarmb-2mb-md-0\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"btn-groupmr-2\"},\"attributes\":{},\"children\":[{\"name\":\"button\",\"text\":\"Share\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondary\"},\"attributes\":{},\"children\":[]},{\"name\":\"button\",\"text\":\"Export\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondary\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"button\",\"text\":\"Thisweek\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondarydropdown-toggle\"},\"attributes\":{},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"featherfeather-calendar\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"rect\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"2\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]}]}]}]}]},{\"name\":\"canvas\",\"text\":null,\"style\":{\"bootstrap\":\"my-4chartjs-render-monitor\"},\"attributes\":{\"name\":\"display:block;height:276px;width:655px;\"},\"children\":[]},{\"name\":\"h2\",\"text\":\"Sectiontitle\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"table-responsive\"},\"attributes\":{},\"children\":[{\"name\":\"table\",\"text\":null,\"style\":{\"bootstrap\":\"tabletable-stripedtable-sm\"},\"attributes\":{},\"children\":[{\"name\":\"thead\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"th\",\"text\":\"#\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"th\",\"text\":\"Header\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"tbody\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,001\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Lorem\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ipsum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"dolor\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sit\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,002\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"amet\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"consectetur\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"adipiscing\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"elit\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,003\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Integer\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nec\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"odio\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Praesent\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,003\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"libero\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Sed\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"cursus\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ante\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,004\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"dapibus\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"diam\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Sed\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nisi\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,005\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Nulla\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"quis\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sem\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"at\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,006\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nibh\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"elementum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"imperdiet\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Duis\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,007\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sagittis\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ipsum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Praesent\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"mauris\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,008\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Fusce\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nec\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"tellus\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sed\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,009\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"augue\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"semper\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"porta\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Mauris\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,010\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"massa\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Vestibulum\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"lacinia\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"arcu\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,011\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"eget\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nulla\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Class\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"aptent\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,012\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"taciti\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sociosqu\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ad\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"litora\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,013\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"torquent\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"per\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"conubia\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"nostra\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,014\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"per\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"inceptos\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"himenaeos\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"Curabitur\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"tr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"td\",\"text\":\"1,015\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"sodales\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"ligula\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"in\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"td\",\"text\":\"libero\",\"style\":{},\"attributes\":{},\"children\":[]}]}]}]}]}]}]}]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"anonymous\"},\"children\":[]},{\"name\":\"script\",\"text\":\"window.jQuery||document.write('<scriptsrc=\\\"../../assets/js/vendor/jquery-slim.min.js\\\"><\\\\/script>')\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/popper.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../dist/js/bootstrap.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"https://unpkg.com/feather-icons/dist/feather.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"feather.replace()\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"https://cdn.jsdelivr.net/npm/chart.js@2.7.1/dist/Chart.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"varctx=document.getElementById(\\\"myChart\\\");\\nvarmyChart=newChart(ctx,{\\ntype:'line',\\ndata:{\\nlabels:[\\\"Sunday\\\",\\\"Monday\\\",\\\"Tuesday\\\",\\\"Wednesday\\\",\\\"Thursday\\\",\\\"Friday\\\",\\\"Saturday\\\"],\\ndatasets:[{\\ndata:[15339,21345,18483,24003,23489,24092,12034],\\nlineTension:0,\\nbackgroundColor:'transparent',\\nborderColor:'#007bff',\\nborderWidth:4,\\npointBackgroundColor:'#007bff'\\n}]\\n},\\noptions:{\\nscales:{\\nyAxes:[{\\nticks:{\\nbeginAtZero:false\\n}\\n}]\\n},\\nlegend:{\\ndisplay:false,\\n}\\n}\\n});\",\"style\":{},\"attributes\":{},\"children\":[]}]}" },
                    { 2, 2, 18, "{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"site-headersticky-toppy-1\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"containerd-flexflex-columnflex-md-rowjustify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":null,\"style\":{\"bootstrap\":\"py-2\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"d-blockmx-auto\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17.94\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"8\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"2.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"16\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21.94\"},\"children\":[]}]}]},{\"name\":\"a\",\"text\":\"Tour\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Product\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Features\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Enterprise\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Support\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Pricing\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Cart\",\"style\":{\"bootstrap\":\"py-2d-noned-md-inline-block\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"position-relativeoverflow-hiddenp-3p-md-5m-md-3text-centerbg-light\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-5p-lg-5mx-automy-5\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Punnyheadline\",\"style\":{\"bootstrap\":\"display-4font-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheadingtoboot.JumpstartyourmarketingeffortswiththisexamplebasedonApple'smarketingpages.\",\"style\":{\"bootstrap\":\"leadfont-weight-normal\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Comingsoon\",\"style\":{\"bootstrap\":\"btnbtn-outline-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"product-devicebox-shadowd-noned-md-block\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"product-deviceproduct-device-2box-shadowd-noned-md-block\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-darkmr-md-3pt-3px-3pt-md-5px-md-5text-centertext-whiteoverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-darkbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-darkbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-primarymr-md-3pt-3px-3pt-md-5px-md-5text-centertext-whiteoverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightbox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"d-md-flexflex-md-equalw-100my-md-3pl-md-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-lightmr-md-3pt-3px-3pt-md-5px-md-5text-centeroverflow-hidden\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"my-3py-3\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherheadline\",\"style\":{\"bootstrap\":\"display-5\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Andanevenwittiersubheading.\",\"style\":{\"bootstrap\":\"lead\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"bg-whitebox-shadowmx-auto\"},\"attributes\":{\"name\":\"width:80%;height:300px;border-radius:21px21px00;\"},\"children\":[]}]}]},{\"name\":\"footer\",\"text\":null,\"style\":{\"bootstrap\":\"containerpy-5\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-12col-md\"},\"attributes\":{},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"d-blockmb-2\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"10\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"17.94\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"8\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"2.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"6.06\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"16\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"21.94\"},\"children\":[]}]},{\"name\":\"small\",\"text\":\"©2017-2018\",\"style\":{\"bootstrap\":\"d-blockmb-3text-muted\"},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Features\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Coolstuff\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Randomfeature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Teamfeature\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Stufffordevelopers\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Anotherone\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Lasttime\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Resourcename\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Anotherresource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Finalresource\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"Resources\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Business\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Education\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Government\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Gaming\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-6col-md\"},\"attributes\":{},\"children\":[{\"name\":\"h5\",\"text\":\"About\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledtext-small\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Team\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Locations\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Privacy\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Terms\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]}]}]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"anonymous\"},\"children\":[]},{\"name\":\"script\",\"text\":\"window.jQuery||document.write('<scriptsrc=\\\"../../assets/js/vendor/jquery-slim.min.js\\\"><\\\\/script>')\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/popper.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../dist/js/bootstrap.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/holder.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"Holder.addTheme('thumb',{\\nbg:'#55595c',\\nfg:'#eceeef',\\ntext:'Thumbnail'\\n});\",\"style\":{},\"attributes\":{},\"children\":[]}]}" },
                    { 3, 2, 19, "{\"name\":\"div\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"container\"},\"attributes\":{},\"children\":[{\"name\":\"header\",\"text\":null,\"style\":{\"bootstrap\":\"blog-headerpy-3\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"rowflex-nowrapjustify-content-betweenalign-items-center\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-4pt-1\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Subscribe\",\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-4text-center\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Large\",\"style\":{\"bootstrap\":\"blog-header-logotext-dark\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-4d-flexjustify-content-endalign-items-center\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":null,\"style\":{\"bootstrap\":\"text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[{\"name\":\"svg\",\"text\":null,\"style\":{\"bootstrap\":\"mx-3\"},\"attributes\":{\"name\":\"round\"},\"children\":[{\"name\":\"circle\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"7.5\"},\"children\":[]},{\"name\":\"line\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"15.8\"},\"children\":[]}]}]},{\"name\":\"a\",\"text\":\"Signup\",\"style\":{\"bootstrap\":\"btnbtn-smbtn-outline-secondary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"nav-scrollerpy-1mb-2\"},\"attributes\":{},\"children\":[{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"navd-flexjustify-content-between\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"World\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"U.S.\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Technology\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Design\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Culture\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Business\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Politics\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Opinion\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Science\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Health\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Style\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Travel\",\"style\":{\"bootstrap\":\"p-2text-muted\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"jumbotronp-3p-md-5text-whiteroundedbg-dark\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6px-0\"},\"attributes\":{},\"children\":[{\"name\":\"h1\",\"text\":\"Titleofalongerfeaturedblogpost\",\"style\":{\"bootstrap\":\"display-4font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Multiplelinesoftextthatformthelede,informingnewreadersquicklyandefficientlyaboutwhat'smostinterestinginthispost'scontents.\",\"style\":{\"bootstrap\":\"leadmy-3\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":null,\"style\":{\"bootstrap\":\"leadmb-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Continuereading...\",\"style\":{\"bootstrap\":\"text-whitefont-weight-bold\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"rowmb-2\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"cardflex-md-rowmb-4box-shadowh-md-250\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"card-bodyd-flexflex-columnalign-items-start\"},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"World\",\"style\":{\"bootstrap\":\"d-inline-blockmb-2text-primary\"},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":null,\"style\":{\"bootstrap\":\"mb-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Featuredpost\",\"style\":{\"bootstrap\":\"text-dark\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Nov12\",\"style\":{\"bootstrap\":\"mb-1text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Thisisawidercardwithsupportingtextbelowasanaturallead-intoadditionalcontent.\",\"style\":{\"bootstrap\":\"card-textmb-auto\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Continuereading\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"card-img-rightflex-autod-noned-md-block\"},\"attributes\":{\"name\":\"true\"},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-6\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"cardflex-md-rowmb-4box-shadowh-md-250\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"card-bodyd-flexflex-columnalign-items-start\"},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"Design\",\"style\":{\"bootstrap\":\"d-inline-blockmb-2text-success\"},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":null,\"style\":{\"bootstrap\":\"mb-0\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Posttitle\",\"style\":{\"bootstrap\":\"text-dark\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"div\",\"text\":\"Nov11\",\"style\":{\"bootstrap\":\"mb-1text-muted\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Thisisawidercardwithsupportingtextbelowasanaturallead-intoadditionalcontent.\",\"style\":{\"bootstrap\":\"card-textmb-auto\"},\"attributes\":{},\"children\":[]},{\"name\":\"a\",\"text\":\"Continuereading\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"img\",\"text\":null,\"style\":{\"bootstrap\":\"card-img-rightflex-autod-noned-md-block\"},\"attributes\":{\"name\":\"width:200px;height:250px;\"},\"children\":[]}]}]}]}]},{\"name\":\"main\",\"text\":null,\"style\":{\"bootstrap\":\"container\"},\"attributes\":{\"name\":\"main\"},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"row\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-8blog-main\"},\"attributes\":{},\"children\":[{\"name\":\"h3\",\"text\":\"FromtheFirehose\",\"style\":{\"bootstrap\":\"pb-3mb-4font-italicborder-bottom\"},\"attributes\":{},\"children\":[]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"blog-post\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Sampleblogpost\",\"style\":{\"bootstrap\":\"blog-post-title\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"January1,2014by\",\"style\":{\"bootstrap\":\"blog-post-meta\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Mark\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Thisblogpostshowsafewdifferenttypesofcontentthat'ssupportedandstyledwithBootstrap.Basictypography,images,andcodeareallsupported.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"hr\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnis,nasceturridiculusmus.Aeneaneuleoquam.Pellentesqueornaresemlaciniaquamvenenatisvestibulum.Sedposuereconsecteturestatlobortis.Crasmattisconsecteturpurussitametfermentum.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"disparturientmontes\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"blockquote\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"Curabiturblandittempusporttitor.ornareveleuleo.Nullamiddoloridnibhultriciesvehiculautidelit.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"Nullamquisrisusegeturnamollis\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"h2\",\"text\":\"Heading\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Vivamussagittislacusvelauguelaoreetrutrumfaucibusdolorauctor.Duismollis,estnoncommodoluctus,nisieratporttitorligula,egetlaciniaodiosemnecelit.Morbileorisus,portaacconsecteturac,vestibulumateros.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":\"Sub-heading\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"pre\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"code\",\"text\":\"Examplecodeblock\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Aeneanlaciniabibendumnullasedconsectetur.Etiamportasemmalesuadamagnamolliseuismod.Fuscedapibus,tellusaccursuscommodo,tortormauriscondimentumnibh,utfermentummassa.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"h3\",\"text\":\"Sub-heading\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.Aeneanlaciniabibendumnullasedconsectetur.Etiamportasemmalesuadamagnamolliseuismod.Fuscedapibus,tellusaccursuscommodo,tortormauriscondimentumnibh,utfermentummassajustositametrisus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"Praesentcommodocursusmagna,velscelerisquenislconsecteturet.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Donecidelitnonmiportagravidaategetmetus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Donecullamcorpernullanonmetusauctorfringilla.Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ol\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"Vestibulumidligulaportafeliseuismodsemper.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Maecenasseddiamegetrisusvariusblanditsitametnonmagna.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Crasmattisconsecteturpurussitametfermentum.Sedposuereconsecteturestatlobortis.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"blog-post\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Anotherblogpost\",\"style\":{\"bootstrap\":\"blog-post-title\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"December23,2013by\",\"style\":{\"bootstrap\":\"blog-post-meta\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Jacob\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnis,nasceturridiculusmus.Aeneaneuleoquam.Pellentesqueornaresemlaciniaquamvenenatisvestibulum.Sedposuereconsecteturestatlobortis.Crasmattisconsecteturpurussitametfermentum.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"disparturientmontes\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"blockquote\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"Curabiturblandittempusporttitor.ornareveleuleo.Nullamiddoloridnibhultriciesvehiculautidelit.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"strong\",\"text\":\"Nullamquisrisusegeturnamollis\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Vivamussagittislacusvelauguelaoreetrutrumfaucibusdolorauctor.Duismollis,estnoncommodoluctus,nisieratporttitorligula,egetlaciniaodiosemnecelit.Morbileorisus,portaacconsecteturac,vestibulumateros.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"blog-post\"},\"attributes\":{},\"children\":[{\"name\":\"h2\",\"text\":\"Newfeature\",\"style\":{\"bootstrap\":\"blog-post-title\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"December14,2013by\",\"style\":{\"bootstrap\":\"blog-post-meta\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Chris\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Cumsociisnatoquepenatibusetmagnisdisparturientmontes,nasceturridiculusmus.Aeneanlaciniabibendumnullasedconsectetur.Etiamportasemmalesuadamagnamolliseuismod.Fuscedapibus,tellusaccursuscommodo,tortormauriscondimentumnibh,utfermentummassajustositametrisus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"ul\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":\"Praesentcommodocursusmagna,velscelerisquenislconsecteturet.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Donecidelitnonmiportagravidaategetmetus.\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"li\",\"text\":\"Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"p\",\"text\":\"Donecullamcorpernullanonmetusauctorfringilla.Nullavitaeelitlibero,apharetraaugue.\",\"style\":{},\"attributes\":{},\"children\":[]}]},{\"name\":\"nav\",\"text\":null,\"style\":{\"bootstrap\":\"blog-pagination\"},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Older\",\"style\":{\"bootstrap\":\"btnbtn-outline-primary\"},\"attributes\":{\"name\":\"#\"},\"children\":[]},{\"name\":\"a\",\"text\":\"Newer\",\"style\":{\"bootstrap\":\"btnbtn-outline-secondarydisabled\"},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"aside\",\"text\":null,\"style\":{\"bootstrap\":\"col-md-4blog-sidebar\"},\"attributes\":{},\"children\":[{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"p-3mb-3bg-lightrounded\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"About\",\"style\":{\"bootstrap\":\"font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"p\",\"text\":\"Etiamportamolliseuismod.Crasmattisconsecteturpurussitametfermentum.Aeneanlaciniabibendumnullasedconsectetur.\",\"style\":{\"bootstrap\":\"mb-0\"},\"attributes\":{},\"children\":[{\"name\":\"em\",\"text\":\"semmalesuadamagna\",\"style\":{},\"attributes\":{},\"children\":[]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"Archives\",\"style\":{\"bootstrap\":\"font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"ol\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyledmb-0\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"March2014\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"February2014\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"January2014\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"December2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"November2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"October2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"September2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"August2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"July2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"June2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"May2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"April2013\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]},{\"name\":\"div\",\"text\":null,\"style\":{\"bootstrap\":\"p-3\"},\"attributes\":{},\"children\":[{\"name\":\"h4\",\"text\":\"Elsewhere\",\"style\":{\"bootstrap\":\"font-italic\"},\"attributes\":{},\"children\":[]},{\"name\":\"ol\",\"text\":null,\"style\":{\"bootstrap\":\"list-unstyled\"},\"attributes\":{},\"children\":[{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"GitHub\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Twitter\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]},{\"name\":\"li\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Facebook\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]}]}]}]}]},{\"name\":\"footer\",\"text\":null,\"style\":{\"bootstrap\":\"blog-footer\"},\"attributes\":{},\"children\":[{\"name\":\"p\",\"text\":\"Blogtemplatebuiltforby.\",\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Bootstrap\",\"style\":{},\"attributes\":{\"name\":\"https://getbootstrap.com/\"},\"children\":[]},{\"name\":\"a\",\"text\":\"@mdo\",\"style\":{},\"attributes\":{\"name\":\"https://twitter.com/mdo\"},\"children\":[]}]},{\"name\":\"p\",\"text\":null,\"style\":{},\"attributes\":{},\"children\":[{\"name\":\"a\",\"text\":\"Backtotop\",\"style\":{},\"attributes\":{\"name\":\"#\"},\"children\":[]}]}]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"anonymous\"},\"children\":[]},{\"name\":\"script\",\"text\":\"window.jQuery||document.write('<scriptsrc=\\\"../../assets/js/vendor/jquery-slim.min.js\\\"><\\\\/script>')\",\"style\":{},\"attributes\":{},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/popper.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../dist/js/bootstrap.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":null,\"style\":{},\"attributes\":{\"name\":\"../../assets/js/vendor/holder.min.js\"},\"children\":[]},{\"name\":\"script\",\"text\":\"Holder.addTheme('thumb',{\\nbg:'#55595c',\\nfg:'#eceeef',\\ntext:'Thumbnail'\\n});\",\"style\":{},\"attributes\":{},\"children\":[]}]}" }
                });

            migrationBuilder.InsertData(
                table: "WidgetProperty",
                columns: new[] { "PropertyID", "WidgetID", "DefaultValue" },
                values: new object[,]
                {
                    { 1, 1, "primary" },
                    { 2, 1, "primary" },
                    { 3, 1, "primary" },
                    { 4, 1, "3" },
                    { 5, 1, "text-left" },
                    { 6, 1, "border border-1" },
                    { 7, 1, "rounded-1" },
                    { 8, 1, "m-1" },
                    { 9, 1, "p-0" },
                    { 10, 1, "d-block" },
                    { 11, 1, "shadow-none" },
                    { 12, 1, "100" },
                    { 13, 1, "100" },
                    { 1, 2, "primary" },
                    { 2, 2, "primary" },
                    { 3, 2, "primary" },
                    { 4, 2, "3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppType_Type",
                table: "AppType",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Project_AppTypeID",
                table: "Project",
                column: "AppTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TargetFrameworkID",
                table: "Project",
                column: "TargetFrameworkID");

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserID_Title",
                table: "Project",
                columns: new[] { "UserID", "Title" },
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Property_ParentPropertyID",
                table: "Property",
                column: "ParentPropertyID",
                unique: true,
                filter: "[ParentPropertyID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Property_PropertyName",
                table: "Property",
                column: "PropertyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyUnit_UnitID",
                table: "PropertyUnit",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValue_PropertyID",
                table: "PropertyValue",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_TargetFramework_FrameworkName",
                table: "TargetFramework",
                column: "FrameworkName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_UnitName",
                table: "Unit",
                column: "UnitName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widget_IconPath",
                table: "Widget",
                column: "IconPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Widget_ParentWidgetID",
                table: "Widget",
                column: "ParentWidgetID");

            migrationBuilder.CreateIndex(
                name: "IX_Widget_RelatedAppTypeID",
                table: "Widget",
                column: "RelatedAppTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Widget_Title",
                table: "Widget",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WidgetAttribute_AttributeId",
                table: "WidgetAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetCodeSnippet_LayoutID",
                table: "WidgetCodeSnippet",
                column: "LayoutID");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetCodeSnippet_TargetFrameworkID",
                table: "WidgetCodeSnippet",
                column: "TargetFrameworkID");

            migrationBuilder.CreateIndex(
                name: "IX_WidgetProperty_PropertyID",
                table: "WidgetProperty",
                column: "PropertyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "PropertyUnit");

            migrationBuilder.DropTable(
                name: "PropertyValue");

            migrationBuilder.DropTable(
                name: "WidgetAttribute");

            migrationBuilder.DropTable(
                name: "WidgetCodeSnippet");

            migrationBuilder.DropTable(
                name: "WidgetProperty");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Layout");

            migrationBuilder.DropTable(
                name: "TargetFramework");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Widget");

            migrationBuilder.DropTable(
                name: "AppType");
        }
    }
}
