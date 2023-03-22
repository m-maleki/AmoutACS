using Framework.Core.Markers;

namespace App.Domain.Core.Cosec.Data.Repositories;
public interface IAvailabilityRepository : IScopedDependency
{
    Task<bool> AvailableCosecDatabase(CancellationToken cancellationToken);
    Task<bool> AvailableAcsDatabase(CancellationToken cancellationToken);
}

