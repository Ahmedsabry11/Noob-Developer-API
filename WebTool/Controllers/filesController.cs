using Microsoft.AspNetCore.Mvc;
using WebTool.Services.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Authorization;
using WebTool.Services.IServices;

namespace WebTool.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("files")]
    public class filesController:ControllerBase
    {
        
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        private readonly IWidgetServices _WidgetServices;
        public filesController(
            FileExtensionContentTypeProvider fileExtensionContentTypeProvider
            ,IWidgetServices WidgetServices)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(
                    nameof(fileExtensionContentTypeProvider));
            _WidgetServices = WidgetServices
                ?? throw new System.ArgumentNullException(
                    nameof(WidgetServices));
        }
        [HttpGet("icons/{path}")]
        public async Task<ActionResult> WidgetIcons(string path)
        {
            //path = path.Replace("&","/");
            path = "icons/" + path;
            Console.WriteLine("path after decode :"+path);
            
            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("wrong image path");
                return NotFound("Icon not found");
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(
                path, out var contentType))
            {
                contentType = "image";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            Console.WriteLine("send image: "+path);
            return File(bytes, contentType, Path.GetFileName(path));
        }
    }
}
