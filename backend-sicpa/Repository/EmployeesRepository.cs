using AutoMapper;
using backend_sicpa.Interfaces;
using backend_sicpa.Models;

namespace backend_sicpa.Repository
{
    public class EmployeesRepository : IEmployeeRepository
    {
        private readonly SicpaDbContext _context;
        private readonly IMapper _mapper;

        public EmployeesRepository(SicpaDbContext context, IMapper mapper)
        {
           _context = context;
           _mapper = mapper;
        }


        public bool CreateEmployee(Employee employee)
        {
            _context.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _context.Remove(employee);
            return Save();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Employees.Where(r => r.Id == employeeId).FirstOrDefault();
        }

        public ICollection<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }


        public bool EmployeeExists(int employeeId)
        {
            return _context.Employees.Any(r => r.Id == employeeId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEmployee(Employee employee)
        {

            _context.Update(employee);
            return Save();
        }
 
    }
}
