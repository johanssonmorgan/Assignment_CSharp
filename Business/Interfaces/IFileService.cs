using Business.Models;

namespace Business.Interfaces
{
    public interface IFileService
    {
        List<Contact> LoadListFromFile();
        bool SaveListToFile(List<Contact> list);
    }
}