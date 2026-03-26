using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMvvmLearn.Commands;

namespace WpfMvvmLearn.ViewModels;

public sealed class TodoViewModel : ViewModelBase
{
    private readonly RelayCommand addCommand;
    private readonly RelayCommand deleteCommand;
    private string newTodoText = string.Empty;
    private string? selectedTodo;

    public TodoViewModel()
    {
        Todos = new ObservableCollection<string>
        {
            "MVVM 바인딩 복습",
            "RelayCommand 동작 확인"
        };

        addCommand = new RelayCommand(AddTodo, CanAddTodo);
        deleteCommand = new RelayCommand(DeleteTodo, CanDeleteTodo);
    }

    public ObservableCollection<string> Todos { get; }

    public string NewTodoText
    {
        get => newTodoText;
        set
        {
            if (SetProperty(ref newTodoText, value))
            {
                addCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string? SelectedTodo
    {
        get => selectedTodo;
        set
        {
            if (SetProperty(ref selectedTodo, value))
            {
                deleteCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public ICommand AddCommand => addCommand;

    public ICommand DeleteCommand => deleteCommand;

    private bool CanAddTodo()
    {
        return !string.IsNullOrWhiteSpace(NewTodoText);
    }

    private void AddTodo()
    {
        string todoText = NewTodoText.Trim();

        if (todoText.Length == 0)
        {
            return;
        }

        Todos.Add(todoText);
        NewTodoText = string.Empty;
    }

    private bool CanDeleteTodo()
    {
        return !string.IsNullOrWhiteSpace(SelectedTodo);
    }

    private void DeleteTodo()
    {
        if (SelectedTodo is null)
        {
            return;
        }

        string todoToDelete = SelectedTodo;
        Todos.Remove(todoToDelete);
        SelectedTodo = null;
    }
}