using App.Domain.Core.CosecApi.Services;

namespace App.Domain.Services.AccessControl.CosecApi;

public class CosecApiService : ICosecApiService
{
    #region Fields

    private readonly IHttpClientFactory _clientFactory;

    #endregion

    #region Ctor
    public CosecApiService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    #endregion

    #region Implementations

    public async Task<string> CallApi(string url, CancellationToken cancellationToken)
    {
        HttpContent httpContent = new StringContent("application/json");
        var httpResult = await _clientFactory
            .CreateClient("CosecAPI").PostAsync(url, httpContent, cancellationToken);

        var result = await httpResult.Content.ReadAsStringAsync(cancellationToken);
        return result;
    }

    #endregion

}

