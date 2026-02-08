namespace MFO.Application.Employees;

public sealed record EmployeeRequest(
    string FullName,
    Guid BranchId,
    bool IsActive);
