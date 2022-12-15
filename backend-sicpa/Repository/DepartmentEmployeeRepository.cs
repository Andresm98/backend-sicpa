using AutoMapper;
using backend_sicpa.Interfaces;
using backend_sicpa.Models;

namespace backend_sicpa.Repository
{
    public class DepartmentEmployeeRepository : IDepartmentEmployeeRepository
    {
        private readonly SicpaDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentEmployeeRepository(SicpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateDepartmentEmployee(DepartmentsEmployee departmentEmployee)
        {
            _context.Add(departmentEmployee);
            return Save();
        }

        public bool DeleteDepartmentEmployee(DepartmentsEmployee departmentEmployee)
        {
            _context.Remove(departmentEmployee);
            return Save();
        }

        public bool DepartmentEmployeeExists(int departmentEmployeeId)
        {
            return _context.DepartmentsEmployees.Any(r => r.Id == departmentEmployeeId);
        }

        public DepartmentsEmployee GetDepartmentEmployee(int departmentEmployeeId)
        {
            return _context.DepartmentsEmployees.Where(r => r.Id == departmentEmployeeId).FirstOrDefault();
        }

        public ICollection<DepartmentsEmployee> GetDepartmentsEmployees()
        {
            return _context.DepartmentsEmployees.ToList();
        }

        public ICollection<DepartmentsEmployee> GetDepartmentsOfAEmployee(int employeeId)
        {
            return _context.DepartmentsEmployees.Where(r => r.Employees.Id == employeeId).ToList();
        }

        public ICollection<DepartmentsEmployee> GetEmployeesOfADepartment(int departmentId)
        {
            return _context.DepartmentsEmployees.Where(r => r.Departments.Id == departmentId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDepartmentEmployee(DepartmentsEmployee departmentEmployee)
        {
            _context.Update(departmentEmployee);
            return Save();
        }
    }
}
