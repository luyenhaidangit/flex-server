using System.Data;
using Flex.Core.Defines.Data;

namespace Flex.Core.Contracts.Data
{
    public interface IDataAccess : IDisposable
    {
        // Execute
        T ExecuteScalar<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        Task<T> ExecuteScalarAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        Task<int> ExecuteNonQueryAsync(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);

        // Query
        IEnumerable<T> Query<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        T QuerySingle<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        Task<T> QuerySingleAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        T QueryFirstOrDefault<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null);
    }
}