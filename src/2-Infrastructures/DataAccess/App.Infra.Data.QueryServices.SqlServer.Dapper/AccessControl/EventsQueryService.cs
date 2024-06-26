﻿using Framework.Core.Configs;
using System.Data;
using System.Data.SqlClient;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using Framework.Core.Utilities;

namespace App.Infra.Data.QueryServices.SqlServer.Dapper.AccessControl;

public class EventsQueryService: IEventsQueryServices
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
        table.Columns.Add("IDateTime", typeof(DateTime));
        table.Columns.Add("EventDateTime", typeof(DateTime));
        table.Columns.Add("EntryExitType", typeof(string));
        table.Columns.Add("MasterControllerId", typeof(int));
        table.Columns.Add("DoorControllerId", typeof(int));
        table.Columns.Add("SpecialFunctionId", typeof(int));
        table.Columns.Add("Leavedt", typeof(DateTime));
        table.Columns.Add("CreateAt", typeof(DateTime));

        foreach (var item in model.Events)
        {
            if (string.IsNullOrEmpty(item.leavedt))
                item.leavedt = null;

            if (string.IsNullOrEmpty(item.idatetime))
                item.idatetime = null;
        }

        model.Events.ForEach(x =>
            table.Rows.Add(null, x.indexno, x.userid, x.username,x.idatetime, x.eventdatetime.ToCorrectDateTimeString(),
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
}
