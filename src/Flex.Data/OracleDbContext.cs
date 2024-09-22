using Dapper;
using Dapper.Oracle;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Flex.Core.Contracts.Data;
using Flex.Core.Defines.Data;

namespace Flex.Data
{
    public class OracleDbContext : IDataAccess
    {
        private readonly string _connectionString;
        private OracleConnection _connection;
        private OracleTransaction? _transaction;

        public OracleDbContext(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new OracleConnection(_connectionString);
            _transaction = null;
        }

        #region Common
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        private OracleMappingType MapDatabaseTypeToOracleMappingType(string type)
        {
            switch (type)
            {
                case DatabaseType.Int32:
                    return OracleMappingType.Int32;
                case DatabaseType.Int64:
                    return OracleMappingType.Int64;
                case DatabaseType.Decimal:
                    return OracleMappingType.Decimal;
                case DatabaseType.Varchar2:
                    return OracleMappingType.Varchar2;
                case DatabaseType.NVarchar2:
                    return OracleMappingType.NVarchar2;
                case DatabaseType.Date:
                    return OracleMappingType.Date;
                case DatabaseType.Blob:
                    return OracleMappingType.Blob;
                case DatabaseType.Clob:
                    return OracleMappingType.Clob;
                case DatabaseType.Byte:
                    return OracleMappingType.Byte;
                case DatabaseType.RefCursor:
                    return OracleMappingType.RefCursor;
                default:
                    throw new NotSupportedException($"Kiểu dữ liệu '{type}' không được hỗ trợ.");
            }
        }

        private OracleDbType MapDatabaseTypeToOracleDbType(string type)
        {
            switch (type)
            {
                case DatabaseType.Int32:
                    return OracleDbType.Int32;
                case DatabaseType.Int64:
                    return OracleDbType.Int64;
                case DatabaseType.Decimal:
                    return OracleDbType.Decimal;
                case DatabaseType.Varchar2:
                    return OracleDbType.Varchar2;
                case DatabaseType.NVarchar2:
                    return OracleDbType.NVarchar2;
                case DatabaseType.Date:
                    return OracleDbType.Date;
                case DatabaseType.Blob:
                    return OracleDbType.Blob;
                case DatabaseType.Clob:
                    return OracleDbType.Clob;
                case DatabaseType.Byte:
                    return OracleDbType.Byte;
                case DatabaseType.RefCursor:
                    return OracleDbType.RefCursor;
                default:
                    throw new NotSupportedException($"Kiểu dữ liệu '{type}' không được hỗ trợ.");
            }
        }
        #endregion

        #region Execute
        public T ExecuteScalar<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.ExecuteScalar<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public async Task<T> ExecuteScalarAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = await _connection.ExecuteScalarAsync<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public int ExecuteNonQuery(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.Execute(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public async Task<int> ExecuteNonQueryAsync(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = await _connection.ExecuteAsync(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }
        #endregion

        #region Query
        public IEnumerable<T> Query<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.Query<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.QueryAsync<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public T QuerySingle<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.QuerySingle<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public Task<T> QuerySingleAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.QuerySingleAsync<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public T QueryFirstOrDefault<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.QueryFirstOrDefault<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var oracleParams = new OracleDynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    oracleParams.Add(
                        name: param.Name,
                        value: param.Value,
                        dbType: this.MapDatabaseTypeToOracleMappingType(param.Type),
                        direction: param.Direction,
                        size: param.Size
                    );
                }
            }

            var result = _connection.QueryFirstOrDefaultAsync<T>(
                sql: query,
                param: oracleParams,
                commandType: commandType
            );

            this.CloseConnection();

            return result;
        }

        public DataSet ExecuteDataSet(string query, CommandType commandType = CommandType.Text, DatabaseParameter[] parameters = null)
        {
            this.OpenConnection();

            var dataSet = new DataSet();

            using (var command = new OracleCommand(query, _connection))
            {
                command.CommandType = commandType;
                command.BindByName = true;

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        var oracleParam = new OracleParameter
                        {
                            ParameterName = param.Name,
                            Value = param.Value ?? DBNull.Value,
                            Direction = param.Direction,
                            OracleDbType = this.MapDatabaseTypeToOracleDbType(param.Type)
                        };

                        if (param.Size.HasValue)
                        {
                            oracleParam.Size = param.Size.Value;
                        }

                        command.Parameters.Add(oracleParam);
                    }
                }

                using (var adapter = new OracleDataAdapter(command))
                {
                    adapter.Fill(dataSet);
                }
            }

            this.CloseConnection();

            return dataSet;
        }
        #endregion
    }
}