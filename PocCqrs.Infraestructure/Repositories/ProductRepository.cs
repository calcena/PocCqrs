using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PocCqrs.Domain;
using PocCqrs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace PocCqrs.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Product>> GetAll()
        {
            var queryRaw = "SELECT * FROM Products";

            using (IDbConnection connection = GetDbconnection())
            {
                connection.Open();
                try
                {
                    var result = await connection.QueryAsync<Product>(queryRaw);
                    return (List<Product>)result;
                }
                catch (Exception e)
                {
                    throw new Exception("Error connetion with DB: ", e);
                }
            }
        }
        public async Task<Product> GetById(int id)
        {
            using (IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("MsqlConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>("SP_Products", this.setParameters(id)
                    , commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        private DynamicParameters setParameters(int id)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return parameters;
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("MsqlConnection"));
        }
    }
}
