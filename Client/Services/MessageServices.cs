using inzLessons.Shared.Message;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Client.Services
{
    public interface IMessageServices
    {
        public Task<List<MessageDTO>> GetMessageInReservation(int reservationId);
        public Task<bool> InsertMessageInReservation(MessageDTO message);
    }

    public class MessageServices : IMessageServices
    {
        private HttpClient _http;

        public MessageServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MessageDTO>> GetMessageInReservation(int reservationId)
        {
            var respond = await _http.GetAsync("Message/" + reservationId);
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MessageDTO>>(retElem);
            }
        }

        public async Task<bool> InsertMessageInReservation(MessageDTO message)
        {
            var elemToSend = JsonConvert.SerializeObject(message);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("Message", content);
            if (!respond.IsSuccessStatusCode)
                return false;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return bool.Parse(retElem);
            }
        }

    }
}
