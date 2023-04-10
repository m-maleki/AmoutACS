using App.Domain.Core.AccessControl.AppServices;
using App.Domain.Core.AccessControl.CosecApi.Dtos;
using App.Domain.Core.CosecApi.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace App.EndPoints.WebApi.Controller;

[ApiController]
[Route("[controller]")]
[EnableCors]
public class AccessControlController : ControllerBase
{
    private readonly IDeviceAppService _deviceAppService;
    private readonly IUserAppService _userAppService;

    public AccessControlController(IDeviceAppService deviceAppService,
        IUserAppService userAppService)
    {
        _deviceAppService = deviceAppService;
        _userAppService = userAppService;
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<List<DeviceOutputDto>> GetDevices(CancellationToken cancellationToken)
    {
        return await _deviceAppService.GetAll(cancellationToken);
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<List<UserOutputDto>> GetUsers(CancellationToken cancellationToken)
    {
        return await _userAppService.GetAll(cancellationToken);
    }
}