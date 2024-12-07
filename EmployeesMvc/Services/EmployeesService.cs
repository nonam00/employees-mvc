using EmployeesMvc.Data;
using EmployeesMvc.Models;

namespace EmployeesMvc.Services;

public interface IEmployeesService
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(EmployeeDto dto);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Guid id);
}

public class EmployeesService(IEmployeesRepository repository) : IEmployeesService
{
    private readonly IEmployeesRepository _repository = repository;
    
    public Task<List<Employee>> GetAllAsync() =>
        Task.FromResult(_repository.GetAll().ToList());

    public Task<Employee> GetByIdAsync(Guid id)
    {
        var employee = _repository.GetById(id) ?? throw new Exception("Employee not found");
        return Task.FromResult(employee);
    }

    public Task<Guid> CreateAsync(EmployeeDto dto) =>
        Task.FromResult(_repository.Create(dto));

    public Task UpdateAsync(Employee employee) =>
        Task.Run(() => _repository.Update(employee));

    public Task DeleteAsync(Guid id) =>
        Task.Run(() => _repository.Delete(id));
}