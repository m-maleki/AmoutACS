using System.Formats.Asn1;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Data.Repositories;
using App.Infra.Data.Db.SqlServer.Ef.DbContexts;
using App.Infra.Data.Db.SqlServer.Ef.Entities.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            .Select(x => new UserOutputDto
            {
                Id = x.Id,
                ReferenceCode = x.ReferenceCode,
                Name = x.Name,
                ShortName = x.ShortName,
                FullName = x.FullName,
                Active = x.Active,
                Pin = x.Pin,
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
                AccessLevel = x.AccessLevel,
                enrolled_faces = x.enrolled_faces,
                enrolled_fingers = x.enrolled_fingers

            }).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<int> GetCount(CancellationToken cancellationToken)
        => await _appDbContext.Users.CountAsync(cancellationToken);

    public async Task<UserOutputDto?> GetById(string id, CancellationToken cancellationToken)
    {
        return await _appDbContext.Users
            .Select(x => new UserOutputDto
            {
                Id = x.Id,
                ReferenceCode = x.ReferenceCode,
                Name = x.Name,
                ShortName = x.ShortName,
                FullName = x.FullName,
                Active = x.Active,
                Pin = x.Pin,
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
                AccessLevel = x.AccessLevel,
                enrolled_faces = x.enrolled_faces,
                enrolled_fingers = x.enrolled_fingers
            })
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    }

    public async Task Update(UserChildDto model, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == model.id, cancellationToken: cancellationToken);

        if (entity != null)
        {
            #region Mapping
            entity.Name = model.name;
            entity.Active = int.Parse(model.active);
            entity.AccessLevel = int.Parse(model.accesslevel);
            entity.Organization = int.Parse(model.organization);
            entity.Branch = int.Parse(model.branch);
            entity.enrolled_faces = int.Parse(model.enrolled_faces);
            entity.ShortName = model.shortname;
            entity.Section = Convert.ToInt32(model.section);
            entity.Card1 = model.card1;
            entity.Card2 = model.card2;
            entity.Pin = model.pin;
            entity.enrolled_fingers = Convert.ToInt32(model.enrolled_fingers);
            entity.AccessValidityDate = string.IsNullOrEmpty(model.accessvaliditydate) ? null
                : DateTime.ParseExact(model.accessvaliditydate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture);
            entity.Category = entity.Category;
            entity.Department = entity.Department;
            entity.ReferenceCode = entity.ReferenceCode;
            entity.Grade = entity.Grade;
            entity.Designation = entity.Designation;
            entity.LeaveGroup = entity.LeaveGroup;
            entity.FullName = entity.FullName;
            #endregion
        }

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAll(CancellationToken cancellationToken)
    {
        _appDbContext.Users.RemoveRange(_appDbContext.Users);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Add(UserChildDto model, CancellationToken cancellationToken)
    {
        var entity = new UserDbEntity
        {
            AccessLevel = Convert.ToInt32(model.accesslevel),
            AccessValidityDate = string.IsNullOrEmpty(model.accessvaliditydate) ? null
                : DateTime.ParseExact(model.accessvaliditydate, "ddMMyyyy", System.Globalization.CultureInfo.InvariantCulture),
            Active = Convert.ToInt32(model.active),
            Branch = Convert.ToInt32(model.branch),
            Card1 = model.card1,
            Card2 = model.card2,
            Category = Convert.ToInt32(model.category),
            Department = Convert.ToInt32(model.department),
            Designation = Convert.ToInt32(model.designation),
            enrolled_faces = Convert.ToInt32(model.enrolled_faces),
            enrolled_fingers = Convert.ToInt32(model.enrolled_fingers),
            FullName = model.fullname,
            Grade = Convert.ToInt32(model.grade),
            Id = model.id,
            Name = model.shortname,
            ShortName = model.name,
            LeaveGroup = Convert.ToInt32(model.section),
            Organization = Convert.ToInt32(model.leave_group),
            Section = Convert.ToInt32(model.organization),
            Pin = model.pin,
            ReferenceCode = Convert.ToInt32(model.referencecode),

        };

        await _appDbContext.Users.AddAsync(entity, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(string userId, CancellationToken cancellationToken)
    {
        var entity = await _appDbContext.Users.Where(x => x.Id == userId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        _appDbContext.Users.Remove(entity);
        await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    #endregion

}
