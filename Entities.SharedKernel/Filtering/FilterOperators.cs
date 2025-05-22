namespace Entities.SharedKernel.Filtering;

public static class FilterOperators
{
    public const string IsEqualTo = "equals";
    public const string NotEquals = "not-equal";
    public const string GreaterThan = "greater-than";
    public const string GreaterThanOrEquals = "greater-than-or-equals";
    public const string LessThan = "less-than";
    public const string LessThanOrEquals = "less-than-or-equals";
    public const string Contains = "contains";
    public const string DoesNotContain = "does-not-contain";
    public const string StartsWith = "starts-with";
    public const string EndsWith = "ends-with";
    public const string IsEmpty = "is-empty";
    public const string IsNotEmpty = "is-not-empty";
    public const string Between = "between-inclusive-inclusive";
    public const string BetweenInclusiveExclusive = "between-inclusive-exclusive";
    public const string BetweenExclusiveInclusive = "between-exclusive-inclusive";
    public const string BetweenExclusiveExclusive = "between-exclusive-exclusive";
    public const string InWeek  = "week";
    public const string InMonth  = "month";
    public const string InQuarter  = "quarter";
    public const string InYear  = "year";
    public const string Last  = "last";
    public const string Next  = "next";
    
    public static class TimePeriods
    {
        public const string Days = "days";
        public const string Weeks = "weeks";
        public const string Months = "months";
        public const string Quarters = "quarters";
        public const string Years = "years";
    }
}