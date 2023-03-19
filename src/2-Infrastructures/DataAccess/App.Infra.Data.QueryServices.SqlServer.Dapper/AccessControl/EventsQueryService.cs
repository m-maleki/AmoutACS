using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using Framework.Core.Configs;
using System.Data;
using System.Data.SqlClient;

namespace App.Infra.Data.QueryServices.SqlServer.Dapper.AccessControl;

public class EventsQueryService: IEventsQueryService
{
    #region Fields

    private readonly SiteSettings _siteSettings;

    #endregion

    #region Ctor

    public EventsQueryService(SiteSettings siteSettings)
    {
        _siteSettings = siteSettings;
    }

    #endregion

    #region Implementations
    public async Task BulkInsert(EventDto model, CancellationToken cancellationToken)
    {
        DataTable table = new DataTable();

        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("IndexNo", typeof(int));
        table.Columns.Add("UserId", typeof(int));
        table.Columns.Add("Username", typeof(string));
        table.Columns.Add("EventDateTime", typeof(DateTime));
        table.Columns.Add("EntryExitType", typeof(string));
        table.Columns.Add("MasterControllerId", typeof(int));
        table.Columns.Add("DoorControllerId", typeof(int));
        table.Columns.Add("SpecialFunctionId", typeof(int));
        table.Columns.Add("Leavedt", typeof(string));
        table.Columns.Add("CreateAt", typeof(DateTime));

        model.Events.ForEach(x =>
            table.Rows.Add(null, x.indexno, x.userid, x.username, DateTimeCorrection(x.eventdatetime),
                x.entryexittype, x.mastercontrollerid, x.doorcontrollerid, x.specialfunctionid,
                x.leavedt, x.idatetime));

        await using var connection = new SqlConnection(_siteSettings.ConnectionStrings.AppDb);
        await connection.OpenAsync(cancellationToken);

        using (var bulkCopy = new SqlBulkCopy(connection))
        {

            bulkCopy.DestinationTableName = "[dbo].[Events]";
            await bulkCopy.WriteToServerAsync(table, cancellationToken);

        }
        await connection.CloseAsync();

    }


    #endregion

    #region PrivateMethods

    private string DateTimeCorrection(string date)
    {
        var day = date.Substring(0, 2);
        var month = date.Substring(3, 2);
        var year = date.Substring(6, 4);
        var time = date.Substring(11, 8);
        return $"{month}-{day}-{year} {time}";
    }

    #endregion
}
