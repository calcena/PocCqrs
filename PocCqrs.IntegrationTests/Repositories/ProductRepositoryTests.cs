using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PocCqrs.Application.Mapper;
using PocCqrs.Application.Models;
using PocCqrs.Domain;
using PocCqrs.Infrastructure.EFCore;
using PocCqrs.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PocCqrs.IntegrationTests.Repositories
{
    public class ProductRepositoryTests: IClassFixture<SharedDatabaseFixture>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly EFDataContext _datacontext; 
        public SharedDatabaseFixture Fixture { get; }


        public ProductRepositoryTests(SharedDatabaseFixture fixture, IConfiguration configuration, EFDataContext datacontext)
        {
            Fixture = fixture;
            _configuration = configuration;

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();
            _datacontext = datacontext;
        }

        [Fact]
        public void GetProducts_ReturnsAllProducts()
        {
            using (var context = Fixture.CreateContext())
            {
                // Arrange
                var productRepository = new ProductRepository(_configuration);
                // Act
                var products = productRepository.GetAll();
                // Assert
                Assert.Equal(1, products.Result.Count);
            }
        }

        [Fact]
        public void CreateProduct_EnsureSavesData()
        {
            using (var transction = Fixture.Connection.BeginTransaction())
            {
                var productId = 0;
                using (var context = Fixture.CreateContext(transction))
                {
                  // Arrage
                    var newProduct = new Product
                    {
                        Code = "0005",
                    };
                    // Act
                    var product = context.Products.Add(newProduct);
                    productId = product.Entity.Id;

                    // Assert
                    Assert.Equal(1, productId);
                }
            }
        }





    }
}
