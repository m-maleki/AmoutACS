using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Db.SqlServer.Ef.Entities.AccessControl;

public class EventDbEntity
{
    public int Id { get; set; }
    public int IndexNo { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public DateTime EventDateTime { get; set; }
    public string EntryExitType { get; set; }
    public int MasterControllerId { get; set; }
    public int DoorControllerId { get; set; }
    public int SpecialFunctionId { get; set; }
    public DateTime? Leavedt { get; set; }
    public DateTime IDateTime { get; set; }
}

public class EventDbEntityConfig : IEntityTypeConfiguration<EventDbEntity>
{
    public void Configure(EntityTypeBuilder<EventDbEntity> builder)
    {
        builder.ToTable("Events", "dbo");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();
    }
}