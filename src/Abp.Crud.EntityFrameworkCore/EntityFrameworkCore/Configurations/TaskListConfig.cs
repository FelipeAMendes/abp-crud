using Abp.Crud.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Abp.Crud.EntityFrameworkCore.Configurations;

public class TaskListConfig : IEntityTypeConfiguration<TaskList>
{
    public void Configure(EntityTypeBuilder<TaskList> builder)
    {
        builder.ToTable(name: "TaskList", schema: "AbpCrud");
        builder.ConfigureByConvention();

        builder.Property(x => x.Title).IsRequired().HasMaxLength(TaskListSpecification.TitleColumnSize);
        builder.Property(x => x.AssignedTo).IsRequired(false).HasMaxLength(TaskListSpecification.AssignedToColumnSize);
        builder.Property(x => x.DueDate);
        builder.Property(x => x.Status);
    }
}