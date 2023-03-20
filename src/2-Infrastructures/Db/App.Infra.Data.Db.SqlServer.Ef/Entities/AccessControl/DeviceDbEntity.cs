using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.Entities.AccessControl;

public class DeviceDbEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SiteId { get; set; }
    public string Type { get; set; }
    public int DeviceType { get; set; }
    public int ApplicationType { get; set; }
    public int DoorId { get; set; }
    public string FingerTemplateType { get; set; }
    public string Ip { get; set; }
    public string Rs485 { get; set; }
    public string Mac { get; set; }
}

public class DeviceDbEntityConfig : IEntityTypeConfiguration<DeviceDbEntity>
{
    public void Configure(EntityTypeBuilder<DeviceDbEntity> builder)
    {
        builder.ToTable("Devices", "dbo");
        builder.HasKey(i => i.Id);
    }
}
