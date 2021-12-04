using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class SupplierEntityConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Supplier");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Name).IsRequired()
                .HasMaxLength(150);
            builder.Property(i => i.Password).IsRequired()
                .HasMaxLength(300);
            builder.Property(i => i.Email).IsRequired()
               .HasMaxLength(300);
            builder.Property(i => i.SSN).IsRequired()
               .HasMaxLength(14);
            builder.Property(i => i.Address).IsRequired()
                .HasMaxLength(500);
            builder.Property(i => i.Phone).IsRequired()
               .HasMaxLength(15);
            builder.Property(i => i.Credit_Card).IsRequired()
              .HasMaxLength(50);
        }

      
    }
}
