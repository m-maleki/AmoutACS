using App.Infra.Data.Db.SqlServer.Ef.Entities.App;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Db.SqlServer.Ef.Entities.Cosec;
public class UserAccessDbEntity
{
    public string UserId { get; set; }
    public DateTime EventDT { get; set; }
    public decimal Action { get; set; }
    public decimal MID { get; set; }
    public decimal DType { get; set; }
    public string SystemUserId { get; set; }
    public decimal AppSource { get; set; }
}

public class UserAccessDbEntityConfig : IEntityTypeConfiguration<UserAccessDbEntity>
{
    public void Configure(EntityTypeBuilder<UserAccessDbEntity> builder)
    {
        builder.ToTable("Mx_DeviceAssignmentDet", "dbo");
        builder.HasKey(i => i.UserId);
    }
}
