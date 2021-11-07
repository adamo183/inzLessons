using inzLessons.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using inzLessons.Shared.Role;

namespace inzLessons.Client.Pages.Login
{
    public partial class Login
    {
        LoginRequest _login = new LoginRequest();
        private bool popup;
        private bool _inncorectLoginData = false;

        protected override async Task OnInitializedAsync()
        {
        }

        private async void LoginAction()
        {
        }

        private void NavigateToRegister()
        {
            NavigationManager.NavigateTo("/register");
        }

        async void OnSubmit(LoginRequest loginRequest)
        {
            _inncorectLoginData = false;
            var loginResponse = await _loginServices.UserLogin(loginRequest);

            if (loginResponse != null && !String.IsNullOrEmpty(loginResponse.Token))
            {
                authenticationService.NotifyUserAuthentication(loginResponse.Token);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResponse.Token);
                await localStorage.ClearAsync();
                await localStorage.SetItemAsync("UserName", loginResponse.Username);
                await localStorage.SetItemAsync("Token", loginResponse.Token);
                await localStorage.SetItemAsync("PersonelId", loginResponse.Id);
                await localStorage.SetItemAsync("Role", loginResponse.Role);



                if (loginResponse.Role == RoleEnum.Student)
                {
                    NavigationManager.NavigateTo("/student");
                }
                else if (loginResponse.Role == RoleEnum.Teacher)
                {
                    NavigationManager.NavigateTo("/teacher");
                }
            }
            else
            {
                _inncorectLoginData = true;
                StateHasChanged();
            }
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
        }
    }
}
