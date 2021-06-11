using Mdm.App.ViewModels;
using System;
using System.Windows.Controls;

namespace Mdm.Windows.Desktop.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            DataContext = App.Container.GetInstance<ILoginViewModel>();
            InitializeComponent();
        }

        internal ILoginViewModel ViewModel => ((ILoginViewModel)DataContext);
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            await ViewModel.Initialize(); 

        }
    }
}
