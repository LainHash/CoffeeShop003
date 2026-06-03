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

            builder.HasKey(rs => rs.RecipeStepId);

            builder.Property(rs => rs.RecipeStepId)
                .UseIdentityColumn();

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
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
