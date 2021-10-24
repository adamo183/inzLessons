using inzLessons.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace inzLessons.Client.Services
{
    using inzLessons.Shared.Login;
    using inzLessons.Shared.Register;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    public interface ILoginServices
    {
        public Task<LoginResponse> UserLogin(LoginRequest param);
        public Task<bool> RegisterUser(RegisterRequest register);
    }

    public class LoginServices : ILoginServices
    {
       private  HttpClient _http;

        public LoginServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> RegisterUser(RegisterRequest register) 
        {
            var elemToSend = JsonConvert.SerializeObject(register);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("Login/register", content);
            if (!respond.IsSuccessStatusCode)
                return false;
            else
                return true;
        }
        public async Task<LoginResponse> UserLogin(LoginRequest param) 
        {
            var elemToSend = JsonConvert.SerializeObject(param);
            var content = new StringContent(elemToSend, Encoding.UTF8, "application/json");
            var respond = await _http.PostAsync("", content);
            if (!respond.IsSuccessStatusCode)
                return null;
            else
            {
                string retElem = await respond.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginResponse>(retElem);
            }
        }
    }
}
