namespace MFO.Application.Employees;

public sealed record EmployeeDto(
    Guid Id,
    string FullName,
    Guid BranchId,
    bool IsActive);
