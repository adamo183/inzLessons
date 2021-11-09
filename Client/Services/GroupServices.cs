using inzLessons.Shared.Group;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace inzLessons.Client.Services
{
    public interface IGroupServices
    {
        public Task<List<GroupWithUsersDTO>> GetTeacherGroup();
        public Task<bool> CreateNewGroup(LessonsGroupDTO group);
        public Task<GroupWithUsersDTO> GetGroupDataById(string Id);
    }

    public class GroupServices : IGroupServices
    {
        private HttpClient _http;

        public GroupServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<GroupWithUsersDTO> GetGroupDataById(string Id)
        {
            var respond = await _http.GetAsync("Group/" + Id);
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GroupWithUsersDTO>(retElem); ;
            }
        }

        public async Task<bool> CreateNewGroup(LessonsGroupDTO group)
        {
            var elemToSend = JsonConvert.SerializeObject(group);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("Group", content);
            if (!respond.IsSuccessStatusCode)
                return false;
            else
                return true;
        }

        public async Task<List<GroupWithUsersDTO>> GetTeacherGroup()
        {
            var respond = await _http.GetAsync("Group");
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<GroupWithUsersDTO>>(retElem); ;
            }
        }
    }
}
