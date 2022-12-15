using backend_sicpa.Models;
using System.Diagnostics.Metrics;

namespace backend_sicpa.Interfaces
{
    public interface IEnterpriseRepository
    {
        ICollection<Enterprise> GetEnterprises();
        Enterprise GetEnterprise(int enterpriseId);
        ICollection<Department> GetDepartmentsByEnterprise(int enterpriseId);
        bool EnterpriseExists(int enterpriseId);
        bool CreateEnterprise(Enterprise enterprise);
        bool UpdateEnterprise(Enterprise enterprise);
        bool DeleteEnterprise(Enterprise enterprise);
        bool Save();
    }
}
