namespace App.Domain.Core.AccessControl.CosecApi.Dtos;

public class UserOutputDto
{
    public int Id { get; set; }
    public int ReferenceCode { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string FullName { get; set; }
    public int Active { get; set; }
    public string Pin { get; set; }
    public string Card1 { get; set; }
    public string Card2 { get; set; }
    public DateTime? AccessValidityDate { get; set; }
    public int Organization { get; set; }
    public int Branch { get; set; }
    public int Department { get; set; }
    public int Designation { get; set; }
    public int Section { get; set; }
    public int Category { get; set; }
    public int Grade { get; set; }
    public int LeaveGroup { get; set; }
    public int AccessLevel { get; set; }
}
