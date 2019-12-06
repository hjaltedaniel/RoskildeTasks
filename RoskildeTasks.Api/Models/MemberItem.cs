using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeTasks.Api.Models
{
    public class MemberItem
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string[] AccessGroups { get; set; }
    }
}
