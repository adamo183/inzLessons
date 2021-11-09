using inzLessons.Shared.Reservation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Client.Services
{
    public interface IReservationServices
    {
        public Task<bool> AddReservationToUser(ReservationDTO reservationDTO);
    }

    public class ReservationServices : IReservationServices
    {
        private HttpClient _http;

        public ReservationServices(HttpClient http)
        {
            _http = http;
        }


        public async Task<bool> AddReservationToUser(ReservationDTO reservationDTO)
        {
            var elemToSend = JsonConvert.SerializeObject(reservationDTO);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("Reservation", content);
            if (!respond.IsSuccessStatusCode)
                return false;
            else
                return true;
        }
    }
}
