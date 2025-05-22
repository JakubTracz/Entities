namespace Entities.SharedKernel.Filtering;

public sealed class FilteringOperatorNotAllowedException(
    string propertyName,
    string filterOperator) : Exception(
    $"Filtering operator {filterOperator} not allowed for property: {propertyName}.");