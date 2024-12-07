using EmployeesMvc.Models;

namespace EmployeesMvc.Data;

public interface IEmployeesRepository
{
    IEnumerable<Employee> GetAll();
    Employee? GetById(Guid id);
    Guid Create(EmployeeDto employee);
    void Update(Employee employee);
    void Delete(Guid id);
}

public class EmployeesRepository : IEmployeesRepository
{
    private static readonly List<Employee> Employees = 
    [
        new()
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            Age = 21
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Jane Doe",
            Age = 24
        }
    ];
    
    public IEnumerable<Employee> GetAll() => Employees;

    public Employee? GetById(Guid id) => Employees.SingleOrDefault(x => x.Id == id);

    public Guid Create(EmployeeDto dto)
    {
        var employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Age = dto.Age
        };
        Employees.Add(employee);
        return employee.Id;
    }

    public void Update(Employee dto)
    {
        var employee = Employees.SingleOrDefault(x => x.Id == dto.Id) 
            ?? throw new Exception("Employee not found");
        employee.Name = dto.Name;
        employee.Age = dto.Age;
    }

    public void Delete(Guid id)
    {
        var employee = Employees.SingleOrDefault(x => x.Id == id)
            ?? throw new Exception("Employee not found");
        Employees.Remove(employee);
    }
}
