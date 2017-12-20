using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class RoomModel
    {
        public string Name { get; set; }
        public IList<string> Users { get; set; }
        public ChatHandler Chat { get; set; }
    }
}