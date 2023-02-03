using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace WebTool.Services.IServices
{
    public interface IWidgetServices
    {
        public Task<List<WidgetDTO>> GetAllChildWidgetCodes(int? id);
        public Task<List<AttributesDTO>> GetWidgetAttriubtes(int id);
    }
}
