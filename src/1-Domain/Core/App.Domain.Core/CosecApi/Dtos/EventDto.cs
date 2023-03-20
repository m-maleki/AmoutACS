using Newtonsoft.Json;

namespace App.Domain.Core.AccessControl.CosecApi.Dtos;

public class EventDto
{
    [JsonProperty("event-ta")]
    public List<EventChildDto> Events { get; set; }
}
 public class EventChildDto
{
    public string indexno { get; set; }
    public string userid { get; set; }
    public string username { get; set; }
    public string eventdatetime { get; set; }
    public string entryexittype { get; set; }
    public string mastercontrollerid { get; set; }
    public string doorcontrollerid { get; set; }
    public string specialfunctionid { get; set; }
    public string leavedt { get; set; }
    public string idatetime { get; set; }
}