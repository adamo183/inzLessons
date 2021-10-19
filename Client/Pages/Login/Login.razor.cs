using inzLessons.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inzLessons.Client.Pages.Login
{
    public partial class Login
    {
        private LoginRequest _login = new LoginRequest();

        protected override async Task OnInitializedAsync()
        {
        }

        private void LoginAction()
        {
        }

        private void NavigateToRegister()
        {
            NavigationManager.NavigateTo("/register");
        }
    }
}
