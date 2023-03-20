using App.Domain.Core.AccessControl.CosecApi.Data.Repositories;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.AccessControl;
public class DeviceRepository : IDeviceRepository
{
    #region Fields

    private readonly AppDbContext _appDbContext;

    #endregion

    #region Ctor

    public DeviceRepository(AppDbContext dbContext)
    {
        _appDbContext = dbContext;
    }

    #endregion

    #region Implementations

    public async Task DeleteAll(CancellationToken cancellationToken)
    {
        _appDbContext.Devices.RemoveRange(_appDbContext.Devices);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCount(CancellationToken cancellationToken)
        => await _appDbContext.Devices.CountAsync(cancellationToken);

    public async Task<int> GetActiveDevicesCount(CancellationToken cancellationToken)
        => await _appDbContext.Devices.CountAsync(x => x.Status == "connected", cancellationToken);

    #endregion


}

