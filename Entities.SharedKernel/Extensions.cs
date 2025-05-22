namespace Entities.SharedKernel;

public static class Extensions
{
    public static bool IsNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    } 
}