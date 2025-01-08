using Assignment_ConsoleApp.Interfaces;
using Assignment_ConsoleApp.Services;
using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
{
    services.AddSingleton<IFileService, FileService>();
    services.AddSingleton<IContactService, ContactService>();
    services.AddSingleton<IMenuService, MenuService>();
})
    .Build();

var menuService = host.Services.GetRequiredService<IMenuService>();

menuService.ShowMenu();