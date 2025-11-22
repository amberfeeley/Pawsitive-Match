using PawsitiveMatch.SharedModels;

namespace PawsitiveMatch.Services
{
    public class StateService
    {
        private User? _currentUser;
        public event Action? OnChange;

        public User? CurrentUser => _currentUser;
        public bool IsLoggedIn => _currentUser != null;

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
            OnChange?.Invoke();
        }

        public void Logout()
        {
            _currentUser = null;
            OnChange?.Invoke();
        }
    }
}