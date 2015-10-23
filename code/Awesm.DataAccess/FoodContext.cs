using System.Data.Entity;
using Awesm.Domain;

namespace Awesm.DataAccess
{
	public class FoodContext : DbContext
	{
		public FoodContext()
			: base("FoodDb")
		{
            
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FoodDescriptionMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<FoodDescription> FoodDescriptions { get; set; }
	}
}
