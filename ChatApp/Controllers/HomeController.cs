using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Rooms(string nickname, string password)
        {
            var cookie = new HttpCookie("ChatAppNickname", nickname) { Expires = DateTime.Now.AddMinutes(30) };
            if (Request.Cookies.AllKeys.Contains("ChatAppNickname"))
                Response.SetCookie(cookie);
            else
                Response.Cookies.Add(cookie);

            if (Lobby.Rooms == null)
                Lobby.LoadDemoRooms();

            if (Lobby.Log == null)
                Lobby.Log = new Dictionary<string, string>();

            return View(Lobby.Rooms);
        }
        
        public ActionResult Chat(string room)
        {
            var user = Request.Cookies["ChatAppNickname"].Value;
            if (string.IsNullOrEmpty(room))
            {
                if (Lobby.Rooms == null)
                    Lobby.Rooms = new List<RoomModel>();

                var newRoom = new RoomModel { Name = "Room-" + (Lobby.Rooms.Count + 1), Chat = new ChatHandler(), Users = new List<string> { user } };
                Lobby.Rooms.Add(newRoom);
                return View(newRoom);
            }
            var chat = Lobby.Rooms.First(r => r.Name == room);
            chat.Users.Add(user);
            return View(chat);
        }

        public void WriteLog(string message, string room)
        {
            if (Lobby.Log.ContainsKey(room))
                Lobby.Log[room] += message;
            else
                Lobby.Log.Add(room, message);
        }

        public string ReadLog(string room)
        {
            return Lobby.Log.ContainsKey(room) ? Lobby.Log[room] : string.Empty;
        }
    }
}