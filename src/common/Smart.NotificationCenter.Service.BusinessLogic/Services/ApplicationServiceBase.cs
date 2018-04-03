using System;
using System.Threading.Tasks;

using Smart.NotificationCenter.Data.Abstractions;
using Smart.NotificationCenter.Data.EntityFramework;

namespace Smart.NotificationCenter.Service.BusinessLogic
{
	public abstract class ApplicationServiceBase
	{
		protected readonly IUnitOfWork _unitOfWork;

		protected internal ApplicationServiceBase(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}