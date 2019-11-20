using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeTasks.Api.Models.DTO
{
    public class TaskMessageItem:MessageItem
    {
        public int TaskId { get; set; }
    }
}
