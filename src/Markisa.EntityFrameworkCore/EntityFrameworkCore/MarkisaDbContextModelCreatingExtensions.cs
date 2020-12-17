using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Markisa.EntityFrameworkCore
{
    public static class MarkisaDbContextModelCreatingExtensions
    {
        public static void ConfigureMarkisa(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(MarkisaConsts.DbTablePrefix + "YourEntities", MarkisaConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}