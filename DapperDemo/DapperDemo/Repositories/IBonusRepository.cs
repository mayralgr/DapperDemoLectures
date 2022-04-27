using DapperDemo.Models;

namespace DapperDemo.Repositories
{
    public interface IBonusRepository
    {
        List<Employee> GetEmployeeWithCompany();
    }
}
