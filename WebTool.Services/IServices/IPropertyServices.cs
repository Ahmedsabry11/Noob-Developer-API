using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.ViewModels;

namespace WebTool.Services.IServices
{
    public  interface IPropertyServices
    {
        public Task<List<PropertyDTO>> TestStyles(int id);
    }
}
