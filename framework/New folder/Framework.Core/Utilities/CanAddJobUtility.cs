using Framework.Core.Configs.Hangfire;

namespace Framework.Core.Utilities;

public static class CanAddJobUtility
{
    public static bool CanAddJob(int value, Jobs hangfireJobs)
    {
        string jobName = hangfireJobs.JobList.FirstOrDefault(x => x.Value == value).Key;
        if (hangfireJobs.ActiveJobs == null || !hangfireJobs.ActiveJobs.Any())
            return true;

        if (hangfireJobs != null)
            if (hangfireJobs.ActiveJobs != null)
                if (hangfireJobs.ActiveJobs.Contains(jobName))
                    return true;

        return false;
    }
    public static string GetJobName(int value, Jobs hangfireJobs)
    {
        return hangfireJobs.JobList.FirstOrDefault(x => x.Value == value).Key;
    }
}