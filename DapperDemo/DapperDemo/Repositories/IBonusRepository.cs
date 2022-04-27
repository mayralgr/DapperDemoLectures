using DapperDemo.Models;

namespace DapperDemo.Repositories
{
    public interface IBonusRepository
    {
        List<Employee> GetEmployeeWithCompany(int companyId);

        Company GetCompanyWithEmployees(int companyId);

        List<Company> GetAllCompanyWithEmployees();

        void AddTestCompanyWithEmploye(Company company);

        void RemoveRange(int[] companyId);

        List<Company> FilterCompanyByName(string name);
    }
}
