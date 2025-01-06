using Business.Models;

namespace Business.Interfaces
{
    public interface IFileService
    {
        List<User> LoadListFromFile();
        bool SaveListToFile(List<User> list);
    }
}