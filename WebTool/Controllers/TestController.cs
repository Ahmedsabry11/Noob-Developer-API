using Microsoft.AspNetCore.Mvc;
using ViewModels.ViewModels;
using DomainLayer.Entities;
using WebTool.Services.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebTool.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Test")]
    public class TestController:ControllerBase
    {

        public TestController()
        {

        }
        [HttpGet("Hello")]
        public ActionResult <string> sendMessage()
        {
            string s = "Hello ahmed";
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null)
            {
                Console.WriteLine("User ID: null" );
            }
            Console.WriteLine("User ID: "+userId);
            return Ok(s+"  "+ userId);
        }

    }
}
