using AutoMapper;
using FluentAssertions;
using Moq;
using CodingAssessment.Application.Interfaces;
using CodingAssessment.Application.Mappers;
using CodingAssessment.Application.Services;
using CodingAssessment.Domain.Entities;
using CodingAssessment.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Application.UnitTest.Services
{
    public class FoodAndDrugServiceTests
    {
        private readonly Mock<IFoodAndDrugEnforcementRepository> _mockfdeRepository;
        private readonly Mock<ISmtpClientRepository> _mockSmtpRepository;
        private readonly IFoodAndDrugEnforcementService _fdeService;

        public FoodAndDrugServiceTests()
        {
            var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ProfileMapper>(); });

            var mapper = configurationProvider.CreateMapper();

            _mockfdeRepository = new Mock<IFoodAndDrugEnforcementRepository>();
            _mockSmtpRepository = new Mock<ISmtpClientRepository>();
            _fdeService = new FoodAndDrugEnforcementService(_mockfdeRepository.Object, _mockSmtpRepository.Object, mapper);
        }

        [Fact]
        public async Task FoodAndDrugService_GetFoodAndDrugEnforcement_Success()
        {
            // Arrange

            var disclaimer = "Do not rely on openFDA to make decisions regarding medical care. While we make every effort to ensure that data is accurate, you should assume all results are unvalidated. We may limit or otherwise restrict your access to the API in line with our Terms of Service";
            var last_updated = "2024-03-27";
            var license = "https://open.fda.gov/license/";
            var terms = "https://open.fda.gov/terms/";
            var meta = new Meta
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
            };

            _mockfdeRepository
                .Setup(x => x.GetFoodDrugEnforcement(1,3))
                .ReturnsAsync(meta)
                .Verifiable();

            // Act
            var response = await _fdeService.GetFoodAndDrugEnforcementAsync(1,3);

            // Arrange
            response.Should().NotBeNull();
            _mockfdeRepository.Verify(x => x.GetFoodDrugEnforcement(1,3), Times.Once());

        }
    }
}
