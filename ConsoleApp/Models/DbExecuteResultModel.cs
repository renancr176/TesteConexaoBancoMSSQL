using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    public class DbExecuteResultModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }

        public DbExecuteResultModel()
        {
        }

        public DbExecuteResultModel(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
