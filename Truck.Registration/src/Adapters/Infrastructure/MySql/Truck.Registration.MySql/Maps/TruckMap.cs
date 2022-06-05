using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = Truck.Registration.Domain.Entities;

namespace Truck.Registration.MySql.Maps
{
    public class TruckMap : IEntityTypeConfiguration<Entity.Truck>
    {
        public void Configure(EntityTypeBuilder<Entity.Truck> builder)
        {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("Truck");

            builder.Property(e => e.Model).HasColumnName("model");
            builder.Property(e => e.ModelYear).HasColumnName("ModelYear");
            builder.Property(e => e.YearManufacture).HasColumnName("YearManufacture");

            builder.Property(e => e.CreateAt).HasColumnName("CreateAt");
            builder.Property(e => e.CreateUser).HasColumnName("CreateUser");
            builder.Property(e => e.UpdateAt).HasColumnName("UpdateAt");
            builder.Property(e => e.UpdateUser).HasColumnName("UpdateUser");
            builder.Property(e => e.Active).HasColumnName("Active");
        }
    }
}