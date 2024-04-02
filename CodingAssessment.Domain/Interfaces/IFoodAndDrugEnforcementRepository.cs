using System.Threading.Tasks;
using CodingAssessment.Domain.Entities;

namespace CodingAssessment.Domain.Interfaces
{
    public interface IFoodAndDrugEnforcementRepository
    {
        /// <summary>
        /// Gets the service foodanddrugenforcement
        /// </summary>
        /// <returns>The Service <see cref="Meta"/></returns>
        Task<Meta> GetFoodDrugEnforcement(int pageNumber, int pageSize);
        Task<int> GetTotalCount();

        Task<Meta> GetFoodDrugEnforcementAll();
    }
}
