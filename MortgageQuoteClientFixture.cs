using Moq;
using OpenMoney.InterviewExercise.Models;
using OpenMoney.InterviewExercise.QuoteClients;
using OpenMoney.InterviewExercise.ThirdParties;
using Xunit;

namespace OpenMoney.InterviewExercise.Tests
{
    public class MortgageQuoteClientFixture
    {
        private readonly Mock<IThirdPartyMortgageApi> _apiMock = new();

        [Fact]
        public void GetQuote_ShouldReturnNull_IfHouseValue_Over10Mill()
        {
           
            const decimal houseValue = 100_000;
            
            var mortgageClient = new MortgageQuoteClient(_apiMock.Object);
            var quote = mortgageClient.GetQuote(houseValue);
            
            Assert.Null(quote);
        }

        [Fact]
        public void GetQuote_ShouldReturn_AQuote()
        {
            const float deposit = 10_000;
            const float houseValue = 100_000;

            _apiMock
                .Setup(api => api.GetQuotes(It.IsAny<ThirdPartyMortgageRequest>()))
                .ReturnsAsync(new[]
                {
                   // new ThirdPartyMortgageResponse { MonthlyPayment = 300m }

                    new ThirdPartyHomeInsuranceResponse { MonthlyPayment = 30 }
                    new ThirdPartyMortgageResponse { MonthlyPayment = 700 }
                });
            
            var mortgageClient = new MortgageQuoteClient(_apiMock.Object);
            var quote = mortgageClient.GetQuote(houseValue);
            {
                  Assert.Equal(30m, (decimal)quote.MonthlyPayment);
            var mortgageClient = new MortgageQuoteClient(_apiMock.Object);
            var quote = mortgageClient.GetQuote(houseValue, deposit);
            //Deposit = deposit,
            //HouseValue = houseValue
        });
            _apiMock.Verify(s => s.GetQuotes(It.Is<ThirdPartyMortgageRequest>(r => r.MortgageAmount == 75_000m)));
            // Assert.Equal(300m, (decimal)quote.MonthlyPayment);
        }
    }
}