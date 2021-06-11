using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Mdm.Windows.Desktop.Assets
{
    public class DynamicListBox : ListBox
    {

        internal bool processSelectionChanges = false;

        public static readonly DependencyProperty BindableSelectedItemsProperty =
            DependencyProperty.Register("BindableSelectedItems",
                typeof(object), typeof(DynamicListBox),
                new FrameworkPropertyMetadata(default(ICollection<object>),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindableSelectedItemsChanged));

        public dynamic BindableSelectedItems
        {
            get => GetValue(BindableSelectedItemsProperty);
            set => SetValue(BindableSelectedItemsProperty, value);
        }

        public static readonly DependencyProperty MaxSelectedItemsProperty =
            DependencyProperty.Register("MaxSelectedItems",
                typeof(object), typeof(DynamicListBox),
                new FrameworkPropertyMetadata(default(int),
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int MaxSelectedItems
        {
            get => (int) GetValue(MaxSelectedItemsProperty);
            set => SetValue(MaxSelectedItemsProperty, value);
        }


        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (BindableSelectedItems == null || !this.IsInitialized) return; //Handle pre initilized calls
            var itemCount = (BindableSelectedItems as ICollection).Count + e.AddedItems.Count - e.RemovedItems.Count;
 
            if (MaxSelectedItems == 0 || MaxSelectedItems >= itemCount)
            {

                if (e.AddedItems.Count > 0)
                    if (!string.IsNullOrWhiteSpace(SelectedValuePath))
                    {
                        foreach (var item in e.AddedItems)
                        {
                            try
                            {
                                if (!BindableSelectedItems.Contains((dynamic)item.GetType().GetProperty(SelectedValuePath).GetValue(item, null)))
                                    BindableSelectedItems.Add((dynamic)item.GetType().GetProperty(SelectedValuePath).GetValue(item, null));
                            }
                            catch
                            {

                            }
                        }
                    }
                    else
                    {
                        foreach (var item in e.AddedItems)
                        {
                            try
                            {
                                if (!BindableSelectedItems.Contains((dynamic)item))
                                    BindableSelectedItems.Add((dynamic)item);
                            }
                            catch
                            {

                            }

                        }
                    }

                if (e.RemovedItems.Count > 0)
                    if (!string.IsNullOrWhiteSpace(SelectedValuePath))
                    {
                        foreach (var item in e.RemovedItems)
                        {
                            try
                            {
                                if (BindableSelectedItems.Contains((dynamic)item.GetType().GetProperty(SelectedValuePath).GetValue(item, null)))
                                    BindableSelectedItems.Remove((dynamic)item.GetType().GetProperty(SelectedValuePath).GetValue(item, null));
                            }
                            catch
                            {

                            }

                        }
                     
                    }
                    else
                    {
                        foreach (var item in e.RemovedItems)
                        {
                            try
                            {
                                if (BindableSelectedItems.Contains((dynamic)item))
                                    BindableSelectedItems.Remove((dynamic)item);
                            }
                            catch
                            {

                            } 
                        }
                       
                    }

            }
            else
            {
                e.Handled = true;
            }
        }

        private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DynamicListBox listBox)
            {
                List<dynamic> newSelection = new List<dynamic>();
                if (!string.IsNullOrWhiteSpace(listBox.SelectedValuePath))
                    foreach (var item in listBox.BindableSelectedItems)
                    {
                        foreach (var lbItem in listBox.Items)
                        {
                            var lbItemValue = lbItem.GetType().GetProperty(listBox.SelectedValuePath).GetValue(lbItem, null);
                            if ((dynamic)lbItemValue == (dynamic)item)
                                newSelection.Add(lbItem);
                        }
                    }
                else
                {
                    foreach(var item in (IEnumerable) listBox.BindableSelectedItems)
                    {
                        newSelection.Add((dynamic)item);
                    }
                } 

                listBox.SetSelectedItems(newSelection);
            }
        } 

    /*
    public static readonly DependencyProperty BindableSelectedItemsProperty =
  DependencyProperty.Register("SelectedItemsList",
      typeof(IEnumerable), typeof(DynamicListBox),
      new FrameworkPropertyMetadata(default(IEnumerable),
          FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBindableSelectedItemsChanged));

    public IEnumerable SelectedItemsList
    {
        get => (IEnumerable)GetValue(BindableSelectedItemsProperty);
        set => SetValue(BindableSelectedItemsProperty, value);
    }

    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        base.OnSelectionChanged(e);
        SelectedItemsList = SelectedItems;
    }

    private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DynamicListBox listBox)
            listBox.SetSelectedItems(listBox.SelectedItemsList);
    }
    */
    /*
    public DynamicListBox()
    {
        SelectionChanged += ListBoxCustom_SelectionChanged;
    }

    void ListBoxCustom_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedItemsList = SelectedItems;
    }

    public IList SelectedItemsList
    {
        get { return (IList)GetValue(SelectedItemsListProperty); }
        set { SetValue(SelectedItemsListProperty, value); }
    }

    public static readonly DependencyProperty SelectedItemsListProperty = DependencyProperty.Register(nameof(SelectedItemsList), typeof(IList), typeof(DynamicListBox), new FrameworkPropertyMetadata(OnBindableSelectedItemsChanged));

    private static void OnBindableSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is DynamicListBox listBox)
            listBox.SetSelectedItems(listBox.SelectedItemsList);
    }
    */
}
}
