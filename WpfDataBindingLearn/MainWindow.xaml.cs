using System.Windows;

namespace WpfDataBindingLearn;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly User _user;
    private int _productCount = 3;

    public MainWindow()
    {
        InitializeComponent();
        _user = new User
        {
            Name = "홍길동"
        };

        _user.Products.Add(new Product { Name = "노트북", Price = 1500000 });
        _user.Products.Add(new Product { Name = "마우스", Price = 50000 });
        _user.Products.Add(new Product { Name = "키보드", Price = 120000 });

        DataContext = _user;
    }

    private void AddProductButton_Click(object sender, RoutedEventArgs e)
    {
        _productCount++;
        _user.Products.Add(new Product
        {
            Name = $"새 상품 {_productCount}",
            Price = _productCount * 10000
        });
    }
}