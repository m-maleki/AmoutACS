namespace App.Domain.Core.Cosec.Dtos;
public class DailyReportDto
{
    public List<DateTime> Days { get; set; }
    public List<int> Events { get; set; }
}