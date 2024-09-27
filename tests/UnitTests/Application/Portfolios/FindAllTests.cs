using FakeItEasy;
using Microsoft.Extensions.Logging;
using ROFE.Domain.Models.Portfolio;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using FindAll = ROFE.Application.Portfolios.FindAll;


namespace UnitTests.Application.Portfolios
{
    public class FindAllTests
    {
        const ushort LIMIT = 200;
        const ushort OFFSET = 0;

        [Fact]
        public async Task GetAll_Filter_ReturnEmpty()
        {
            //Arrange
            var query = CreateQuery(true);
            var stubPortfolioRepo = A.Fake<IPortfolioRepository>();
            var stubLogger = A.Fake<ILogger<FindAll.FindAllQuery>>();

            A.CallTo(() => stubPortfolioRepo.CountAsync()).Returns(Task.FromResult(0));
            A.CallTo(() => stubPortfolioRepo.GetPagedAsync(A<int>._, LIMIT)).Returns([]);

            var handler = new FindAll.FindAllHandler(stubPortfolioRepo, stubLogger);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(0, result.Total);
        }

        [Fact]
        public async Task GetAll_Filter_ReturnOneResult()
        {
            //Arrange
            var query = CreateQuery(true);
            var portfolios = new List<Portfolio> { new(1) };
            var stubPortfolioRepo = A.Fake<IPortfolioRepository>();
            var stubLogger = A.Fake<ILogger<FindAll.FindAllQuery>>();

            A.CallTo(() => stubPortfolioRepo.CountAsync()).Returns(Task.FromResult(portfolios.Count));
            A.CallTo(() => stubPortfolioRepo.GetPagedAsync(A<int>._, LIMIT)).Returns(portfolios);

            var handler = new FindAll.FindAllHandler(stubPortfolioRepo, stubLogger);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(1, result.Total);
        }

        private static FindAll.FindAllQuery CreateQuery(bool enabled)
        {
            return new  FindAll.FindAllQuery()
            {
                Limit = LIMIT,
                Offset = OFFSET,
                Sort = string.Empty
            };
        }
    }
}
