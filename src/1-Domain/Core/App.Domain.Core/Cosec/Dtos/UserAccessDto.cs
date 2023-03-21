namespace App.Domain.Core.Cosec.Dtos;
public class UserAccessDto
{
    public string UserId { get; set; }
    public DateTime EventDT { get; set; }
    public decimal Action { get; set; }
    public decimal MID { get; set; }
    public decimal DType { get; set; }
    public string SystemUserId { get; set; }
    public decimal AppSource { get; set; }
}
