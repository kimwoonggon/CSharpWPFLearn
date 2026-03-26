namespace WpfMvvmLearn.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        Counter = new CounterViewModel();
        Login = new LoginViewModel();
    }

    public CounterViewModel Counter { get; }

    public LoginViewModel Login { get; }
}