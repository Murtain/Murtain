﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uranus.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        string Id { get; }
        UnitOfWorkOption UnitOfWorkOption { get; }


        void Begin(UnitOfWorkOption options);

 
        void Complete();
        Task CompleteAsync();
   
        void SaveChanges();
        Task SaveChangesAsync();
    }
}