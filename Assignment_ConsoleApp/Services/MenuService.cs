using Assignment_ConsoleApp.Interfaces;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using System.ComponentModel.DataAnnotations;

namespace Assignment_ConsoleApp.Services;

public class MenuService : IMenuService
{

    public MenuService(IContactService contactService)
    {
        _contactService = (ContactService?)contactService!;
    }

    private readonly ContactService _contactService;
    public void ShowMenu()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-------- MAIN MENU --------\n");
            Console.WriteLine($"{"1.",-3}VIEW ALL CONTACTS"); ;
            Console.WriteLine($"{"2.",-3}ADD A NEW CONTACT"); ;
            Console.WriteLine($"{"Q.",-3}QUIT APPLICATION"); ;
            Console.WriteLine("\n---------------------------\n");
            Console.Write("SELECTED OPTION: ");
            var option = Console.ReadLine()!;

            switch (option.ToLower())
            {
                case "1":
                    ViewContactsDialog();
                    break;
                case "2":
                    AddNewContactDialog();
                    break;
                case "q":
                    QuitApplicationDialog();
                    break;
                default:
                    InvalidOptionDialog();
                    break;
            }
        }

    }

    public void ViewContactsDialog()
    {
        Console.Clear();
        Console.WriteLine("------ VIEW CONTACTS ------\n");

        var contacts = _contactService.GetContacts();

        foreach (var contact in contacts)
        {
            Console.WriteLine($"{"Id:",-10}{contact.Id}");
            Console.WriteLine($"{"Name:",-10}{contact.FirstName} {contact.LastName}");
            Console.WriteLine($"{"Email:",-10}{contact.Email}");
            Console.WriteLine($"{"Phone:",-10}{contact.Phone}");
            Console.WriteLine($"{"Address:",-10}{contact.StreetAddress}, {contact.PostalCode} {contact.City}\n");
        }

        Console.ReadKey();
    }

    public void AddNewContactDialog()
    {
        Console.Clear();
        Console.WriteLine("----- ADD CONTACT -----\n");

        ContactRegistrationForm contact = ContactFactory.Create();

        contact.FirstName = PromptAndValidate("Enter your first name: ", nameof(contact.FirstName));
        contact.LastName = PromptAndValidate("Enter your last name: ", nameof(contact.LastName));
        contact.Email = PromptAndValidate("Enter your email: ", nameof(contact.Email));
        contact.Phone = PromptAndValidate("Enter your phone number: ", nameof(contact.Phone));
        contact.StreetAddress = PromptAndValidate("Enter your street address: ", nameof(contact.StreetAddress));
        contact.PostalCode = PromptAndValidate("Enter your postal code: ", nameof(contact.PostalCode));
        contact.City = PromptAndValidate("Enter your city: ", nameof(contact.City));

        _contactService.AddContact(contact);
    }

    public void QuitApplicationDialog()
    {
        Console.Clear();
        Console.WriteLine("Are you sure you want to close the application? (y/n)");
        var option = Console.ReadLine()!.ToLower();
        if (option == "y")
        {
            Console.Clear();
            Console.Write("Goodbye!");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }

    public void InvalidOptionDialog()
    {
        Console.WriteLine("");
        Console.Write("Invalid option selected. Please try again.");
        Console.ReadKey();
    }

    public string PromptAndValidate(string prompt, string propertyName)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine() ?? string.Empty;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(new ContactRegistrationForm()) { MemberName = propertyName };

            //If validation is ok, input is returned and saved in to the form.
            if (Validator.TryValidateProperty(input, context, results))
            {
                return input;
            }
            //If validation fails, a error message is displayed and the user is asked to try again.
            else
            {
                Console.WriteLine($"{results[0].ErrorMessage}. Please try again.\n");
            }
        }
    }
}
