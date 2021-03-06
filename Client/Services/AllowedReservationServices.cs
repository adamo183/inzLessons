using inzLessons.Shared.AllowedReservation;
using inzLessons.Shared.ReservationRequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Client.Services
{
    public interface IAllowedReservationServices
    {
        public Task<bool> CheckAllowedReservationAvailable(AllowedReservationDTO model);
        public Task<bool> AddAllowedTeacherHours(AllowedReservationDTO allowedReservationDTO);
        public Task<bool> EditAllowedTeacherHours(AllowedReservationDTO allowedReservationDTO);
        public Task<bool> AddReservationRequest(ReservationRequestDTO request);
        public Task<List<AllowedReservationDTO>> GetAllowedReservationsToTeacher();
        public Task<List<AllowedReservationDTO>> GetAllowedReservationsToStudent();
        public Task<List<AllowedReservationDTO>> GetAllowedReservationsByTeacherId(int id);
        public Task<List<ReservationRequestDTO>> GetStudentReservationRequest();
        public Task<List<ReservationRequestDTO>> GetTeacherReservationRequest();
        public Task<bool> AcceptLessonRequest(ReservationRequestDTO requestDTO);
        public Task<bool> RejectLessonRequest(ReservationRequestDTO requestDTO);
    }

    public class AllowedReservationServices : IAllowedReservationServices
    {
        private readonly HttpClient _httpClient;

        public AllowedReservationServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AcceptLessonRequest(ReservationRequestDTO requestDTO)
        {
            var elemToSend = JsonConvert.SerializeObject(requestDTO);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PutAsync("AllowedReservation/AcceptRequest", content);
            var status = await respond.Content.ReadAsStringAsync();
            return bool.Parse(status);
        }

        public async Task<bool> RejectLessonRequest(ReservationRequestDTO requestDTO)
        {
            var elemToSend = JsonConvert.SerializeObject(requestDTO);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PutAsync("AllowedReservation/RejectRequest", content);
            var status = await respond.Content.ReadAsStringAsync();
            return bool.Parse(status);
        }

        public async Task<List<ReservationRequestDTO>> GetTeacherReservationRequest()
        {
            var respond = await _httpClient.GetAsync("AllowedReservation/TeacherRequest");
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ReservationRequestDTO>>(retElem);
            }
        }

        public async Task<List<ReservationRequestDTO>> GetStudentReservationRequest()
        {
            var respond = await _httpClient.GetAsync("AllowedReservation/UserRequest");
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ReservationRequestDTO>>(retElem);
            }
        }

        public async Task<bool> AddReservationRequest(ReservationRequestDTO request)
        {
            var elemToSend = JsonConvert.SerializeObject(request);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PostAsync("AllowedReservation/Request", content);
            var status = await respond.Content.ReadAsStringAsync();
            return bool.Parse(status);
        }

        public async Task<bool> EditAllowedTeacherHours(AllowedReservationDTO allowedReservationDTO)
        {
            var elemToSend = JsonConvert.SerializeObject(allowedReservationDTO);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PutAsync("AllowedReservation", content);
            var status = await respond.Content.ReadAsStringAsync();
            return bool.Parse(status);
        }

        public async Task<List<AllowedReservationDTO>> GetAllowedReservationsToStudent()
        {
            var respond = await _httpClient.GetAsync("AllowedReservation/Student");
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AllowedReservationDTO>>(retElem);
            }
        }

        public async Task<List<AllowedReservationDTO>> GetAllowedReservationsToTeacher()
        {
            var respond = await _httpClient.GetAsync("AllowedReservation/Teacher");
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AllowedReservationDTO>>(retElem);
            }
        }

        public async Task<List<AllowedReservationDTO>> GetAllowedReservationsByTeacherId(int id)
        {
            var respond = await _httpClient.GetAsync("AllowedReservation/Teacher/" + id);
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<AllowedReservationDTO>>(retElem);
            }
        }


        public async Task<bool> AddAllowedTeacherHours(AllowedReservationDTO allowedReservationDTO)
        {
            var elemToSend = JsonConvert.SerializeObject(allowedReservationDTO);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PostAsync("AllowedReservation", content);
            var status = await respond.Content.ReadAsStringAsync();
            return bool.Parse(status);
        }

        public async Task<bool> CheckAllowedReservationAvailable(AllowedReservationDTO model)
        {
            var elemToSend = JsonConvert.SerializeObject(model);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _httpClient.PostAsync("AllowedReservation/CheckAvailable", content);
            var status = await respond.Content.ReadAsStringAsync();
            return bool.Parse(status);
        }
    }
}
