using Framework.Core.Markers;

namespace App.Domain.Core.CosecApi.Services;
public interface ICosecApiService : IScopedDependency
{
    Task<string> CallApi(string url, CancellationToken cancellationToken);
}
