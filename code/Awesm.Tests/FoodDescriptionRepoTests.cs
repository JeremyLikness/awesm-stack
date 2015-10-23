using Awesm.DataAccess;
using Awesm.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Awesm.Tests
{
    [TestClass]
    public class FoodDescriptionRepoTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GivenRepoWhenDeleteCalledThenItemIsRemoved()
        {
            var id = string.Empty;

            using (var ctx = new FoodContext())
            {
                var description = TestHelper.GetNewFoodDescription();
                id = description.Id;
                ctx.FoodDescriptions.Add(description);
                await ctx.SaveChangesAsync();
            }

            using (var unit = new WorkUnit(Thread.CurrentPrincipal))
            {
                var repo = new FoodDescriptionRepo();
                var success = await repo.DeleteFoodDescriptionAsync(unit, id);
                Assert.IsTrue(success, "Test failed: operation should have returned true.");
                await unit.CommitAsync();
            }

            using (var ctx = new FoodContext())
            {
                var exists = ctx.FoodDescriptions.Any(f => f.Id == id);
                Assert.IsFalse(exists, "Test failed: item was not deleted.");
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GivenRepoWhenNewItemThenItemIsInserted()
        {
            var newItem = TestHelper.GetNewFoodDescription();

            using (var unit = new WorkUnit(Thread.CurrentPrincipal))
            {
                var repo = new FoodDescriptionRepo();
                var success = await repo.UpdateOrAddFoodDescriptionAsync(unit, newItem);
                Assert.IsNotNull(success, "Test failed: operation should have returned the item.");
                await unit.CommitAsync();
            }

            using (var ctx = new FoodContext())
            {
                var exists = ctx.FoodDescriptions.FirstOrDefault(f => f.Id == newItem.Id);
                Assert.IsNotNull(exists, "Test failed: item was not inserted.");
                ctx.FoodDescriptions.Remove(exists);
                await ctx.SaveChangesAsync();
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GivenRepoWhenExistingItemThenItemIsUpdated()
        {
            FoodDescription item;
            DateTimeOffset date;

            using (var ctx = new FoodContext())
            {
                item = ctx.FoodDescriptions.AsNoTracking().FirstOrDefault();
                date = item.SomeDate;
            }

            using (var unit = new WorkUnit(Thread.CurrentPrincipal))
            {
                var repo = new FoodDescriptionRepo();
                item.SomeDate = date.AddDays(1);
                var success = await repo.UpdateOrAddFoodDescriptionAsync(unit, item);
                Assert.IsNotNull(success, "Test failed: operation should have returned the item.");
                await unit.CommitAsync();
            }

            using (var ctx = new FoodContext())
            {
                var actual = ctx.FoodDescriptions.FirstOrDefault(f => f.Id == item.Id);
                Assert.IsNotNull(actual, "Test failed: unable to obtain the item.");
                Assert.AreNotEqual(date, actual.SomeDate, "Test failed: the date was not updated.");
            }
        }
    }
}
