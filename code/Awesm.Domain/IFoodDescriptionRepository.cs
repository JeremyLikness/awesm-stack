using System.Collections.Generic;
using System.Threading.Tasks;

namespace Awesm.Domain
{
    public interface IFoodDescriptionRepository
    {
        Task<ICollection<FoodDescription>> QueryFoodDescriptionsAsync(
            IWorkUnit workUnit,
            string searchText);

        Task<FoodDescription> UpdateOrAddFoodDescriptionAsync(
            IWorkUnit workUnit, 
            FoodDescription foodDescription);

        Task<bool> DeleteFoodDescriptionAsync(
            IWorkUnit workUnit,
            string id);
    }
}
