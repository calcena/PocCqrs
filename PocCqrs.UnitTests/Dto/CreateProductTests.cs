namespace PocCqrs.Test.UnitTest.Dto
{
    using PocCqrs.Application.Models;
    using Xunit;
    public class CreateProductTests : BaseTests
    {
        [Theory]
        [InlineData("99", 1)]
        [InlineData("009",0)]
        [InlineData("0009", 0)]
        [InlineData(null, 1)]
        [InlineData("00099", 0)]
        [InlineData("000999", 1)]
        public void ValidateModel_ReturnsCorrectNumberOfErrors(string code, int numberErrorExpected)
        {
           // Arrange
            var request = new ProductDto
            {
                Code = code
            };

            // Act
            var errorList = ValidateModel(request);

            // Assert
            Assert.Equal(numberErrorExpected, errorList.Count);
        }
    }
}
