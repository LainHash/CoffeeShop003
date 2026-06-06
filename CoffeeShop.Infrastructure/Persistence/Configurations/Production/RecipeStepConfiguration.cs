using CoffeeShop.Domain.Entities.Production.Recipes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Infrastructure.Persistence.Configurations.Production
{
    public class RecipeStepConfiguration : IEntityTypeConfiguration<RecipeStep>
    {
        public void Configure(EntityTypeBuilder<RecipeStep> builder)
        {
            builder.ToTable("RecipeSteps");

            builder.HasKey(rs => rs.Id);

            builder.Property(rs => rs.Id)
                .UseIdentityColumn();

            builder.Property(r => r.PublicId)
                .IsRequired()
                .HasDefaultValueSql("newid()");

            builder.HasIndex(r => r.PublicId)
                .IsUnique();

            builder.Property(rs => rs.RecipeId)
                .IsRequired();

            builder.Property(rs => rs.StepNumber)
                .IsRequired();

            builder.Property(rs => rs.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(rs => rs.DurationSeconds)
                .IsRequired();

            // Check Constraint
            builder.ToTable(t => t.HasCheckConstraint("CK_RecipeSteps", "[StepNumber] > 0"));

            // Relationship
            builder.HasOne(rs => rs.Recipe)
                .WithMany(r => r.RecipeSteps)
                .HasForeignKey(rs => rs.RecipeId)
                .HasPrincipalKey(r => r.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
