using System;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public interface IUnlockQueueSheetViewModel : IBaseViewModel
    {
        int? Digit1 { get; set; }
        int? Digit2 { get; set; }
        int? Digit3 { get; set; }
        int? Digit4 { get; set; }
        ICommand UnlockCommand { get; }
        ICommand CancelCommand { get; } 

        bool HasError { get; }

        event EventHandler OnCancel;
        event EventHandler OnUnlock; 
    }
}
