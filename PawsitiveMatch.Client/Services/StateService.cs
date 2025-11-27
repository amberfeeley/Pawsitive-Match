using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using PawsitiveMatch.SharedModels.Models;

namespace PawsitiveMatch.Services
{
    public class StateService : AuthenticationStateProvider
    {
        private User? _currentUser;
        public User? CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsPrincipal principal;

            if (_currentUser != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, _currentUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, _currentUser.Email),
                    new Claim("firstName", _currentUser.FirstName),
                    new Claim("lastName", _currentUser.LastName)
                }, "PawsitiveAuth");

                principal = new ClaimsPrincipal(identity);
            }
            else
            {
                principal = new ClaimsPrincipal(new ClaimsIdentity());
            }

            return Task.FromResult(new AuthenticationState(principal));
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Logout()
        {
            _currentUser = null;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
