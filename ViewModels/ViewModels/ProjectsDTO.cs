using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ViewModels
{
    public class ProjectsDTO
    {
        public string Title { get; set; }
        public string? GeneratedAppPath { get; set; }
        public string? Code { get; set; }
        public string? Widgets { get; set; }

        public string? Description { get; set; }

    }
}
