using System;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Smart.NotificationCenter.Data.EntityFramework
{
	public interface IUnitOfWork<TContext> where TContext : DbContext
	{
		Task<TResult> ExecuteAsync<TParameter, TResult>(Func<TParameter, TResult> commands, TParameter parameter,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

		Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> commands, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

		Task<TResult> ExecuteAsync<TParameter, TResult>(Func<TParameter, Task<TResult>> commands, TParameter parameter,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

		Task<TResult> ReturnAsync<TResult>(Func<Task<TResult>> commands, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

		Task<TResult> ReturnAsync<TParameter, TResult>(Func<TParameter, Task<TResult>> commands, TParameter parameter,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
	}
}