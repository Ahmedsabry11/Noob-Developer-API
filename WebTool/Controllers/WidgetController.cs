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
    [Route("api/widgets")]
    public class WidgetController : ControllerBase
    {

        private readonly IWidgetServices _WidgetServices;
        public WidgetController(IWidgetServices WidgetServices)
        {
            _WidgetServices = WidgetServices ??
                throw new ArgumentNullException(nameof(WidgetServices));
        }


        //[HttpGet]
        //public async Task<ActionResult<List<WidgetATO>>> GetWidgets()
        //{
        //    // notice: you should check apptype of retrieved widget.

        //    List<WidgetATO> results = await _WidgetServices.GetAllWidgets();
        //    return Ok(results);
            

        //}


        //[HttpGet("{id}")]
        //public async Task<ActionResult<Widget>> GetWidget(int id)
        //{

        //    //WidgetATO result = await _WidgetServices.GetWidget(id);
        //    WidgetATO result = await _WidgetServices.GetWidgetCode(id);
        //    if (result == null)
        //        return NotFound();
        //    return Ok(result);
            
        //}
        [HttpGet("child")]
        public async Task<ActionResult<List<WidgetDTO>>> getfirstchilds()
        {

            //List<WidgetATO> results = await _WidgetServices.GetChildWidgets(id);
            List<WidgetDTO> results = await _WidgetServices.GetAllChildWidgetCodes(null);
            return Ok(results);
        }
        [HttpGet("child/{id}")]
        public async Task<ActionResult<List<WidgetDTO>>> getchilds(int? id)
        {
            List<WidgetDTO> results = await _WidgetServices.GetAllChildWidgetCodes(id);
            return Ok(results);
        }
        [HttpGet("Attributes/{id}")]
        public async Task<ActionResult<List<AttributesDTO>>>  GetWidgetAttributes(int id)
        {
            List<AttributesDTO> attributes = await _WidgetServices.GetWidgetAttriubtes(id);
            if(attributes == null)
            {
                return NotFound("invaild widget id");
            }
            return Ok(attributes);
        }
    }
}
