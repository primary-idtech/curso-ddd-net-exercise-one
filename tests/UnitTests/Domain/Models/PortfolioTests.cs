using ROFE.Domain;
using ROFE.Domain.Models.Portfolio;
using Xunit;

namespace UnitTests.Domain.Models
{
    public class PortfolioTests
    {
        [Fact]
        public void Portfolio_WithValidData_ShouldCreate()
        {
            // Arrange
            var id = 1;

            // Act
            var portfolio = new Portfolio(id);

            // Assert
            Assert.NotNull(portfolio);
            Assert.Equal(id, portfolio.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Portfolio_WithEmptyName_ShouldThrowException(int id)
        {
            // Arrange

            // Act
            Portfolio act() => new(id);

            // Assert
            Assert.Throws<BusinessException>(act);
        }
    }
}
