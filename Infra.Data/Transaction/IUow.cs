using System;
using System.Data;

namespace Infra.Data.Transaction
{
    public interface IUow : IDisposable
    {
        void BeginTransaction();
        bool Commit();
        bool Rollback();

        IDbConnection GetConnection();
        IDbTransaction GetTransaction();
    }
}
