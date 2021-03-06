using inzLessons.Shared.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace inzLessons.Client.Services
{
    public interface IUserServices
    {
        public Task<List<UserDTO>> GetUserList();
        public Task<List<UserDTO>> GetUserInGroup(int id);
    }
    public class UserServices : IUserServices
    {
        private HttpClient _http;

        public UserServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UserDTO>> GetUserList()
        {
            var respond = await _http.GetAsync("Users/studentToSelect");
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserDTO>>(retElem); ;
            }
        }

        public async Task<List<UserDTO>> GetUserInGroup(int id)
        {
            var respond = await _http.GetAsync("Users/Group/"+ id);
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<UserDTO>>(retElem); ;
            }
        }
    }
}
