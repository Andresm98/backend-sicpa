using backend_sicpa.Models;

namespace backend_sicpa.Interfaces
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();
        Department GetDepartment(int departmentId);
        ICollection<Department> GetDepartmentsOfAEnterprise(int enterpriseId);
        bool DepartmentExists(int reviewId);
        bool CreateDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(Department department);
        bool DeleteEnterprises(List<Department> enterprises);
        bool Save();
    }
}
