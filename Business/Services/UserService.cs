using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly IFileService _fileService;
    private List<User> _users = [];

    public UserService(IFileService fileService)
    {
        _fileService = fileService;
    }


    public bool AddUser(UserRegistrationForm registrationForm)
    {
        try
        {
            User user = UserFactory.Create(registrationForm);
            _users.Add(user);
            _fileService.SaveListToFile(_users);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public IEnumerable<User> GetUsers()
    {
        _users = _fileService.LoadListFromFile();
        return _users;
    }

    public bool DeleteUser(string id)
    {
        var users = _fileService.LoadListFromFile();
        var user = users.FirstOrDefault(user => user.Id == id);

        if (user == null)
        {
            return false;
        }

        try
        {
            users.Remove(user);
            _fileService.SaveListToFile(users);
            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }


    public bool UpdateUser(string id, User updatedUser)
    {
        var users = _fileService.LoadListFromFile();
        var user = users.FirstOrDefault(user => user.Id == id);

        if (user == null)
        {
            return false;
        }

        try
        {
            users[users.IndexOf(user)] = updatedUser;
            
            _fileService.SaveListToFile(users);
            return true;
        }

        catch (Exception ex)
        {
            return false;
        }
    }
}
