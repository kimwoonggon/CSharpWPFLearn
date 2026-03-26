using System.Windows.Input;
using WpfMvvmLearn.Commands;

namespace WpfMvvmLearn.ViewModels;

public sealed class LoginViewModel : ViewModelBase
{
    private readonly RelayCommand loginCommand;
    private string userName = string.Empty;
    private string password = string.Empty;
    private string loginStatus = "아이디와 비밀번호를 입력하면 로그인 버튼이 활성화됩니다.";

    public LoginViewModel()
    {
        loginCommand = new RelayCommand(Login, CanLogin);
    }

    public string UserName
    {
        get => userName;
        set
        {
            if (SetProperty(ref userName, value))
            {
                loginCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string Password
    {
        get => password;
        set
        {
            if (SetProperty(ref password, value))
            {
                loginCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public string LoginStatus
    {
        get => loginStatus;
        private set => SetProperty(ref loginStatus, value);
    }

    public ICommand LoginCommand => loginCommand;

    private bool CanLogin()
    {
        return !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);
    }

    private void Login()
    {
        LoginStatus = $"{UserName}님, 로그인 명령이 실행되었습니다.";
    }
}