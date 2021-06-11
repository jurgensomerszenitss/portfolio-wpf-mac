using Mdm.App.ViewModels;
using Mdm.Core.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Mdm.Windows.Desktop.Views
{
    /// <summary>
    /// Interaction logic for QueueView.xaml
    /// </summary>
    public partial class QueueSheetView : UserControl
    {
        public QueueSheetView()
        {
            this.Loaded += QueueSheetView_Loaded;

            DataContext = App.Container.GetInstance<IQueueSheetViewModel>();
            InitializeComponent();          
        }

        private IQueueSheetViewModel ViewModel => ((IQueueSheetViewModel)DataContext);

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e); 
            ViewModel.OnShowSettings += ViewModel_OnShowSettings;
            ViewModel.OnShowLogin += ViewModel_OnShowLogin;
            ViewModel.OnUnlock += ViewModel_OnUnlock; 
        }

        private async void QueueSheetView_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.Initialize();
        }

        private void ViewModel_OnUnlock(object sender, QueueSheet e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                var unlockQueueSheetWindow = new UnlockQueueSheetWindow();
                var mainWindow = Application.Current.MainWindow;
                unlockQueueSheetWindow.Owner = mainWindow;
                unlockQueueSheetWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                unlockQueueSheetWindow.ShowDialog();
            });
        }

        private void ViewModel_OnShowSettings(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                var userSettingsWindow = new UserSettingsWindow();
                var mainWindow = Application.Current.MainWindow;
                userSettingsWindow.Owner = mainWindow;
                userSettingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                userSettingsWindow.ShowDialog();
            });
        } 

        private void ViewModel_OnShowLogin(object sender, EventArgs e)
        {
 
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                var loginWindow = new LoginWindow();
                var mainWindow = Application.Current.MainWindow; 
                loginWindow.Owner = mainWindow;
                loginWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                loginWindow.ShowDialog();
            });

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            ViewModel.SelectCommand.Execute(listBox.SelectedItem as QueueSheet);
        }
    }
}
