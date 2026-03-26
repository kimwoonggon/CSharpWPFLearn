using System.Collections.ObjectModel;
using System.Windows;

namespace WpfStyleAndTemplateLearn;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<User> Users { get; } = new();

    public MainWindow()
    {
        InitializeComponent();

        Users.Add(new User { Name = "홍길동", Email = "hong@example.com" });
        Users.Add(new User { Name = "김철수", Email = "kim@example.com" });
        Users.Add(new User { Name = "이영희", Email = "lee@example.com" });

        DataContext = this;
    }
}