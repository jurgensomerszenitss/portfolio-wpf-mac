using Mdm.App.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Mdm.Windows.Desktop.Views
{
    /// <summary>
    /// Interaction logic for UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : System.Windows.Controls.UserControl
    {
        public UserSettingsView()
        {
            DataContext = App.Container.GetInstance<IUserSettingsViewModel>();
            InitializeComponent();
        }

        internal IUserSettingsViewModel ViewModel => ((IUserSettingsViewModel)DataContext);
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            await ViewModel.Initialize();
            ViewModel.OnSelectFolder += ViewModel_OnSelectFolder;
        }

        private void ViewModel_OnSelectFolder(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            { 
                using (var dialog = new FolderBrowserDialog())
                {
                    var result = dialog.ShowDialog();
                    if (result == DialogResult.OK) 
                    {
                        ViewModel.FileLocation = dialog.SelectedPath;
                    }
                }
            });
        }
 
    }
}
