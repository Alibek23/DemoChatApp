using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApp.Models
{
    public class Lobby
    {
        public static void LoadDemoRooms()
        {
            Rooms = new List<RoomModel>{
                new RoomModel { Name = "Room-1", Chat = new ChatHandler(), Users = new List<string>() },
                new RoomModel { Name = "Room-2", Chat = new ChatHandler(), Users = new List<string>() },
                new RoomModel { Name = "Room-3", Chat = new ChatHandler(), Users = new List<string>() }
            };
        }
        
        public static IList<RoomModel> Rooms { get; set; }

        public static IDictionary<string, string> Log { get; set; }
    }
}