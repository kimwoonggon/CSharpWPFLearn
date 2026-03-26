namespace WpfMvvmLearn.ViewModels;

public sealed class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        Counter = new CounterViewModel();
        Login = new LoginViewModel();
        Todo = new TodoViewModel();
    }

    public CounterViewModel Counter { get; }

    public LoginViewModel Login { get; }

    public TodoViewModel Todo { get; }
}