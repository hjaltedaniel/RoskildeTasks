using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeTasks.Api.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<EditorItem> Editor { get; set; }
        public List<AnswerRootItem> Answers { get; set; }
        public DateTime Deadline { get; set; }
        public CategoryItem Category { get; set; }
        public bool isDone { get; set; }
    }
}
