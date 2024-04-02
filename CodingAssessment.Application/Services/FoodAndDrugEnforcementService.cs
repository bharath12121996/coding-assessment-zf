using AutoMapper;
using CodingAssessment.Application.Dtos;
using CodingAssessment.Domain.Interfaces;
using CodingAssessment.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IFoodAndDrugEnforcementService = CodingAssessment.Application.Interfaces.IFoodAndDrugEnforcementService;

namespace CodingAssessment.Application.Services
{
    public class FoodAndDrugEnforcementService : IFoodAndDrugEnforcementService
    {
        private readonly IFoodAndDrugEnforcementRepository _foodAndDrugEnforcementRepository;
        private readonly ISmtpClientRepository _smtpClientRepository;
        private readonly IMapper _mapper;

        public FoodAndDrugEnforcementService(IFoodAndDrugEnforcementRepository foodAndDrugEnforcementRepository, ISmtpClientRepository smtpClientRepository, IMapper mapper)
        {
            _foodAndDrugEnforcementRepository = foodAndDrugEnforcementRepository;
            _smtpClientRepository = smtpClientRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<FoodAndDrugResponse>> GetFoodAndDrugEnforcementAsync(int pageNumber, int pageSize)
        {

            var totalRecords = await _foodAndDrugEnforcementRepository.GetTotalCount();
            var response = await _foodAndDrugEnforcementRepository.GetFoodDrugEnforcement(pageNumber, pageSize);

            var fdaresponse = new FoodAndDrugResponse
            {
                meta = _mapper.Map<MetaResponse>(response),
                result = _mapper.Map<List<ResultResponse>>(response.results)
            };
            var pagedResponse = new PagedResponse<FoodAndDrugResponse>(fdaresponse, pageNumber, pageSize, totalRecords);

            return pagedResponse;
        }

        public async Task GetFoodAndDrugEnforcementSendEmailAsync()
        {
            var response = await _foodAndDrugEnforcementRepository.GetFoodDrugEnforcementAll();
            if (null != response)
            {
                var fdaresponse = new FoodAndDrugResponse
                {
                    meta = _mapper.Map<MetaResponse>(response),
                    result = _mapper.Map<List<ResultResponse>>(response.results)
                };
                await _smtpClientRepository.SendEmailAsync(JsonConvert.SerializeObject(fdaresponse));
            }
        }
    }
}
