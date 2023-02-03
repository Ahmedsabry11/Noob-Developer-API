using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.IRepository;
using ViewModels.ViewModels;
using DomainLayer.Entities;
using WebTool.Services.Services;
using Microsoft.AspNetCore.Authorization;
using WebTool.Services.IServices;

namespace WebTool.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/properties")]
    public class PropertyController: ControllerBase
    {
        private readonly IPropertyServices _PropertyServices;
        public PropertyController(IPropertyServices propertyServices)
        {
            _PropertyServices = propertyServices ??
                throw new ArgumentNullException(nameof(propertyServices));
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Dictionary<string,List<string>>>> SendWidgetStyle(int id)
        //{
        //    Console.WriteLine("\n\nWelcome to Property!!\n\n");
        //    Dictionary<string,List<string>> results = await _PropertyServices.GetWidgetStyle(id);
        //    if (results == null)
        //        return NotFound("invaild widget id");
        //    return Ok(results);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<List<PropertyDTO>>> Test(int id)
        {
            Console.WriteLine("\n\nWelcome to Property!!\n\n");
            List<PropertyDTO> results = await _PropertyServices.TestStyles(id);
            if (results == null)
                return NotFound("invaild widget id");
            return Ok(results);
        }
    }
}
