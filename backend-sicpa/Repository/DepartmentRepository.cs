using AutoMapper;
using backend_sicpa.Interfaces;
using backend_sicpa.Models;

namespace backend_sicpa.Repository
{
    public class DepartmentRepository: IDepartmentRepository 
    {
        private readonly SicpaDbContext _context;
        private readonly IMapper _mapper;

        public DepartmentRepository(SicpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateDepartment(Department department)
        {
            _context.Add(department);
            return Save();
        }

        public bool DeleteDepartment(Department department)
        {
            _context.Remove(department);
            return Save();
        }

        public bool  DeleteEnterprises(List<Department> enterprises)
        {
            _context.Remove(enterprises);
            return Save();
        }

        public bool DepartmentExists(int departmentId)
        {
            return _context.Departments.Any(r => r.Id == departmentId);
        }

        public Department GetDepartment(int departmentId)
        {
            return _context.Departments.Where(r => r.Id == departmentId).FirstOrDefault();
        }

        public ICollection<Department>GetDepartments()
        {
            return _context.Departments.ToList();
        }

        public ICollection<Department> GetDepartmentsOfAEnterprise(int enterpriseId)
        {
            return _context.Departments.Where(r => r.Enterprises.Id == enterpriseId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDepartment(Department department)
        {
           _context.Update(department);
            return Save();
        }
    }
}
