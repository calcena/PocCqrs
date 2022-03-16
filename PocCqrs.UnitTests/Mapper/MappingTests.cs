namespace PocCqrs.Test.UnitTest.Mapper
{
    using AutoMapper;
    using PocCqrs.Application.Mapper;
    using PocCqrs.Application.Models;
    using PocCqrs.Domain;
    using System;
    using System.Runtime.Serialization;
    using Xunit;
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }
        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(ProductDto), typeof(Product))]
        [InlineData(typeof(Product), typeof(ProductDto))]
        public void Map_SourceToDestination_ExistConfiguration(Type origin, Type destination)
        {
            // Arrange

            // Act
            var instance = FormatterServices.GetUninitializedObject(origin);
            // Assert
            _mapper.Map(instance, origin, destination);
        }
    }
}
