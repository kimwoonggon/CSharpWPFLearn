using System.Windows;
using System.Windows.Controls;

namespace WpfAnimationAndCustomControl.Controls;

/// <summary>
/// 사각형 모양의 커스텀 ProgressBar (CustomControl).
/// ProgressBar를 상속받지 않고 Control을 직접 상속합니다.
/// </summary>
public class RectangleProgressBar : Control
{
    static RectangleProgressBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(RectangleProgressBar),
            new FrameworkPropertyMetadata(typeof(RectangleProgressBar)));
    }

    #region Value (0 ~ 100)

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(double),
            typeof(RectangleProgressBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, OnValueChanged, CoerceValue));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RectangleProgressBar bar)
        {
            bar.UpdateFillWidth();
        }
    }

    private static object CoerceValue(DependencyObject d, object baseValue)
    {
        double v = (double)baseValue;
        return Math.Clamp(v, 0.0, 100.0);
    }

    #endregion

    #region BarBrush

    public static readonly DependencyProperty BarBrushProperty =
        DependencyProperty.Register(
            nameof(BarBrush),
            typeof(System.Windows.Media.Brush),
            typeof(RectangleProgressBar),
            new FrameworkPropertyMetadata(System.Windows.Media.Brushes.DodgerBlue));

    public System.Windows.Media.Brush BarBrush
    {
        get => (System.Windows.Media.Brush)GetValue(BarBrushProperty);
        set => SetValue(BarBrushProperty, value);
    }

    #endregion

    private FrameworkElement? _fillElement;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _fillElement = GetTemplateChild("PART_Fill") as FrameworkElement;
        UpdateFillWidth();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        UpdateFillWidth();
    }

    private void UpdateFillWidth()
    {
        if (_fillElement is null) return;
        double ratio = Value / 100.0;
        _fillElement.Width = ActualWidth * ratio;
    }
}
