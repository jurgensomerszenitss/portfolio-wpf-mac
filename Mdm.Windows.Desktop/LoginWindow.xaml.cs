using System;
using System.Windows;

namespace Mdm.Windows.Desktop
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();  
            loginView.ViewModel.OnLogin += ViewModel_OnLogin;
        }

        private bool _shutDown = true;

        
        private void ViewModel_OnLogin(object sender, bool ok)
        {
            _shutDown = !ok;
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                this.Close();
            });
             
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if(_shutDown)
                Application.Current.Shutdown();
        }
    }
}
