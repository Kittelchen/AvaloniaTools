namespace Common.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value) => string.IsNullOrEmpty(value);
    public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);
    
    public static bool IsNotNullOrEmpty(this string? value) => !string.IsNullOrEmpty(value);
    public static bool IsNotNullOrWhiteSpace(this string? value) => !string.IsNullOrWhiteSpace(value);
    
    public static bool IsEQ(this string? value, string? other, bool caseSensitive = false)
    {
        if (caseSensitive)  return string.Equals(value, other);
        return string.Equals(value, other, StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsNE(this string? value, string? other, bool caseSensitive = false)
    {
        return !value.IsEQ(other, caseSensitive);
    }
    public static bool StartsWithIgnoreCase(this string? value, string? prefix)
    {
        return value?.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    public static bool EndsWithIgnoreCase(this string? value, string? suffix)
    {
        return value?.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) ?? false;
    }

    public static bool ContainsIgnoreCase(this string? value, string? substring)
    {
        if (value == null || substring == null) return false;
        return value.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}