using CodingAssessment.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using CodingAssessment.Domain.Entities;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CodingAssessment.Infrastructure.Repositories
{
    public class FoodAndDrugEnforcementRepository : IFoodAndDrugEnforcementRepository
    {
        private readonly DbContext DbContext;

        public FoodAndDrugEnforcementRepository(DbContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public async Task<Meta> GetFoodDrugEnforcement(int pageNumber, int pageSize)
        {
            return await DbContext.Meta.Include(d => d.results.OrderBy(x => x.Id)
                                                              .Skip((pageNumber - 1) * pageSize)
                                                              .Take(pageSize)).FirstOrDefaultAsync();
        }

        public async Task<int> GetTotalCount()
        {
            return await DbContext.Result.CountAsync();
        }

        public async Task<Meta> GetFoodDrugEnforcementAll()
        {
            return await DbContext.Meta.Include(d => d.results).FirstOrDefaultAsync();
        }
    }
}