namespace WpfDataBindingLearn;

public class Product
{
    public string Name { get; set; } = string.Empty;

    public int Price { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Price:N0}원";
    }
}