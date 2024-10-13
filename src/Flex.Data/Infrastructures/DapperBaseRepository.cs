using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Flex.Data.Infrastructures
{
    public abstract class DapperBaseRepository
    {
        private readonly string _connectionString;

        protected DapperBaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}
