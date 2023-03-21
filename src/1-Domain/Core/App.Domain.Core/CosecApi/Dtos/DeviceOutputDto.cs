namespace App.Domain.Core.CosecApi.Dtos;
public class DeviceOutputDto
{
    public int DeviceId { get; set; }
    public int DeviceType { get; set; }
    public string Name { get; set; }
    public int DId { get; set; }
    public int Active { get; set; }
    public string Ip { get; set; }
    public string Mac { get; set; }
    public string Status { get; set; }
    public DateTime? ConnectTime { get; set; }
    public DateTime? DisconnectTime { get; set; }
    public bool UserAccess { get; set; }
}
