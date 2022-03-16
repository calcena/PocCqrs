using Microsoft.Data.Sqlite;
using System;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using PocCqrs.Infrastructure.EFCore;
using PocCqrs.Domain;

namespace PocCqrs.IntegrationTests
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        private static bool _databaseInitialized;
        private string dbName = "TestDatabbase.db";

        public SharedDatabaseFixture()
        {
            Connection = new SqliteConnection($"Filename=(dbName");
            Seed();
            Connection.Open();
        }

        public DbConnection Connection { get; }

        public EFDataContext CreateContext(DbTransaction transaction = null)
        {
            var context = new EFDataContext(new DbContextOptionsBuilder<EFDataContext>().UseSqlite(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }
            return context;
        }

        public void Dispose() => Connection.Dispose();
  
        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        SeedData(context);
                    }
                    _databaseInitialized = true;
                }
            }
        }
        private void SeedData(EFDataContext context)
        {
            var product1 = new Product
            {
                Id = 1,
                Code = "0005",
                Name = "Product Test 1",
                Description = "Description Test Integration" 
            };
            context.AddRange(product1);
            context.SaveChanges();
        }
    }
}
