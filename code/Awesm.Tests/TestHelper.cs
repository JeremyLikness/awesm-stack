using Awesm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awesm.Tests
{
    public static class TestHelper
    {
        public static string GetNumber()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty);
        }

        public static string GetName()
        {
            return Guid.NewGuid().ToString();
        }

        public static string GetDescription()
        {
            return string.Format("{0}{1}{2}{3}", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        }

        public static FoodDescription GetNewFoodDescription()
        {
            var item = new FoodDescription
            {
                Id = GetNumber(),
                Name = GetName(),
                Description = GetDescription(),
                SomeDate = DateTimeOffset.Now
            };            
            return item;
        }
    }
}
