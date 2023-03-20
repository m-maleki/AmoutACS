using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.AccessControl.CosecApi.Dtos;
public class UserDto
{
    [JsonProperty("user")]
    public List<UserChildDto> Users { get; set; }
}

public class UserChildDto
{
    public string id { get; set; }
    [JsonProperty("reference-code")]
    public string referencecode { get; set; }
    public string name { get; set; }
    [JsonProperty("short-name")]
    public string shortname { get; set; }
    [JsonProperty("full-name")]
    public string fullname { get; set; }
    public string active { get; set; }
    public string pin { get; set; }
    [JsonProperty("card-1")]
    public string card1 { get; set; }
    [JsonProperty("card-2")]
    public string card2 { get; set; }
    [JsonProperty("access-validity-date")]
    public string? accessvaliditydate { get; set; }
    public string organization { get; set; }
    public string branch { get; set; }
    public string department { get; set; }
    public string designation { get; set; }
    public string section { get; set; }
    public string category { get; set; }
    public string grade { get; set; }
    public string leave_group { get; set; }
    [JsonProperty("access-level")]
    public string accesslevel { get; set; }
    public string enrolled_fingers { get; set; }
    public string enrolled_faces { get; set; }
}