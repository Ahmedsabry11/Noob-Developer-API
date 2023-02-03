using Microsoft.AspNetCore.Mvc;
using ViewModels.ViewModels;
using WebTool.Services.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IO.Compression;
using WebTool.Services.IServices;

namespace WebTool.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/projects")]
    public class ProjectController:ControllerBase
    {
        private readonly IProjectServices _ProjectServices;
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public ProjectController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider,
            IProjectServices projectservices)
        {

            _ProjectServices = projectservices
                ?? throw new System.ArgumentNullException(
                    nameof(ProjectServices));
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(
                    nameof(fileExtensionContentTypeProvider));
        }
        
        [Authorize]
        [HttpGet("")]
        //-------------- Fetch all projects for certian users
        public  async Task<ActionResult<List<ProjectsDTO>>> SendUserProjects()
        {
            // get userid from token and claims
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userid == null)
            {
                return Unauthorized("Expired Token");
            }
            // query to get all projects of userid
            List<ProjectsDTO> results = await _ProjectServices.GetProjects(userid);
            if(results == null)
            {
                return NotFound("Invaild Userid");
            }
            return Ok(results);
        }
        //---------- load certain project by title of project
        [Authorize]
        [HttpPost("projecttitle/{title}")]
        public async Task<ActionResult<string>> SendUserProject(string title)
        {
            // get userid from token and claims   
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userid == null)
            {
                return Unauthorized("Expired Token");
            }
            // query to get project of userid by title
            string result = await _ProjectServices.GetProjectWithTitle(userid, title);
            if (result == null)
            {
                return NotFound("invalid user id or title");
            } 
            return Ok(result);
        }
        //----------- create a project to user
        [Authorize]
        [HttpPost("Create")]
        public async Task<ActionResult<string>> CreateProject([FromBody] ProjectsDTO project)
        {
            // get userid and username from token and claims
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            if (userid == null || username == null)
            {
                return Unauthorized("Expired Token");
            }
            // query to create a project with unique title for certain user with description.
            bool result = await _ProjectServices.CreateProject(
                userid,username, project.Title, project.Description);
            if(result)
            {
                Response.StatusCode = 201;
                // create folder for the project to save user files
                System.IO.Directory.CreateDirectory("./Projects/" + username +"/"+ project.Title);
                string generatedapppath = "Projects&" + username + "&" + project.Title + "&";
                return Ok(generatedapppath);
            }
            // project with same title exists in database
            Response.StatusCode = 400;
            return BadRequest("Enter New Title");
        }
        //---------------- download project zip file
        [Authorize]
        [HttpGet("Download/{title}")]

        public async Task<ActionResult> DownloadProject(string title)
        {
            // get userid and username from token and claims
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            if (userid == null)
            {
                return Unauthorized("Expired Token");
            }

            // get path of project of user 
            string pathToFile = await _ProjectServices.Download(userid, title);
            if (pathToFile == null)
            {
                return NotFound("Invaild Title");
            }

            string startPath =  "Projects/"+userName + "/" + title+"/";
            string zipPath = "Projects/" + userName +"/"+title+".zip";
            Console.WriteLine("startPath: " + startPath);
            Console.WriteLine("zipPath: " + zipPath);
            try
            {
                // check if already exists an old version to delete
                if(System.IO.File.Exists(zipPath))
                {
                    System.IO.File.Delete(zipPath);
                    Console.WriteLine("Zip File deleted");
                }
                // zip project
                ZipFile.CreateFromDirectory(startPath, zipPath, CompressionLevel.Fastest, true);
                Console.WriteLine("successful create zip file");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Whoops");
            }
            if (!System.IO.File.Exists(zipPath))
            {
                return NotFound("File not found");
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(
                pathToFile, out var contentType))
            {
                contentType = "application/zip";
            }
            // send zip file bytes
            var bytes = System.IO.File.ReadAllBytes(zipPath);
            Console.WriteLine("successful sending file to front");
            return File(bytes, contentType, Path.GetFileName(zipPath));
        }
        //-------------- save widgets code of project in database
        [Authorize]
        [HttpPost("Save")]
        public async Task<ActionResult> Save([FromBody] ProjectsDTO project)
        {
            try
            {
                // get userid and username from token and claims
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                string userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userid == null)
                {
                    return Unauthorized("Expired Token");
                }
                // validate inputs
                if (project.Widgets == null )
                {
                    return BadRequest("Enter Widgets");
                }
                string appPath = await _ProjectServices.Save(
                    userid, project.Title, project.Widgets);
                if (appPath == null)
                    return NotFound("Invaild ID or Title");
                return Ok("Widget Code Saved Successfully");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return NotFound(e.Message);
            }
        }
        //--------------------- save users images and files
        [Authorize]
        [HttpPost("saveimage/{path}"), DisableRequestSizeLimit]
        //[ActionName("Upload")]
        public  ActionResult SaveImage(IFormFile file ,[FromRoute] string path)
        {
            path = path.Replace("&", "/");
            //path = Uri.UnescapeDataString(path);
            Console.WriteLine("path after decode :" + path);
            if (!System.IO.Directory.Exists(path))
            {
                Console.WriteLine("wrong app path");
                return NotFound(path);
            }

            string originalFileName = Path.GetFileName(file.FileName);
            Console.WriteLine("file name: "+ originalFileName);

            path = path + originalFileName;
            Console.WriteLine("Final path: " + path);
            Console.WriteLine("file: " + file.Length);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            using (Stream stream = new FileStream("./"+path,FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Flush();
            }
            return Ok("Saved file successfully ");

            //if (!_fileExtensionContentTypeProvider.TryGetContentType(
            //   path, out var contentType))
            //{
            //    contentType = "image";
            //}
            //var bytes = System.IO.File.ReadAllBytes(path);
            //return File(bytes, contentType, Path.GetFileName(path));
            //return Ok("Saved image successfully");
        }
        //-------------- send images from server to client
        [HttpGet("loadimage/{path}"), DisableRequestSizeLimit]
        public ActionResult LoadImage(string path)
        {
            // Replace '&' in path with / to generate correct path
            path = path.Replace("&", "/");
            //path = Uri.UnescapeDataString(path);
            Console.WriteLine("path after decode :" + path);
            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("wrong file path");
                return NotFound("file not found");
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(
               path, out var contentType))
            {
                contentType = "image";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, contentType, Path.GetFileName(path));
        }
        [Authorize]
        [HttpDelete("DeleteFile/{path}")]
        public ActionResult DeleteFile([FromRoute] string path)
        {
            path = Uri.UnescapeDataString(path);
            Console.WriteLine("path after decode :" + path);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                Console.WriteLine("file deleted Successfully");
                Response.StatusCode = 204;
                return Ok("file deleted Successfully");
            }
            return NotFound("file not found");
        }
        [Authorize]
        [HttpDelete("DeleteProject/{title}")]
        public async Task<ActionResult> DeleteProject(string title)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userid == null)
            {
                return Unauthorized("Expired Token");
            }
            bool isDeleted = await _ProjectServices.Delete(userid, title);
            if (isDeleted)
            {
                Console.WriteLine("Project Deleted Successfully");
                Response.StatusCode = 204;
                return Ok("Project Deleted Successfully");
            }
            return NotFound("project not found");
        }
    }

}
