namespace CulturalEvents.App.Core;

public class Option<T> where T : class
{
    public T? Value { get; }
    public bool IsSome { get; }
    public bool IsNone => !IsSome;

    private Option(T? value)
    {
        Value = value;
        IsSome = value is not null;
    }

    private Option()
    {
        IsSome = false;
    }

    private Option(IEnumerable<T> values)
    {
        if (values is null)
        {
            IsSome = false;
        }
        else
        {
            var arr = values.Take(1).ToArray();
            IsSome = arr.Length == 1;
            if(IsSome)
                Value = arr[0];
        }
    }
    
    public static implicit operator T(Option<T> option)
    {
        if (option is { Value: null })
            throw new InvalidOperationException("Cannot retrieve value from a null result.");
        return option.Value!;
    }
    public static implicit operator Option<T>(T? value)
    {
        return new Option<T>(value);
    }
}