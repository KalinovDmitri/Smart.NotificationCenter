using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Smart.NotificationCenter.Data.EntityFramework
{
	public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
	{
		private readonly IDbContextAccessor _contextAccessor;

		protected DbContext Context => _contextAccessor.GetContext();

		public UnitOfWork(IDbContextAccessor accessor)
		{
			_contextAccessor = accessor;
		}
		
		public Task<TResult> ExecuteAsync<TParameter, TResult>(Func<TParameter, TResult> commands, TParameter parameter,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			return InternalExecuteAsync(commands, parameter, isolationLevel, true);
		}

		public Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> commands, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			return InternalExecuteAsync(commands, isolationLevel, true);
		}

		public Task<TResult> ExecuteAsync<TParameter, TResult>(Func<TParameter, Task<TResult>> commands, TParameter parameter,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			return InternalExecuteAsync(commands, parameter, isolationLevel, true);
		}

		public Task<TResult> ReturnAsync<TResult>(Func<Task<TResult>> commands, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			return InternalExecuteAsync(commands, isolationLevel, false);
		}

		public Task<TResult> ReturnAsync<TParameter, TResult>(Func<TParameter, Task<TResult>> commands, TParameter parameter,
			IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			return InternalExecuteAsync(commands, parameter, isolationLevel, false);
		}

		public Task<int> SaveChangesAsync()
		{
			return Context.SaveChangesAsync();
		}

		private async Task<TResult> InternalExecuteAsync<TResult>(Func<Task<TResult>> commands, IsolationLevel isolationLevel, bool withChanges)
		{
			TResult result = default(TResult);

			DbContext context = _contextAccessor.GetContext();

			DbContextTransaction transaction = null;
			try
			{
				using (transaction = CreateTransaction(context, isolationLevel, withChanges))
				{
					result = await commands.Invoke();
					if (withChanges)
					{
						await context.SaveChangesAsync();
					}
					transaction.Commit();
				}
			}
			catch
			{
				throw;
			}

			return result;
		}

		private async Task<TResult> InternalExecuteAsync<TParameter, TResult>(Func<TParameter, TResult> commands, TParameter parameter,
			IsolationLevel isolationLevel, bool withChanges)
		{
			TResult result = default(TResult);

			DbContext context = _contextAccessor.GetContext();

			DbContextTransaction transaction = null;
			try
			{
				using (transaction = CreateTransaction(context, isolationLevel, withChanges))
				{
					result = commands.Invoke(parameter);
					if (withChanges)
					{
						await context.SaveChangesAsync();
					}
					transaction.Commit();
				}
			}
			catch
			{
				throw;
			}

			return result;
		}

		private async Task InternalExecuteAsync<TParameter, TResult>(Func<TParameter, Task> commands, TParameter parameter,
			IsolationLevel isolationLevel, bool withChanges)
		{
			DbContext context = _contextAccessor.GetContext();

			DbContextTransaction transaction = null;
			try
			{
				using (transaction = CreateTransaction(context, isolationLevel, withChanges))
				{
					await commands.Invoke(parameter);
					if (withChanges)
					{
						await context.SaveChangesAsync();
					}
					transaction.Commit();
				}
			}
			catch
			{
				throw;
			}
		}

		private async Task<TResult> InternalExecuteAsync<TParameter, TResult>(Func<TParameter, Task<TResult>> commands, TParameter parameter,
			IsolationLevel isolationLevel, bool withChanges)
		{
			TResult result = default(TResult);

			DbContext context = _contextAccessor.GetContext();

			DbContextTransaction transaction = null;
			try
			{
				using (transaction = CreateTransaction(context, isolationLevel, withChanges))
				{
					result = await commands.Invoke(parameter);
					if (withChanges)
					{
						await context.SaveChangesAsync();
					}
					transaction.Commit();
				}
			}
			catch
			{
				throw;
			}

			return result;
		}

		private DbContextTransaction CreateTransaction(DbContext context, IsolationLevel isolationLevel, bool withChanges)
		{
			if (context.Database.CurrentTransaction != null)
			{
				throw new InvalidOperationException("Database context has already been initialized.");
			}

			context.Configuration.AutoDetectChangesEnabled = withChanges;

			return context.Database.BeginTransaction(isolationLevel);
		}
	}
}