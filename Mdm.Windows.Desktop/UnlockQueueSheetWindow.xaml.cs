using System;
using System.Windows;

namespace Mdm.Windows.Desktop
{
    /// <summary>
    /// Interaction logic for UnlockQueueSheetWindow.xaml
    /// </summary>
    public partial class UnlockQueueSheetWindow : Window
    {
        public UnlockQueueSheetWindow()
        {
            InitializeComponent();

            unlockQueueSheetView.ViewModel.OnCancel += ViewModel_OnCancel;
            unlockQueueSheetView.ViewModel.OnUnlock += ViewModel_OnUnlock;
        }

        protected override void OnInitialized(EventArgs e)
        {
            unlockQueueSheetView.Initialize();
            base.OnInitialized(e);
        }

        private void ViewModel_OnUnlock(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                //DialogResult = true;
                this.Close();
            });
        }

        private void ViewModel_OnCancel(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                this.Close();
            });
        }
    }
}
