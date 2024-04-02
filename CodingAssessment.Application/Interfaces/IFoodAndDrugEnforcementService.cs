using CodingAssessment.Application.Dtos;
using CodingAssessment.Domain.Model;
using System.Threading.Tasks;

namespace CodingAssessment.Application.Interfaces
{
    public interface IFoodAndDrugEnforcementService
    {
        /// <summary>
        /// Get the FoodAndDrugResponse of the service
        /// </summary>
        /// <returns>The <see cref="FoodAndDrugResponse"/></returns>
        Task<PagedResponse<FoodAndDrugResponse>> GetFoodAndDrugEnforcementAsync(int pageNumber, int pageSize);

        Task GetFoodAndDrugEnforcementSendEmailAsync();
    }
}
