using AutoMapper;
using Microsoft.Extensions.Configuration;
using PocCqrs.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PocCqrs.Test
{

    public class Example
    {
        private readonly AutoMapper.IConfigurationProvider _configuration;
        private readonly IMapper _mapper;
        public Example()
        {
            _configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<MapperProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void Add_ReturnCorrectValue(){

            // Arrrange
            var a = 1;
            var b = 2;

            // Act
            var c = a + b;

            //Assert
            Assert.Equal(3, c);
        }
        [Theory]
        [InlineData(1,0, 1)]
        [InlineData(2, 2, 4)]
        public void Add_Params_ReturnCorrectValue(int a, int b, int expected)
        {
            // Arrrange
  
            // Act
            var c = a + b;

            //Assert
            Assert.Equal(expected, c);
        }

        [Fact]
        public void GetCurrentData()
        {
            // Arrrange
            var actualData = DateTime.Now;

            // Act

            var yearExcected = DateTime.Now.AddYears(2);

            //Assert
            Assert.True(yearExcected.Year >= 2024);
        }

        [Fact]
        public void ShouldBeValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
    }
}
