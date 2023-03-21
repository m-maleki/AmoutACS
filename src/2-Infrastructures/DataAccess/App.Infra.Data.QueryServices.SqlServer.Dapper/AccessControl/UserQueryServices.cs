using Framework.Core.Configs;
using System.Data;
using System.Data.SqlClient;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.AccessControl.CosecApi.QueryServices;
using Framework.Core.Utilities;

namespace App.Infra.Data.QueryServices.SqlServer.Dapper.AccessControl;

public class UserQueryServices : IUserQueryServices
{
    #region Fields

    private readonly SiteSettings _siteSettings;

    #endregion

    #region Ctor

    public UserQueryServices(SiteSettings siteSettings)
    {
        _siteSettings = siteSettings;
    }

    #endregion

    #region Implementations

    public async Task BulkInsert(List<UserChildDto> model, CancellationToken cancellationToken)
    {
        DataTable table = new DataTable();

        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("ReferenceCode", typeof(int));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("ShortName", typeof(string));
        table.Columns.Add("FullName", typeof(string));
        table.Columns.Add("Active", typeof(int));
        table.Columns.Add("Pin", typeof(string));
        table.Columns.Add("Card1", typeof(string));
        table.Columns.Add("Card2", typeof(string));
        table.Columns.Add("AccessValidityDate", typeof(DateTime));
        table.Columns.Add("Organization", typeof(int));
        table.Columns.Add("Branch", typeof(int));
        table.Columns.Add("Department", typeof(int));
        table.Columns.Add("Designation", typeof(int));
        table.Columns.Add("Section", typeof(int));
        table.Columns.Add("Category", typeof(int));
        table.Columns.Add("Grade", typeof(int));
        table.Columns.Add("LeaveGroup", typeof(int));
        table.Columns.Add("AccessLevel", typeof(int));
        table.Columns.Add("enrolled_fingers", typeof(int));
        table.Columns.Add("enrolled_faces", typeof(int));

        foreach (var userChildDto in model)
        {
            if (string.IsNullOrEmpty(userChildDto.accessvaliditydate))
                userChildDto.accessvaliditydate = null;
            else
                userChildDto.accessvaliditydate = userChildDto.accessvaliditydate.ToCorrectDateTimeString();

        }

        model.ForEach(x =>
            table.Rows.Add(x.id, x.referencecode, x.name, x.shortname, x.fullname,
                x.active, x.pin, x.card1, x.card2, x.accessvaliditydate, x.organization, x.branch,
                x.department, x.designation, x.section, x.category, x.grade, x.leave_group, x.accesslevel,
                x.enrolled_fingers,x.enrolled_faces));

        await using var connection = new SqlConnection(_siteSettings.ConnectionStrings.AppDb);
        await connection.OpenAsync(cancellationToken);

        using (var bulkCopy = new SqlBulkCopy(connection))
        {

            bulkCopy.DestinationTableName = "[dbo].[Users]";
            await bulkCopy.WriteToServerAsync(table, cancellationToken);

        }
        await connection.CloseAsync();

    }

    #endregion

}
