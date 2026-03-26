using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfMvvmLearn.Behaviors;

public static class ListBoxAssistant
{
    public static readonly DependencyProperty AutoScrollToSelectedItemProperty =
        DependencyProperty.RegisterAttached(
            "AutoScrollToSelectedItem",
            typeof(bool),
            typeof(ListBoxAssistant),
            new PropertyMetadata(false, OnAutoScrollToSelectedItemChanged));

    public static bool GetAutoScrollToSelectedItem(DependencyObject dependencyObject)
    {
        return (bool)dependencyObject.GetValue(AutoScrollToSelectedItemProperty);
    }

    public static void SetAutoScrollToSelectedItem(DependencyObject dependencyObject, bool value)
    {
        dependencyObject.SetValue(AutoScrollToSelectedItemProperty, value);
    }

    private static void OnAutoScrollToSelectedItemChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependencyObject is not ListBox listBox)
        {
            return;
        }

        if ((bool)eventArgs.OldValue)
        {
            listBox.SelectionChanged -= HandleSelectionChanged;
        }

        if ((bool)eventArgs.NewValue)
        {
            listBox.SelectionChanged += HandleSelectionChanged;
        }
    }

    private static void HandleSelectionChanged(object sender, SelectionChangedEventArgs eventArgs)
    {
        if (sender is not ListBox listBox || listBox.SelectedItem is null)
        {
            return;
        }

        object selectedItem = listBox.SelectedItem;

        listBox.Dispatcher.BeginInvoke(() =>
        {
            listBox.ScrollIntoView(selectedItem);
            listBox.UpdateLayout();

            if (listBox.ItemContainerGenerator.ContainerFromItem(selectedItem) is ListBoxItem item)
            {
                item.Focus();
            }
        }, DispatcherPriority.Background);
    }
}