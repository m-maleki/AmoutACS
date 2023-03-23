namespace App.Domain.Core.CosecApi.Dtos;
public class CreateDeviceDto
{
    public int DeviceId { get; set; }
    public string DeviceName { get; set; }
    public int DeviceTypeId { get; set; }
    public string MacAddress { get; set; }
}

