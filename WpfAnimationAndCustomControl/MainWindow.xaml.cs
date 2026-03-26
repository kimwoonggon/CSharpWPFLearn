using System.Windows;
using System.Windows.Media;

namespace WpfAnimationAndCustomControl;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnBlueClick(object sender, RoutedEventArgs e)
        => CustomProgressBar.BarBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3B82F6"));

    private void OnGreenClick(object sender, RoutedEventArgs e)
        => CustomProgressBar.BarBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22C55E"));

    private void OnRedClick(object sender, RoutedEventArgs e)
        => CustomProgressBar.BarBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EF4444"));
}