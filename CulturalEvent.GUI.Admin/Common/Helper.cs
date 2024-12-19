using System.ComponentModel.DataAnnotations;
using System.Reflection;
using CulturalEvents.App.Core.Entity;

namespace CulturalEvent.GUI.Admin.Common;

public static class Helper
{
    internal static string GetDisplayName<T>()
    {
        return GetDisplayName(typeof(T));
    }
    internal static string GetDisplayName(MemberInfo type)
    {
        return type.GetCustomAttribute<DisplayAttribute>()?.Name ?? type.Name;
    } 
    internal static void OnlyNumber(object? sender, KeyPressEventArgs e)
    {
        if (sender is null)
            return;
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    internal static void OnlyFloatNumber(object? sender, KeyPressEventArgs e)
    {
        if (sender is null)
            return;
        const char decimalSeparator = '.'; //Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
        
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != decimalSeparator || ((TextBox)sender).Text.Contains(decimalSeparator)))
        {
            e.Handled = true;
        }
    }
}