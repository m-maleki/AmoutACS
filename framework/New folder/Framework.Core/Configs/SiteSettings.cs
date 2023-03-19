using Framework.Core.Configs.AccessControl;
using Framework.Core.Configs.ConnectionStrings;
using Framework.Core.Configs.Hangfire;
using Framework.Core.Configs.Sync;

namespace Framework.Core.Configs;

public class SiteSettings
{
    public Jobs HangfireJobs { get; set; }
    public SyncConfig SyncConfig { get; set; }
    public ConnectionString ConnectionStrings { get; set; }
    public CosecApi CosecApi { get; set; }
}
