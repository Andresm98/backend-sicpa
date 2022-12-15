using backend_sicpa.Models;

namespace backend_sicpa.Interfaces
{
    public interface IDepartmentEmployeeRepository
    {

        ICollection<DepartmentsEmployee> GetDepartmentsEmployees();
        DepartmentsEmployee GetDepartmentEmployee(int departmentEmployeeId);

        ICollection<DepartmentsEmployee> GetDepartmentsOfAEmployee(int employeeId);
        ICollection<DepartmentsEmployee> GetEmployeesOfADepartment(int departmentId);

        bool DepartmentEmployeeExists(int departmentEmployeeId);

        bool CreateDepartmentEmployee(DepartmentsEmployee departmentEmployee);
        bool UpdateDepartmentEmployee(DepartmentsEmployee departmentEmployee);
        bool DeleteDepartmentEmployee(DepartmentsEmployee departmentEmployee);

        bool Save();

    }
}
