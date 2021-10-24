using inzLessons.Shared.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using inzLessons.Client.Services;

namespace inzLessons.Client.Pages.Register
{
    public partial class RegisterUser
    {
        
        RegisterRequest _registerRequest = new RegisterRequest();
        private bool popup;
        private string _repeatPassword = "";
        private int value = 1;

        protected override async Task OnInitializedAsync()
        {
        }

        private void SendAccount()
        {
        }

        async void OnSubmit(RegisterRequest registerRequest)
        {
            await _loginServices.RegisterUser(_registerRequest);
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
        }
    }
}
