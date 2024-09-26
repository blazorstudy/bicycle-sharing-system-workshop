namespace BicycleSharingSystem.Support.UI.Units;

public class BicycleRadioButton : RadioButton
{
    public static readonly DependencyProperty IconTypeProperty = DependencyProperty.Register("IconType", typeof(string), typeof(BicycleRadioButton), new PropertyMetadata(null, OnMenuIconTypePropertyChanged));

    public string IconType
    {
        get { return (string)GetValue(IconTypeProperty); }
        set { SetValue(IconTypeProperty, value); }
    }

    public BicycleRadioButton()
    { 
        DefaultStyleKey = typeof(BicycleRadioButton);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        UpdateMenuIconType();

    }

    private static void OnMenuIconTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BicycleRadioButton vmbi)
        {
            vmbi.UpdateMenuIconType();
        }
    }

    private void UpdateMenuIconType()
    {
        VisualStateManager.GoToState(this, IconType, false);
    }
}
