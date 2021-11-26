﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL.Models.Chat
{
    public class ChatDto
    {
       
        public string Sub { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
