using CodingAssessment.Domain.Entities;
using CodingAssessment.Domain.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Meta = CodingAssessment.Domain.Entities.Meta;

namespace CodingAssessment.Infrastructure
{
    public class DbContextDataSeed
    {
        public static async Task SeedSampleDataAsync(DbContext context,string endpoint)
        {
            if (!context.Meta.Any())
            {
                var food = await HttpClientWrapper<FoodApiResponseModel>.Get(endpoint);

                context.Meta.Add(
                    new Meta
                    {
                        disclaimer = food.meta.disclaimer,
                        license = food.meta.license,
                        terms = food.meta.terms,
                        last_updated = food.meta.last_updated,
                        results = food.results.Select(f => new Domain.Entities.Result
                        {
                            country = f.country,
                            address_1 = f.address_1,
                            address_2 = f.address_2,
                            center_classification_date = f.center_classification_date,
                            city = f.city,
                            classification = f.classification,
                            code_info = f.code_info,
                            distribution_pattern = f.distribution_pattern,
                            event_id = f.event_id,
                            initial_firm_notification = f.initial_firm_notification,
                            more_code_info = f.more_code_info,
                            postal_code = f.postal_code,
                            product_description = f.product_description,
                            product_quantity = f.product_quantity,
                            product_type = f.product_type,
                            reason_for_recall = f.reason_for_recall,
                            recalling_firm = f.recalling_firm,
                            recall_initiation_date = f.recall_initiation_date,
                            recall_number = f.recall_number,
                            report_date = f.report_date,
                            state = f.state,
                            voluntary_mandated = f.voluntary_mandated,
                            status = f.status,
                            termination_date = f.termination_date
                        }).ToList()
                    });

                context.SaveChanges();
            }
        }
    }
}