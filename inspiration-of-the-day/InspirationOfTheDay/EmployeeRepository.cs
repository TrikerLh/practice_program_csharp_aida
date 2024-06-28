using System.Collections.Generic;

namespace InspirationOfTheDay;

public interface EmployeeRepository
{
    IList<Employee> GetEmployees();
}