using System.Windows;

namespace WpfDataBindingLearn;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new User
        {
            Name = "홍길동"
        };
    }
}