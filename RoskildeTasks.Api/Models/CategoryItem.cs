using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeTasks.Api.Models
{
    public class CategoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public ColorItem Color { get; set; }
        public string StandardMessage { get; set; }
        public bool isOnlyMessages { get; set; }
    }
}
