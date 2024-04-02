using AutoMapper;
using FluentAssertions;
using CodingAssessment.Application.Dtos;
using CodingAssessment.Application.Mappers;
using CodingAssessment.Domain.Entities;
using System;
using Xunit;
using System.ComponentModel;

namespace Application.UnitTest.Mappers
{
    public class FoodAndDrugProfileMapperTests
    {
        public readonly IMapper _mapper;

        public FoodAndDrugProfileMapperTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProfileMapper>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public void MetaMapper_FromMetaToMetaResponse_Success()
        {
            var disclaimer = "Do not rely on openFDA to make decisions regarding medical care. While we make every effort to ensure that data is accurate, you should assume all results are unvalidated. We may limit or otherwise restrict your access to the API in line with our Terms of Service";
            var last_updated = "2024-03-27";
            var license = "https://open.fda.gov/license/";
            var terms = "https://open.fda.gov/terms/";

            // Arrange
            var meta = new Meta
            {
                disclaimer = disclaimer,
                last_updated = last_updated,
                license = license,
                terms = terms
            };

            // Act
            var response = _mapper.Map<MetaResponse>(meta);

            // Assert
            response.Should().NotBeNull();
            response.disclaimer.Should().Be(disclaimer);
            response.last_updated.Should().Be(last_updated);
            response.license.Should().Be(license);
            response.terms.Should().Be(terms);
        }

        [Fact]
        public void ResultMapper_FromResultToResultResponse_Success()
        {
            var postal_code = "Do not rely on openFDA to make decisions regarding medical care. While we make every effort to ensure that data is accurate, you should assume all results are unvalidated. We may limit or otherwise restrict your access to the API in line with our Terms of Service";
            var address_1 = "2024-03-27";
            var center_classification_date = "https://open.fda.gov/license/";
            var country = "https://open.fda.gov/terms/";

            // Arrange
            var meta = new Result
            {
                address_1 = address_1,
                center_classification_date = center_classification_date,
                postal_code = postal_code,
                country = country
            };

            // Act
            var response = _mapper.Map<ResultResponse>(meta);

            // Assert
            response.Should().NotBeNull();
            response.address_1.Should().Be(address_1);
            response.center_classification_date.Should().Be(center_classification_date);
            response.postal_code.Should().Be(postal_code);
            response.country.Should().Be(country);
        }
    }
}
