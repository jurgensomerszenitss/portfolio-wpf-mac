using Mdm.App.Infrastructure;
using Mdm.Core.Models;
using System.Windows;
using System.Windows.Controls;

namespace Mdm.Windows.Desktop.Assets
{
    public class QueueItemIconDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null)
            {
                if(item is Track)
                    return element.FindResource("TrackIconDataTemplate") as DataTemplate;
                else
                    return element.FindResource("FolderIconDataTemplate") as DataTemplate; 
            }

            return null;
        }
    }

    public class QueueItemListDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null)
            {
                if (item is Track)
                    return element.FindResource("TrackListDataTemplate") as DataTemplate;
                else
                    return element.FindResource("FolderListDataTemplate") as DataTemplate;
            }

            return null;
        }
    }
}
