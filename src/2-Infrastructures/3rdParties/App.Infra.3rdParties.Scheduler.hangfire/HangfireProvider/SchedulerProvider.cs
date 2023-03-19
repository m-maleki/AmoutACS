using Hangfire;
using System.Linq.Expressions;
using App.Domain.Core._Providers.Scheduler.Service;
using Hangfire.Storage;

namespace App.Infra._3rdParties.Scheduler.hangfire.HangfireProvider;

public class SchedulerProvider : ISchedulerProvider
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IRecurringJobManager _recurringJobManager;

    public SchedulerProvider(IBackgroundJobClient backgroundJobClient,
        IRecurringJobManager recurringJobManager)
    {
        _backgroundJobClient = backgroundJobClient;
        _recurringJobManager = recurringJobManager;
    }

    public void Enqueue(Expression<Action> action)
    {
        _backgroundJobClient.Enqueue(action);
    }

    public void Enqueue<T>(Expression<Action<T>> action)
    {
        _backgroundJobClient.Enqueue(action);
    }

    public void Delay(Expression<Action> action, TimeSpan delay)
    {
        _backgroundJobClient.Schedule(action, delay);
    }

    public void Delay<T>(Expression<Action<T>> action, TimeSpan delay)
    {
        _backgroundJobClient.Schedule(action, delay);
    }

    public void Queue(Expression<Action> action, DateTime dateTime)
    {
        var dateDif = DateTime.Now - dateTime;
        _backgroundJobClient.Schedule(action, dateDif);
    }
    public void Queue<T>(Expression<Action<T>> action, DateTime dateTime)
    {
        _backgroundJobClient.Schedule(action, dateTime);
    }

    public void AddRecurringJob<T>(string name, string queueName, string cron, Expression<Action<T>> action)
    {
        _recurringJobManager.AddOrUpdate(name.ToLower(), action, cron, TimeZoneInfo.Local, queueName.ToLower());
    }

    public void AddRecurringJob(string name, string queueName, string cron, Expression<Action> action)
    {
        _recurringJobManager.AddOrUpdate(name.ToLower(), action, cron, TimeZoneInfo.Local, queueName.ToLower());
    }

    public void RemoveAllRecurringJobs()
    {
        using var connection = JobStorage.Current.GetConnection();
        foreach (var recurringJob in connection.GetRecurringJobs()) RecurringJob.RemoveIfExists(recurringJob.Id);
    }
}