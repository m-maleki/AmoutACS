using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.CosecApi.Dtos;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using App.Infra.Data.Db.SqlServer.Ef.Entities.App;
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

    public async Task<List<DeviceOutputDto>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Devices.Select(x => new DeviceOutputDto
        {
            DeviceId = x.DeviceId,
            Name = x.Name,
            Active = x.Active,
            ConnectTime = x.ConnectTime,
            DId = x.DId,
            DeviceType = x.DeviceType,
            Mac = x.Mac,
            Status = x.Status,
            DisconnectTime = x.DisconnectTime,
            Ip = x.Ip,
        }).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<int> GetDeviceTypeWithDeviceId(int deviceId, CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Devices
            .FirstAsync(x => x.DeviceId == deviceId, cancellationToken);

        return result.DeviceType;
    }
    
    #endregion


}

