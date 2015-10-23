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
    public class WorkUnitTests
    {
        const string test = "Test description";

        [TestCategory("Integration")]
        [TestMethod]
        public void GivenWorkUnitWhenNotCommittedThenDatabaseShouldRemainTheSame()
        {
            var id = string.Empty;
            var date = DateTime.Now;
            using (var unit = new WorkUnit(Thread.CurrentPrincipal))
            {
                var firstItem = unit.Collection<FoodDescription>().FirstOrDefault();
                firstItem.SomeDate = date;
                id = firstItem.Id;
            }

            using (var ctx = new FoodContext())
            {
                var actual = ctx.FoodDescriptions.Find(id);
                if (actual == null)
                {
                    Assert.Fail("Item was not found.");
                }
                Assert.IsFalse(actual.SomeDate == date, "Test failed: date should not have been updated.");
            }
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GivenWorkUnitWhenCommittedThenDatabaseShouldUpdate()
        {
            var id = string.Empty;
            var date = DateTime.Now;
            using (var unit = new WorkUnit(Thread.CurrentPrincipal))
            {
                var firstItem = unit.Collection<FoodDescription>().FirstOrDefault();
                firstItem.SomeDate = date;
                id = firstItem.Id;
                await unit.CommitAsync();
            }

            using (var ctx = new FoodContext())
            {
                var actual = ctx.FoodDescriptions.Find(id);
                if (actual == null)
                {
                    Assert.Fail("Item was not found.");
                }
                Assert.IsTrue(actual.SomeDate == date, "Test failed: date should have been updated.");
            }
        }
    }
}
