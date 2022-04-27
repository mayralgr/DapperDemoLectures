﻿using Dapper;
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
