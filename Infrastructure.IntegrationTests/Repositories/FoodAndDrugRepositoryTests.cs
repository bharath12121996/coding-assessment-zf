using CodingAssessment.Domain.Entities;
using CodingAssessment.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Infrastructure.IntegrationTests.Repositories
{
    [Collection("DatabaseCollectionFixture")]
    public class FoodAndDrugEnforcementRepositoryTests
    {
        private readonly FoodAndDrugEnforcementRepository _fdeRepository;
        private readonly DatabaseFixture _fixture;

        public FoodAndDrugEnforcementRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _fdeRepository = new FoodAndDrugEnforcementRepository(_fixture.DbContext);
        }

        [Fact]
        public async Task FoodAndDrugEnforcementRepository_GetFoodDrugEnforcement_Success()
        {
            // Arrange
            var disclaimer = "Do not rely on openFDA to make decisions regarding medical care. While we make every effort to ensure that data is accurate, you should assume all results are unvalidated. We may limit or otherwise restrict your access to the API in line with our Terms of Service";
            var last_updated = "2024-03-27";
            var license = "https://open.fda.gov/license/";
            var terms = "https://open.fda.gov/terms/";

            await _fixture.DbContext.Meta.AddAsync(
                new Meta
                {
                    disclaimer = disclaimer,
                    last_updated = last_updated,
                    license = license,
                    terms = terms,
                    results = new List<Result>
                    {
                        new Result(),
                        new Result(),
                        new Result()
                    }
                });

            await _fixture.DbContext.SaveChangesAsync();

            // Act
            var response = await _fdeRepository.GetFoodDrugEnforcement(1,1);

            // Assert
            response.Should().NotBeNull();
            response.disclaimer.Should().Be(disclaimer);
            response.last_updated.Should().Be(last_updated);
            response.license.Should().Be(license);
            response.terms.Should().Be(terms);
        }

        [Fact]
        public async Task FoodAndDrugEnforcementRepository_GetTotalCount_Success()
        {
            // Arrange
            var disclaimer = "Do not rely on openFDA to make decisions regarding medical care. While we make every effort to ensure that data is accurate, you should assume all results are unvalidated. We may limit or otherwise restrict your access to the API in line with our Terms of Service";
            var last_updated = "2024-03-27";
            var license = "https://open.fda.gov/license/";
            var terms = "https://open.fda.gov/terms/";

            await _fixture.DbContext.Meta.AddAsync(
                new Meta
                {
                    disclaimer = disclaimer,
                    last_updated = last_updated,
                    license = license,
                    terms = terms,
                    results = new List<Result>
                    {
                        new Result(),
                        new Result(),
                        new Result()
                    }
                });

            await _fixture.DbContext.SaveChangesAsync();

            // Act
            var response = await _fdeRepository.GetTotalCount();

            // Assert
            response.Should().BeGreaterThan(2);
        }
    }
}
