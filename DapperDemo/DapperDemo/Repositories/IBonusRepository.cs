﻿using DapperDemo.Models;

namespace DapperDemo.Repositories
{
    public interface IBonusRepository
    {
        List<Employee> GetEmployeeWithCompany(int companyId);

        Company GetCompanyWithEmployees(int companyId);

        List<Company> GetAllCompanyWithEmployees();
    }
}
