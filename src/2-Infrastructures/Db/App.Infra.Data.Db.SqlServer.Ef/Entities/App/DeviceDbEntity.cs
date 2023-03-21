using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.Entities.App;

public class DeviceDbEntity
{
    public int DeviceId { get; set; }
    public int DeviceType { get; set; }
    public string Name { get; set; }
    public int DId { get; set; }
    public int Active { get; set; }
    public string Ip { get; set; }
    public string Mac { get; set; }
    public string Status { get; set; }
    public DateTime? ConnectTime { get; set; }
    public DateTime? DisconnectTime { get; set; }
}

public class DeviceDbEntityConfig : IEntityTypeConfiguration<DeviceDbEntity>
{
    public void Configure(EntityTypeBuilder<DeviceDbEntity> builder)
    {
        builder.ToTable("Devices", "dbo");
        builder.HasKey(i => i.DeviceId);
    }
}
