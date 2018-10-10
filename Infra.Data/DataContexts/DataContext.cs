﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace Infra.Data.DataContexts
{
    public class DataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }
       

        public DataContext()
        {
            Connection = new SqlConnection(Config.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
