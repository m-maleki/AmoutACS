using System.Linq.Expressions;

namespace App.Domain.Core._Providers.Scheduler.Service;

public interface ISchedulerProvider
{
    void Enqueue(Expression<Action> action);
    void Enqueue<T>(Expression<Action<T>> methodCall);
    void Delay(Expression<Action> action, TimeSpan timeSpan);
    void Delay<T>(Expression<Action<T>> action, TimeSpan delay);
    void Queue(Expression<Action> action, DateTime dateTime);
    void Queue<T>(Expression<Action<T>> action, DateTime dateTime);
    void AddRecurringJob<T>(string name, string queueName, string cron, Expression<Action<T>> action);
    void AddRecurringJob(string name, string queueName, string cron, Expression<Action> action);
    void RemoveAllRecurringJobs();
}

