using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Murtain.Domain.UnitOfWork.Provider
{
    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        private const string CONTEXT_KEY = "Murtain_UNITOFWORK_CURRENT";
        private static readonly ConcurrentDictionary<string, IUnitOfWork> uowDisctionary = new ConcurrentDictionary<string, IUnitOfWork>();

        private static AsyncLocal<string> AsyncLocalUnitOfWork = new AsyncLocal<string>();
        public static AsyncLocal<string> asyncLocalUnitOfWork
        {
            get { return asyncLocalUnitOfWork; }
            private set { asyncLocalUnitOfWork = value; }
        }

        public UnitOfWorkProvider()
        {

        }
        public IUnitOfWork Current { get => GetCurrentUnitOfWork(); set => SetCurrentUnitOfWork(value); }


        private IUnitOfWork GetCurrentUnitOfWork()
        {
            if (AsyncLocalUnitOfWork.Value == null)
            {
                return null;
            }

            IUnitOfWork unitOfWork;
            if (!uowDisctionary.TryGetValue(AsyncLocalUnitOfWork.Value, out unitOfWork))
            {
                asyncLocalUnitOfWork.Value = null;
                return null;
            }

            if (unitOfWork.IsDisposed)
            {
                uowDisctionary.TryRemove(AsyncLocalUnitOfWork.Value, out unitOfWork);
                asyncLocalUnitOfWork.Value = null;
                return null;
            }

            return unitOfWork;

        }

        private static void SetCurrentUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                ExitFromCurrentUintOfWorkScope();
                return;
            }

            if (AsyncLocalUnitOfWork.Value != null)
            {
                IUnitOfWork outer;
                if (uowDisctionary.TryGetValue(AsyncLocalUnitOfWork.Value, out outer))
                {
                    if (outer == unitOfWork)
                    {
                        return;
                    }
                    unitOfWork.Outer = outer;
                }
            }
            var unitOfWorkKey = unitOfWork.Id;
            if (!uowDisctionary.TryAdd(unitOfWorkKey, unitOfWork))
            {
                throw new Exception("Can not set current unit of work");
            }

            AsyncLocalUnitOfWork.Value = unitOfWork.Id;
        }

        private static void ExitFromCurrentUintOfWorkScope()
        {
            if (AsyncLocalUnitOfWork.Value == null)
            {
                return;
            }

            IUnitOfWork unitOfWork;
            if (!uowDisctionary.TryGetValue(AsyncLocalUnitOfWork.Value, out unitOfWork))
            {
                AsyncLocalUnitOfWork.Value = null;
                return;
            }

            uowDisctionary.TryRemove(AsyncLocalUnitOfWork.Value, out unitOfWork);
            if (unitOfWork.Outer == null)
            {
                AsyncLocalUnitOfWork.Value = null;
                return;
            }

            var outerUnitOfWorkKey = unitOfWork.Outer.Id;
            if (!uowDisctionary.TryGetValue(outerUnitOfWorkKey, out unitOfWork))
            {
                AsyncLocalUnitOfWork.Value = null;
                return;
            }

            AsyncLocalUnitOfWork.Value = outerUnitOfWorkKey;
        }
    }
}
