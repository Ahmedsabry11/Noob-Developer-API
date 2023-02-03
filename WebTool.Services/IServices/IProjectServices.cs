using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace WebTool.Services.IServices
{
    public interface IProjectServices
    {
        public  Task<List<ProjectsDTO>> GetProjects(string id);
        public  Task<string> GetProjectWithTitle(string id, string title);
        public  Task<bool> CreateProject(string id, string username, string title, string description);
        public  Task<string> Download(string id, string Title);
        public  Task<string> Save(string id, string Title, string widgets);
        public  Task<bool> Delete(string userid, string title);
    }
}
