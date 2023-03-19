namespace Framework.Core.Configs.Hangfire;

public class Jobs
{
    public Dictionary<string, int> JobList { get; set; }
    public List<string> ActiveJobs { get; set; }
}
