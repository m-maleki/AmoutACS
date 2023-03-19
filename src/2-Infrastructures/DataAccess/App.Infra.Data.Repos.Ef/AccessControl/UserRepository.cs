using App.Domain.Core.AccessControl.CosecApi.Data.Repositories;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using App.Infra.Data.Db.SqlServer.Ef.Entities.AccessControl;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repos.Ef.AccessControl;

public class UserRepository : IUserRepository
{
    #region Fields

    private readonly AppDbContext _appDbContext;

    #endregion

    #region Ctor

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #endregion

    #region Implementations
    public async Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _appDbContext.Users
            .AsNoTracking()
            .Select(x=> new UserOutputDto
            {
                Id = x.Id,
                ReferenceCode = x.ReferenceCode,
                Name = x.Name,
                ShortName = x.ShortName,
                FullName = x.FullName,
                Active = x.Active,
                Pin= x.Pin,
                Card1 = x.Card1,
                Card2 = x.Card2,
                AccessValidityDate = x.AccessValidityDate,
                Organization = x.Organization,
                Branch = x.Branch,
                Department = x.Department,
                Designation = x.Designation,
                Section = x.Section,
                Category = x.Category,
                Grade = x.Grade,
                LeaveGroup = x.LeaveGroup,
                AccessLevel = x.AccessLevel

            }).ToListAsync(cancellationToken);

        return result;
    }

    #endregion

}
