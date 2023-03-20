using Newtonsoft.Json;

namespace App.Domain.Core.AccessControl.CosecApi.Dtos;

public class DeviceDto
{
    [JsonProperty("device")]
    public List<DeviceChildDto> Devices { get; set; }
}
public class DeviceChildDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SiteId { get; set; }
    public string Type { get; set; }

    [JsonProperty("device-type")]
    public string DeviceType { get; set; }

    [JsonProperty("application-type")]
    public string ApplicationType { get; set; }

    [JsonProperty("door-id")]
    public string DoorId { get; set; }

    [JsonProperty("finger-template-type")]
    public string FingerTemplateType { get; set; }
    public string Ip { get; set; }

    [JsonProperty("rs-485")]
    public string Rs485 { get; set; }
    public string Mac { get; set; }
}
