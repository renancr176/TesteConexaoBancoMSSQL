using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp.Data
{
    public class DataContext : IDisposable
    {
        private static string ConnectionString = "Data Source=DESKTOP-E1JHOO7\\SQLEXPRESS;Initial Catalog=SysDb;Persist Security Info=True;User ID=SysUser;Password=SysUserPwd;MultipleActiveResultSets=True";
        public SqlConnection Connection { get; private set; }

        public DataContext()
        {
            Connection = new SqlConnection(ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
