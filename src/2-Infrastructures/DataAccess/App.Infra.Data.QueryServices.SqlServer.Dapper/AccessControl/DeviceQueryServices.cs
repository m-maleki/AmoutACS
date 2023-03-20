using Framework.Core.Configs;
using System.Data;
using System.Data.SqlClient;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using Framework.Core.Utilities;

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

        table.Columns.Add("DeviceId", typeof(int));
        table.Columns.Add("DeviceType", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("DId", typeof(int));
        table.Columns.Add("Active", typeof(int));
        table.Columns.Add("Ip", typeof(string));
        table.Columns.Add("Mac", typeof(string));
        table.Columns.Add("Status", typeof(string));
        table.Columns.Add("ConnectTime", typeof(DateTime));
        table.Columns.Add("DisconnectTime", typeof(DateTime));

        model.ForEach(x =>
            table.Rows.Add(x.DeviceId, x.DeviceType, x.Name, x.DId, x.Active,
                x.Ip, x.Mac, x.Status, x.ConnectTime.ToCorrectDateTimeString(), x.DisconnectTime.ToCorrectDateTimeString()));

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

