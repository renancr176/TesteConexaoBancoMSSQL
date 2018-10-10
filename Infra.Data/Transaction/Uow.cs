using System.Data;
using System.Data.SqlClient;

namespace Infra.Data.Transaction
{
    public class Uow : IUow
    {
        public IDbTransaction Transaction { get; private set; }
        public IDbConnection Connection { get; private set; }

        public Uow()
        {
            Connection = new SqlConnection(Config.ConnectionString);
            Connection.Open();
        }

        public void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
        }

        public bool Commit()
        {
            if (Transaction == null) return false;
            try
            {
                Transaction.Commit();
                return true;
            }
            catch
            {
                Transaction.Rollback();
                return false;
            }
            finally
            {
                Transaction.Dispose();
                Transaction = null;
            }
        }

        public bool Rollback()
        {
            try
            {
                if (Transaction == null) return false;
                Transaction.Rollback();
                Transaction = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IDbConnection GetConnection() => Connection;

        public IDbTransaction GetTransaction() => Transaction;

        public void Dispose()
        {
            if (Transaction != null)
            {
                Transaction.Dispose();
                Transaction = null;
            }
            if (Connection == null) return;
            Connection.Dispose();
            Connection = null;
        }
    }
}
