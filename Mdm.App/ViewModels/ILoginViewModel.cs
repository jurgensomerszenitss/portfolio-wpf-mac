using System;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public interface ILoginViewModel : IBaseViewModel
    {
        string UserName { get; set; }
        string Password { get; set; }
        bool Remember { get; set; }
        bool IsLoginOk { get; }
        string LoginResult { get; }

        ICommand LoginCommand { get;  } 
        ICommand ForgotCommand { get; }

        event EventHandler<bool> OnLogin; 
    }
}
