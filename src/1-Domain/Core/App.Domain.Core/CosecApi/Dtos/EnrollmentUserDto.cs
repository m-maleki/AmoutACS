namespace App.Domain.Core.CosecApi.Dtos;
public class EnrollmentUserDto
{
    public int UserId  { get; set; }
    public int DeviceTypeId { get; set; }
    public int DeviceId { get; set; }
    public string EnrollmentType { get; set; }
    public int EnrollmentCount { get; set; }
}
