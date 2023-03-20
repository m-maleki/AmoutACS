using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using Framework.Core.Configs;
using System.Data;
using System.Data.SqlClient;

namespace App.Infra.Data.QueryServices.SqlServer.Dapper.AccessControl;
public class DeviceQueryServices : IDeviceQueryServices
{
    #region Fields

    private readonly SiteSettings _siteSettings;

    #endregion

    #region Ctor
    public DeviceQueryServices(SiteSettings siteSettings)
    {
        _siteSettings = siteSettings;
    }

    #endregion

    #region Implementations
    public async Task BulkInsert(List<DeviceChildDto> model, CancellationToken cancellationToken)
    {
        DataTable table = new DataTable();

        table.Columns.Add("Id", typeof(string));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("SiteId", typeof(int));
        table.Columns.Add("Type", typeof(string));
        table.Columns.Add("DeviceType", typeof(int));
        table.Columns.Add("ApplicationType", typeof(int));
        table.Columns.Add("DoorId", typeof(int));
        table.Columns.Add("FingerTemplateType", typeof(string));
        table.Columns.Add("Ip", typeof(string));
        table.Columns.Add("Rs485", typeof(string));
        table.Columns.Add("Mac", typeof(string));

        model.ForEach(x =>
            table.Rows.Add(x.Id, x.Name, x.SiteId, x.Type, x.DeviceType,
                x.ApplicationType, x.DoorId, x.FingerTemplateType, x.Ip, x.Rs485, x.Mac));

        await using var connection = new SqlConnection(_siteSettings.ConnectionStrings.AppDb);
        await connection.OpenAsync(cancellationToken);

        using (var bulkCopy = new SqlBulkCopy(connection))
        {
            bulkCopy.DestinationTableName = "[dbo].[Devices]";
            await bulkCopy.WriteToServerAsync(table, cancellationToken);
        }
        await connection.CloseAsync();

    }

    #endregion


}

