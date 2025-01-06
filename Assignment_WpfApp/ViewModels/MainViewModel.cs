using CommunityToolkit.Mvvm.ComponentModel;

namespace Assignment_WpfApp.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;
}