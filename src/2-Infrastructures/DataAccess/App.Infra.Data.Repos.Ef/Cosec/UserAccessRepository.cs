using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.Cosec.Dtos;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using App.Infra.Data.Db.SqlServer.Ef.Entities.Cosec;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.Cosec;
public class UserAccessRepository : IUserAccessRepository
{
    private readonly CosecDbContext _cosecDbContext;

    public UserAccessRepository(CosecDbContext cosecDbContext)
    {
        _cosecDbContext = cosecDbContext;
    }

    public async Task<List<UserAccessDto>> GetAll(CancellationToken cancellationToken)
        => await _cosecDbContext.UserAccess
            .Select(x=> new UserAccessDto
            {
                UserId = x.UserId,
                Action = x.Action,
                AppSource = x.AppSource,
                DType = x.DType,
                EventDT = x.EventDT,
                MID = x.MID,
                SystemUserId = x.SystemUserId
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);


    public async Task<List<UserAccessDto>> GetAllByUserId(int userId, CancellationToken cancellationToken)
        => await _cosecDbContext.UserAccess
            .Where(x=>x.UserId == userId.ToString())
            .Select(x => new UserAccessDto
            {
                UserId = x.UserId,
                Action = x.Action,
                AppSource = x.AppSource,
                DType = x.DType,
                EventDT = x.EventDT,
                MID = x.MID,
                SystemUserId = x.SystemUserId
            }).ToListAsync(cancellationToken);
}

