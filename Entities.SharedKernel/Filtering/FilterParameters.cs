namespace Entities.SharedKernel.Filtering;

public sealed class FilterParameters
{
    private const string FilterValuesDelimiter = " ";

    public FilterParameters(string filter)
    {
        var filterPair = filter.Split(FilterValuesDelimiter).ToList();
        ExtractPropertyNameAndOperator(filterPair);
        Value = string.Join(FilterValuesDelimiter, filterPair).TrimStart('\'').TrimEnd('\'');
    }

    private void ExtractPropertyNameAndOperator(List<string> filterPair)
    {
        PropertyName = filterPair[0];
        FilterOperator = filterPair[1];
        filterPair.RemoveRange(0, 2);
    }

    public string PropertyName { get; private set; } = string.Empty;
    public string FilterOperator { get; private set; } = string.Empty;
    public string Value { get; }

    public bool HaveNoFilter() => Value.IsNullOrWhiteSpace();
}