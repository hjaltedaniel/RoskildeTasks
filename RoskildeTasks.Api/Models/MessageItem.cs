﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoskildeTasks.Api.Models
{
    public class MessageItem
    {
        public string MemberUdi { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool isFromAdmin { get; set; }
    }
}
