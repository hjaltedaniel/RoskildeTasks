using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeTasks.Api.Models
{
    public class TaskItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime deadline { get; set; }
        public JObject category { get; set; }
        public string description { get; set; }
        public JObject editor { get; set; }
    }
}
