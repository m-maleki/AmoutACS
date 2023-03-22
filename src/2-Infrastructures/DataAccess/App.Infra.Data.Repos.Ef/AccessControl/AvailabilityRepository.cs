using App.Domain.Core.Cosec.Data.Repositories;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.AccessControl;
public class AvailabilityRepository : IAvailabilityRepository
{
    #region Fields

    private readonly AppDbContext _appDbContext;
    private readonly CosecDbContext _cosecDbContext;

    #endregion

    #region Ctor
    public AvailabilityRepository(AppDbContext appDbContext,
        CosecDbContext cosecDbContext)
    {
        _appDbContext = appDbContext;
        _cosecDbContext = cosecDbContext;
    }

    #endregion

    #region Implementations

    public async Task<bool> AvailableCosecDatabase(CancellationToken cancellationToken)
        => await _cosecDbContext.Database.CanConnectAsync(cancellationToken);

    public async Task<bool> AvailableAcsDatabase(CancellationToken cancellationToken)
        => await _appDbContext.Database.CanConnectAsync(cancellationToken);

    #endregion

}
