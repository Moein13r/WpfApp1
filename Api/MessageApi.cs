using Chat.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WpfApp1.Api
{
    public class MessageApi
    {
        const string baseUrl = "https://localhost:7033/Room/";
        public async ValueTask<Room[]> GetRoomsByUserId(int userId)
        {
            try
            {
                string result = await ApiBase.Get(baseUrl + $"GetRoomsByUserId?userId={userId}");
                var Rooms = JsonSerializer.Deserialize<Room[]>(result);
                return Rooms;
            }
            catch (Exception e)
            {
                throw new NotImplementedException(e.Message);
            }
        }
        public async ValueTask<Message[]> GetRoomMessages(int roomId)
        {
            try
            {
                string result = await ApiBase.Get(baseUrl + $"GetRoomMessages?roomId={roomId}");
                var Messages = JsonSerializer.Deserialize<Message[]>(result);
                return Messages;
            }
            catch (Exception e)
            {
                throw new NotImplementedException(e.Message);
            }
        }
    }
}
