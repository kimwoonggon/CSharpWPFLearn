using System.Windows.Input;
using WpfMvvmLearn.Commands;

namespace WpfMvvmLearn.ViewModels;

public sealed class CounterViewModel : ViewModelBase
{
    private int count;

    public CounterViewModel()
    {
        IncrementCommand = new RelayCommand(Increment);
    }

    public int Count
    {
        get => count;
        set => SetProperty(ref count, value);
    }

    public ICommand IncrementCommand { get; }

    private void Increment()
    {
        Count++;
    }
}