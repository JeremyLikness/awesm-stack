using Awesm.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awesm.DataAccess
{
    public class FoodDescriptionMap : EntityTypeConfiguration<FoodDescription>
    {
        public FoodDescriptionMap()
        {
            Property(f => f.Id).HasColumnName("Number");            
        }
    }
}
