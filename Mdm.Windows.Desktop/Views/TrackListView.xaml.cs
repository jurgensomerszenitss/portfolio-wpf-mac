using Mdm.App.ViewModels;
using Mdm.Core.Models;
using Mdm.Windows.Desktop.Assets;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;

namespace Mdm.Windows.Desktop.Views
{
    /// <summary>
    /// Interaction logic for FileListView.xaml
    /// </summary>
    public partial class TrackListView : System.Windows.Controls.UserControl
    {
        public TrackListView()
        {
            DataContext = App.Container.GetInstance<ITrackListViewModel>();
            InitializeComponent();
            ViewModel.OnDownloadSuccess += ViewModel_OnDownloadSuccess;
            ViewModel.OnDownloadFailed += ViewModel_OnDownloadFailed;
        }

        private void ViewModel_OnDownloadFailed(object sender, string e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                System.Windows.Forms.MessageBox.Show(e, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
        }

        private void ViewModel_OnDownloadSuccess(object sender, string e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                await ViewModel.Load();
                System.Windows.Forms.MessageBox.Show(e, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
        }

        private ITrackListViewModel ViewModel => ((ITrackListViewModel)DataContext);
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            await ViewModel.Initialize();
        }

        private void DynamicListBox_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                var parent = (DynamicListBox)sender;
                var dragSource = parent;
                object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

                if (data != null)
                {
                    var queueFile = (Track)data;
                    if (File.Exists(queueFile.FullPath))
                    {
                        string[] array = { queueFile.FullPath };
                        var copy = new System.Windows.DataObject(System.Windows.DataFormats.FileDrop, array);
                        DragDrop.DoDragDrop(parent, copy, System.Windows.DragDropEffects.Copy);
                    }
                }
            }
            catch
            {

            }
        }

       

        private static object GetDataFromListBox(DynamicListBox source, System.Windows.Point point)
        {
            
            var element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }

                    if (element == source)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    if(data is Track ) // && ((QueueFile) data).Content != null)
                    return ((Track)data);
                }
            }

            return null;
        }

    
    }
}
