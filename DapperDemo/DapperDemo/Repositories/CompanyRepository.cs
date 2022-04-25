﻿using Dapper;
using DapperDemo.DbContexts;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private IDbConnection db;

        public CompanyRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode); SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = db.Query<int>(sql, new
            {
                @Name = company.Name,
                @Address = company.Address,
                @City = company.City,
                @State = company.State,
                @PostalCode = company.PostalCode

            }).Single();
            company.CompanyID = id;
            return company;
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @Id";
            return db.Query<Company>(sql, new
            {
                @Id = id
            }).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return db.Query <Company> (sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @Id";
            db.Query<Company>(sql, new
            {
                @Id = id
            });
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies SET Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode WHERE CompanyId = @CompanyId";
            db.Query<int>(sql, new
            {
                @Name = company.Name,
                @Address = company.Address,
                @City = company.City,
                @State = company.State,
                @PostalCode = company.PostalCode,
                @CompanyId = company.CompanyID
            });
            return company;
        }
    }
}