using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using GlobalBlue_AustriaPurchases.API.Queries.CalculatePurchase;
using GlobalBlue_AustriaPurchases.DomainModel.ValueObject.Enum;
using GlobalBlue_AustriaPurchases.DomainModel.Domain.Model;

namespace GlobalBlue_AustriaPurchases.Test
{
    public class CalculatePurchaseTests
    {
        private CalculatePurchaseHandler _calcHandler;

        [SetUp]
        public void Setup()
        {
            _calcHandler = new CalculatePurchaseHandler();
        }

        [Test]
        public async Task CalculatePurchase_InvalidGrossValue()
        {
            var query = new CalculatePurchaseQuery() {
                Gross = 0
            };

            var context = new ValidationContext(query, null, null);
            var results = new List<ValidationResult>();
            var valid = Validator.TryValidateObject(query, context, results, true);

            Assert.False(valid);
        }

        [Test]
        public async Task CalculatePurchase_BasicGrossTest()
        {
            var query = new CalculatePurchaseQuery()
            {
                PurchaseRate = PurchaseRateEnum.Ten,
                Vat = 100
            };

            var result = await _calcHandler.Handle(query, CancellationToken.None);
            Assert.IsNotNull(result);

            result.Net.Value.Should().Be(1500);
            result.Gross.Value.Should().Be(1800);
        }
    }
}