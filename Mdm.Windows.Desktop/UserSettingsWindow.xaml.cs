using System;
using System.Windows;

namespace Mdm.Windows.Desktop
{
    /// <summary>
    /// Interaction logic for UserSettingsWindow.xaml
    /// </summary>
    public partial class UserSettingsWindow : Window
    {
        public UserSettingsWindow()
        {
            InitializeComponent();

            userSettingsView.ViewModel.OnSave += ViewModel_OnSave;
            userSettingsView.ViewModel.OnCancel += ViewModel_OnCancel; 
        }

       
        private void ViewModel_OnCancel(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                this.Close();
            });
        }

        private void ViewModel_OnSave(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
               // DialogResult = true;
                this.Close();
            });
        }
    }
}
