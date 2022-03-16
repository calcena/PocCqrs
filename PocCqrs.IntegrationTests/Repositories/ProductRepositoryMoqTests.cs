using Moq;
using PocCqrs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PocCqrs.IntegrationTests.Repositories
{

    public class ProductRepositoryMoqTests
    {


        public ProductRepositoryMoqTests()
        {


        }
        [Fact]
        public void Get_ReturnAllProducts()
        {
            // Arrange
            Mock<IProductRepository> productRepository = new Mock<IProductRepository>();
            var instance =productRepository.Object;

            // Act
            var products = instance.GetAll();

            // Assert
            Assert.Equal(1, products.Id);



        } 


    }
}
