using Dapper;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Repositories
{
    public class BonusRepository : IBonusRepository
    {
        private IDbConnection db;

        public BonusRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Company> GetAllCompanyWithEmployees()
        {
            var sql = "SELECT C.*,E.* FROM Employees AS E INNER JOIN Companies AS C ON E.CompanyId = C.CompanyId ";

            var companyDic = new Dictionary<int, Company>();

            var company = db.Query<Company, Employee, Company>(sql, (c, e) =>
            {
                if (!companyDic.TryGetValue(c.CompanyId, out var currentCompany))
                {
                    currentCompany = c;
                    companyDic.Add(currentCompany.CompanyId, currentCompany);
                }
                currentCompany.Employees.Add(e);
                return currentCompany;
            }, splitOn: "EmployeeId");

            return company.Distinct().ToList();
        }

        public Company GetCompanyWithEmployees(int companyId)
        {
            var p = new
            {
                CompanyId = companyId
            };
            var sql = "SELECT * FROM COMPANIES WHERE CompanyId = @CompanyId;";
            sql += "SELECT * FROM Employees WHERE CompanyId = @CompanyId;";
            Company company;
            using(var lists = db.QueryMultiple(sql, p))
            {
                company = lists.Read<Company>().ToList().FirstOrDefault();
                company.Employees = lists.Read<Employee>().ToList();
            }
            return company;
        }

        public List<Employee> GetEmployeeWithCompany(int companyId)
        {
            var sql = "SELECT E.*, C.* FROM Employees AS E INNER JOIN Companies AS C ON E.CompanyId = C.CompanyId";
            if(companyId != 0)
            {
                sql += " WHERE E.CompanyId = @Id";
            }
            var employee = db.Query<Employee, Company, Employee>(sql,(employee, company) =>
            {
                employee.Company = company;
                return employee;
            }, new { Id = companyId }, splitOn: "CompanyId");
            return employee.ToList();
        }
    }
}
