using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.Entities.App;

public class EventsProcessedDbEntity
{
    public int Id { get; set; }
    public int LastIndexNo { get; set; }
    public int ProcessedCount { get; set; }
    public DateTime CreateAt { get; set; }
}

public class EventsProcessedDbEntityConfig : IEntityTypeConfiguration<EventsProcessedDbEntity>
{
    public void Configure(EntityTypeBuilder<EventsProcessedDbEntity> builder)
    {
        builder.ToTable("EventsProcessed", "dbo");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();
    }
}