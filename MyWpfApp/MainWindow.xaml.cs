using System.Windows;

namespace MyWpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        var userName = UserNameTextBox.Text;
        var password = PasswordTextBox.Text;

        MessageBox.Show(
            $"아이디: {userName}\n비밀번호: {password}",
            "입력한 로그인 정보",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}