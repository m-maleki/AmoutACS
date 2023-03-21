using Newtonsoft.Json;

namespace App.Domain.Core.AccessControl.CosecApi.Dtos;

public class DeviceDto
{
    [JsonProperty("device")]
    public List<DeviceChildDto> Devices { get; set; }
}
public class DeviceChildDto
{
    [JsonProperty("device-id")]
    public int DeviceId { get; set; }
    [JsonProperty("device-type")]
    public string DeviceType { get; set; }
    public string Name { get; set; }
    public int DId { get; set; }
    public int Active { get; set; }
    public string Ip { get; set; }
    public string Mac { get; set; }
    public string Status { get; set; }

    [JsonProperty("connect-time")]
    public string? ConnectTime { get; set; }

    [JsonProperty("disconnect-time")]
    public string? DisconnectTime { get; set; }
    public bool UserAccess { get; set; }
}
