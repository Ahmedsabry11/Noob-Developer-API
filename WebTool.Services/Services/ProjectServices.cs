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

    public class ProjectServices: IProjectServices
    {
        private readonly IRepository<Project> _ProjectRepository;
        private readonly APPDBContext _context;
        public ProjectServices(IRepository<Project> projectRepository, APPDBContext context)
        {
            _ProjectRepository = projectRepository;
            _context = context;
        }

        public async Task<List<ProjectsDTO>> GetProjects(string id)
        {
            var user = await _context.Users.Where(p => p.Id == id).ToListAsync();
            List<ProjectsDTO> results = new List<ProjectsDTO>();
            Console.WriteLine("user check");
            if (user.Count == 0)
                return null;
            Console.WriteLine("user found");
            var projects = await _context.Projects.Where(p => p.UserID == id).ToListAsync();
            foreach (var project in projects)
            {
                results.Add(
                    new ProjectsDTO
                    {
                        Title = project.Title,
                        Description = project.Description,
                        GeneratedAppPath = project.GeneratedAppPath,
                    }
                    );
            }
            return results;
        }
        public async Task<string> GetProjectWithTitle(string id, string title)
        {
            var projects = await _context.Projects.Where(p => p.UserID == id
                                                        && p.Title == title).ToListAsync();
            string result = null;
            foreach (var project in projects)
            {
               result = project.Widgets;
                
            }
            return result;
        }
        public async Task<bool> CreateProject(string id,string username,string title,string description)
        {
            var checktitle = await _context.Projects.Where(p => p.UserID == id &&
                    p.Title == title).ToListAsync();

            if (checktitle.Count != 0)
            {
                Console.WriteLine("Found");
                return false;
            }
            //---------------------- create project -----------------------------
            string GeneratedAppPath = "Projects&"+username+"&"+title+"&";
            string widgets = "{}";
            _ProjectRepository.Insert(
                new Project
                {
                    AppTypeID = 1,
                    TargetFrameworkID = 1,
                    Title = title,
                    UserID = id,
                    Description = description,
                    CreationDate = DateTime.Now,
                    Widgets = widgets,
                    GeneratedAppPath = GeneratedAppPath
                }
                );
            //-------------- what happen if there was an error in insert ?? -------------
            //----------- use string to store error and send it as a response-----------
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<string> Download(string id,string Title)
        {
            var projects = await _context.Projects.Where(p => p.UserID == id &&
                    p.Title == Title).ToListAsync();

            string path = null; 
            foreach (var project in projects)
            {
                path = project.GeneratedAppPath;
            }
            return path;
        }
        public async Task<string> Save(string id, string Title,string widgets)
        {
            Project project = _context.Projects.Where(p => p.UserID == id &&
                    p.Title == Title).First();
            if (project == null)
                return null;

            project.Widgets = widgets;
            string path = project.GeneratedAppPath;
            await _context.SaveChangesAsync();
            return path;
        }
        public async Task<bool> Delete(string userid,string title)
        {

            if (userid == null || title == null)
            {
                return false;
            }
            Project ProjectToDelete =await _context.Projects.Where(p => p.UserID == userid && p.Title == title).FirstOrDefaultAsync();
            if(ProjectToDelete == null)
            {
                return false;
            }
            _ProjectRepository.Delete(ProjectToDelete);
            return true;
        }
    }
}
