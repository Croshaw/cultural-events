namespace CulturalEvent.GUI.Admin.Common;

class ValueWrapper
{
    private object? _value;
    public delegate void ValueChangedEventHandler(object? value);
    public event ValueChangedEventHandler? ValueChanged;

    public object? Value
    {
        get => _value;
        set
        {
            if (_value == value) return;
            _value = value;
            ValueChanged?.Invoke(_value);
        }
    }

    public ValueWrapper(object? value)
    {
        _value = value;
    }
}