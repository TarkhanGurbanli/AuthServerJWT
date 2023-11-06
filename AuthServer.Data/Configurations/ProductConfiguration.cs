using AuthServer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Configurations
{
    //Burada IEntityTypeConfiguration bu interface i intherit alib implement elemek lazimdir
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Primary Key teyin edirik
            builder.HasKey(x => x.Id);
            //Product Name i bos gondermek olmaz deyirik(IsRequired), ve maks uzunugu 200 ola biler deyirik
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            //qiymetin nece olaagini qeyd edirik burada meselen:100000000000000.12 bele ola biler deyirik
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
