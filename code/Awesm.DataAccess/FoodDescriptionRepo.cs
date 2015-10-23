using Awesm.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Awesm.DataAccess
{
    public class FoodDescriptionRepo : IFoodDescriptionRepository
    {
        public async Task<bool> DeleteFoodDescriptionAsync(IWorkUnit workUnit, string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id");
            }

            if (workUnit == null)
            {
                throw new ArgumentNullException("workUnit");
            }

            var ctx = workUnit.GetContext<FoodContext>();
            var foodItem = await ctx.FoodDescriptions.FirstOrDefaultAsync(
                f => f.Id == id);

            if (foodItem != null)
            {
                ctx.Entry(foodItem).State = EntityState.Deleted;
            }

            return true;
        }

        public async Task<ICollection<FoodDescription>> QueryFoodDescriptionsAsync(IWorkUnit workUnit, string searchText)
        {
            if (workUnit == null)
            {
                throw new ArgumentNullException("workUnit");
            }

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return await workUnit.Collection<FoodDescription>().ToListAsync();
            }

            return await workUnit.Collection<FoodDescription>()
                .Where(f => f.Name.Contains(searchText) || f.Description.Contains(searchText))
                .ToListAsync();
        }

        public async Task<FoodDescription> UpdateOrAddFoodDescriptionAsync(IWorkUnit workUnit, FoodDescription foodDescription)
        {
            if (workUnit == null)
            {
                throw new ArgumentNullException("workUnit");
            }

            if (foodDescription == null)
            {
                throw new ArgumentNullException("foodDescription");
            }

            var exists = await workUnit.Collection<FoodDescription>()
                .Select(f => f.Id)
                .FirstOrDefaultAsync(n => n == foodDescription.Id);

            workUnit.GetContext<FoodContext>().Entry(foodDescription).State =
                exists == null ? EntityState.Added : EntityState.Modified;

            return foodDescription;
        }
    }
}
