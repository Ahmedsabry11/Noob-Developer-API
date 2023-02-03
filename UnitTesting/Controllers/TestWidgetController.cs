using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;
using WebTool.Controllers;
using WebTool.Services.Services;
using WebTool.Services.IServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace UnitTesting.Controllers
{
    public class TestWidgetController
    {
        public TestWidgetController()
        {

        }
        [Fact]
        public async Task Test1()
        {

            // Arrange
            int id = 1;
            var services = new Mock<IWidgetServices>();
            services.Setup(_ => _.GetAllChildWidgetCodes(id)).ReturnsAsync(new List<WidgetDTO>());
            var widgetcontroller = new WidgetController(services.Object);

            // Act
            var result = await widgetcontroller.getchilds(id);

            // Assert
            result.GetType().Should().Be(typeof(ActionResult<List<WidgetDTO>>));
            //(result as ActionResult<List<WidgetDTO>>)..Should().Be(200);
        }
        [Fact]
        public async Task NotFoundAttribute()
        {

            // Arrange
            int id = 1;
            var services = new Mock<IWidgetServices>();
            List<AttributesDTO> list = null;
            services.Setup(_ => _.GetWidgetAttriubtes(id)).ReturnsAsync(list);
            var widgetcontroller = new WidgetController(services.Object);

            // Act
            var result = await widgetcontroller.GetWidgetAttributes(id);

            // Assert
            result.GetType().Should().Be(typeof(ActionResult<List<AttributesDTO>>));
            
            // Assert
            var contentResult = Assert.IsType<ActionResult<List<AttributesDTO>>>(result);
            //Assert.Equal(404, contentResult.);
            contentResult.Result.Should().NotBeNull();
            Assert.Equal(null, contentResult.Value);
            //contentResult.Result.
            var redirectToActionResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            redirectToActionResult.Value.Should().Be("invaild widget id");
            //Assert.Null(redirectToActionResult.ControllerName);
            //Assert.Equal("invaild widget id", redirectToActionResult.ActionName);
        }

    }
}
