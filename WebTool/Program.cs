
using Microsoft.EntityFrameworkCore;
using DomainLayer.Data;
using DomainLayer.Entities;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using Microsoft.AspNetCore.Identity;
using WebTool.Services.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using WebTool.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddDbContext<APPDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDB"), b => b.MigrationsAssembly("DomainLayer")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<APPDBContext>()
                .AddDefaultTokenProviders();



builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<> ));
builder.Services.AddScoped(typeof(IWidgetServices), typeof(WidgetServices));
builder.Services.AddScoped(typeof(IPropertyServices), typeof(PropertyServices));
builder.Services.AddScoped(typeof(IProjectServices), typeof(ProjectServices));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<APPDBContext>();
    dataContext.Database.Migrate();
}

try
{
    if(!System.IO.Directory.Exists("./Projects"))
    {
        System.IO.Directory.CreateDirectory("./Projects");
        Console.WriteLine("Create Projects Folder for users");
    }
    
}
catch(Exception e)
{
    Console.WriteLine(e);
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider
  (Path.Combine(Directory.GetCurrentDirectory(), @"")),
    RequestPath = new PathString("")
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
