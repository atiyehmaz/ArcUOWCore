using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
        bool Successful { get; set; }

    }
}
