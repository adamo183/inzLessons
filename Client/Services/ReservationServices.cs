using inzLessons.Shared.Reservation;
using inzLessons.Shared.Users;
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
        public Task<List<ReservationDTO>> GetTeacherReservation(ReservationParams param);
        public Task<List<ReservationDTO>> GetStudentReservation(ReservationParams param);
        public Task<bool> CheckTeacherHourAvaiable(ReservationParams param);
        public Task<List<UserDTO>> GetStudentTeacher();
    }

    public class ReservationServices : IReservationServices
    {
        private HttpClient _http;

        public ReservationServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserDTO>> GetStudentTeacher()
        {
            var respond = await _http.GetAsync("Reservation/StudentTeacher");
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserDTO>>(retElem); ;
            }
        }
        public async Task<List<ReservationDTO>> GetStudentReservation(ReservationParams param)
        {
            var elemToSend = JsonConvert.SerializeObject(param);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("Reservation/Student", content);
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ReservationDTO>>(retElem); ;
            }
        }

        public async Task<List<ReservationDTO>> GetTeacherReservation(ReservationParams param)
        {
            var elemToSend = JsonConvert.SerializeObject(param);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("Reservation/Teacher", content);
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ReservationDTO>>(retElem); ;
            }
        }

        public async Task<bool> CheckTeacherHourAvaiable(ReservationParams param)
        {
            var elemToSend = JsonConvert.SerializeObject(param);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("Reservation/IsHourAvailable", content);
            if (!respond.IsSuccessStatusCode)
                return false;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return bool.Parse(retElem);
            }
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
