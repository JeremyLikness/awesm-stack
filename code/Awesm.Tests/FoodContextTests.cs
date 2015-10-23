using Microsoft.VisualStudio.TestTools.UnitTesting;
using Awesm.DataAccess;
using System.Linq;

namespace Awesm.Tests
{
    [TestClass]
    public class FoodContextTests
    {
        [TestCategory("Integration")] 
        [TestMethod]
        public void GivenContextWhenFoodDescriptionsFetchedThenShouldReturnList()
        {
            using (var ctx = new FoodContext())
            {
                var list = ctx.FoodDescriptions.ToList();
                Assert.IsNotNull(list, "Test failed: list should exist.");
                Assert.IsTrue(list.Count > 0, "Test failed: at least one item should exist.");
            }
        }
    }
}
