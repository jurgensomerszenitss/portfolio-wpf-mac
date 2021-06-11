using Mdm.App.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mdm.App.ViewModels
{
    public interface IBaseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        bool IsValid { get; }
        Task Initialize();
    }

    public abstract class BaseViewModel : IBaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            RaisePropertyChanged(propertyName);
            CommandManager.RefreshCommandStates();
            return true;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
        public virtual bool IsValid
        {
            get => true;
        }


        public string this[string name]
        {
            get
            {
                return GetPropertyError(name);
            }
        }

        protected virtual string GetPropertyError(string propertyName)
        {
            return null;
        } 

        public virtual async Task Initialize()
        {
            await Task.Yield();
        }
    }
}
