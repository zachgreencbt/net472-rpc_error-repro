using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;

namespace IssueRepro
{
    public class Interceptor : IDbCommandInterceptor
        {

            public void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
            {
            }

            public void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
            {
                Log(command, interceptionContext);
            }

            public void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
            {
            }

            public void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
            {
                Log(command, interceptionContext);
            }

            public void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
            {
            }

            public void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
            {
                Log(command, interceptionContext);
            }

            private static void Log<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
            {
                 Debug.WriteLine(string.Format(
                    "Context '{0}' is executing command '{1}'{2}",
                    interceptionContext.GetType().Name,
                    command.CommandText.Replace(Environment.NewLine, ""),
                    Environment.NewLine));
            }

        }
}
