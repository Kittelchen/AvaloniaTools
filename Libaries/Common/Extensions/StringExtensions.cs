namespace Common.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value);
    }

    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    
    public static bool IsEQ(this string? value, string? other, bool caseSensitive = false)
    {
        if (caseSensitive)  return string.Equals(value, other);
        return string.Equals(value, other, StringComparison.OrdinalIgnoreCase);
    }
}