using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeTasks.Api.Models
{
    public class RessourceItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Filetype { get; set; }
        public CategoryItem Category { get; set; }
    }
}
