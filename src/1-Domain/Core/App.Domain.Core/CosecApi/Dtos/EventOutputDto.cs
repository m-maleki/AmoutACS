namespace App.Domain.Core.CosecApi.Dtos;
public class EventOutputDto
{
    public int Id { get; set; }
    public int IndexNo { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public DateTime EventDateTime { get; set; }
    public string EntryExitType { get; set; }
    public int MasterControllerId { get; set; }
    public int DoorControllerId { get; set; }
    public int SpecialFunctionId { get; set; }
    public DateTime? Leavedt { get; set; }
    public DateTime? IDateTime { get; set; }
    public string DeviceName { get; set; }
}
