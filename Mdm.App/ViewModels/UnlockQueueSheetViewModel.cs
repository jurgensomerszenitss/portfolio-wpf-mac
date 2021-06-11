using Mdm.App.Infrastructure;
using Mdm.Core.Models;
using Mdm.Core.Operations;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mdm.App.ViewModels
{
    public class UnlockQueueSheetViewModel : BaseViewModel, IUnlockQueueSheetViewModel
    {
        public UnlockQueueSheetViewModel(IOperations operations, IStateManager stateManager)
        {
            _operations = operations;
            _stateManager = stateManager;
            UnlockCommand = new RelayCommand(Unlock, CanUnlock);
            CancelCommand = new RelayCommand(Cancel);

        }

        private readonly IOperations _operations;
        private readonly IStateManager _stateManager;
        private int? _digit1;
        private int? _digit2;
        private int? _digit3;
        private int? _digit4;
        private bool _hasError;

        public int? Digit1
        {
            get => _digit1;
            set => SetProperty(ref _digit1, value);
        }

        public int? Digit2
        {
            get => _digit2;
            set => SetProperty(ref _digit2, value);
        }

        public int? Digit3
        {
            get => _digit3;
            set => SetProperty(ref _digit3, value);
        }

        public int? Digit4
        {
            get => _digit4;
            set => SetProperty(ref _digit4, value);
        }

        public bool HasError
        {
            get => _hasError;
            private set => SetProperty(ref _hasError, value);
        }

        public ICommand UnlockCommand { get; }

        public ICommand CancelCommand { get; }

        public event EventHandler OnCancel;
        public event EventHandler OnUnlock;

        public override Task Initialize()
        {
            Reset();
            return base.Initialize();
        }

        public void Reset()
        {
            HasError = false;
            Digit1 = null;
            Digit2 = null;
            Digit3 = null;
            Digit4 = null;
        }

        private async Task Unlock()
        {
            var code = int.Parse($"{_digit1}{_digit2}{_digit3}{_digit4}");
            var succeed = await _operations.Remote.Unlock(_stateManager.UnlockParam, code);
            if (succeed)
            {
                Reset();
                OnUnlock?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                HasError = true;
            }
        }

        private bool CanUnlock()
        {
            return _digit1 != null &&
                   _digit2 != null &&
                   _digit3 != null &&
                   _digit4 != null;
        }

        private async Task Cancel()
        {
            Reset();
            OnCancel?.Invoke(this, EventArgs.Empty);
            await Task.Yield();
        }

    }
}
