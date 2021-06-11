using Mdm.App.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Mdm.Windows.Desktop.Views
{
    /// <summary>
    /// Interaction logic for UnlockQueueSheetView.xaml
    /// </summary>
    public partial class UnlockQueueSheetView : UserControl
    {
        public UnlockQueueSheetView()
        {
            DataContext = App.Container.GetInstance<IUnlockQueueSheetViewModel>();
            InitializeComponent();
        }

        internal IUnlockQueueSheetViewModel ViewModel => ((IUnlockQueueSheetViewModel)DataContext);

        internal void Initialize()
        {
            pwBox1.Focus();
        }

        private void PasswordBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var regex = new Regex("[0-9]+");
            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true;
            }
        }

        private void PasswordBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var pwBox = sender as PasswordBox;
            if (!string.IsNullOrEmpty(pwBox.Password))
            {
                if (pwBox == pwBox1) pwBox2.Focus();
                if (pwBox == pwBox2) pwBox3.Focus();
                if (pwBox == pwBox3) pwBox4.Focus();
            }
        }
    }
}
