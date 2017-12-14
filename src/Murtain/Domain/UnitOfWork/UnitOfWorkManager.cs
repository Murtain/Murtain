using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Murtain.Domain.UnitOfWork.Configuration;
using Murtain.Domain.UnitOfWork.EventHandlers;
using Murtain.Domain.UnitOfWork.Provider;
using Murtain.Extensions;

namespace Murtain.Domain.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {

        private readonly IServiceProvider serviceProvider;
        private readonly IUnitOfWorkProvider unitOfWorkProvider;
        private readonly IUnitOfWorkConfiguration unitOfWorkConfiguration;

        public UnitOfWorkManager(IServiceProvider serviceProvider, IUnitOfWorkProvider unitOfWorkProvider, IUnitOfWorkConfiguration unitOfWorkConfiguration)
        {
            this.serviceProvider = serviceProvider;
            this.unitOfWorkProvider = unitOfWorkProvider;
            this.unitOfWorkConfiguration = unitOfWorkConfiguration;
        }

        public IUnitOfWork Current => unitOfWorkProvider.Current;

        public IUnitOfWork Begin()
        {
            return Begin(unitOfWorkConfiguration.DefaultUnitOfWorkOption);
        }

        public IUnitOfWork Begin(TransactionScopeOption scope)
        {
            return Begin(new UnitOfWorkOption
            {
                Scope = scope
            });
        }

        public IUnitOfWork Begin(UnitOfWorkOption option)
        {
            if (option?.Scope == TransactionScopeOption.Required && Current != null)
            {
                return unitOfWorkProvider.Current;
            }

            var uow = serviceProvider.GetService(typeof(IUnitOfWork)).TryAs<UnitOfWork.UnitOfWorkBase>();

            uow.Completed += Uow_Completed;
            uow.Failed += Uow_Failed;
            uow.Disposed += Uow_Disposed;

            uow.BeforeBegin(option);

            return unitOfWorkProvider.Current = uow;
        }

        private void Uow_Disposed(object sender, EventArgs e)
        {
            unitOfWorkProvider.Current = null;
        }

        private void Uow_Failed(object sender, EventArgs e)
        {
            unitOfWorkProvider.Current = null;
        }

        private void Uow_Completed(object sender, EventArgs e)
        {
            unitOfWorkProvider.Current = null;
        }
    }
}
