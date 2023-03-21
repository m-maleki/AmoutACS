using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.Cosec.Data.Repositories;
using App.Domain.Core.Cosec.Services;

namespace App.Domain.Services.AccessControl.Cosec;

public class UserService : IUserService
{
    #region Fields

    private readonly IUserRepository _userRepository;

    #endregion

    #region Ctor

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region Implementations

    public async Task<List<UserOutputDto>> GetAll(CancellationToken cancellationToken)
        => await _userRepository.GetAll(cancellationToken);

    public async Task DeleteAll(CancellationToken cancellationToken)
        => await _userRepository.DeleteAll(cancellationToken);

    public async Task<int> GetCount(CancellationToken cancellationToken)
        => await _userRepository.GetCount(cancellationToken);

    public async Task<UserOutputDto?> GetById(int id, CancellationToken cancellationToken)
        => await _userRepository.GetById(id, cancellationToken);

    public async Task Update(UserChildDto model, CancellationToken cancellationToken)
        => await _userRepository.Update(model, cancellationToken);

    public async Task Add(UserChildDto model, CancellationToken cancellationToken)
        => await _userRepository.Add(model, cancellationToken);

    public async Task Delete(int userId, CancellationToken cancellationToken)
        => await _userRepository.Delete(userId, cancellationToken);

    #endregion
}

