using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        bool AddUser(UserRegistrationForm registrationForm);
        IEnumerable<User> GetUsers();
        bool DeleteUser(string id);
        bool UpdateUser(string userId, User updatedUser);
    }
}