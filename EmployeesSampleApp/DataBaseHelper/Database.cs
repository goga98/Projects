using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataBaseHelper
{
    public class Database
    {
        #region Private Members

        #endregion

        #region Private Methods

        #endregion

        #region Constructors

        public Database()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[this.GetType().Name].ConnectionString;
        }

        public Database(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #endregion

        #region Events

        public event Action<string, Exception> OnError;

        #endregion

        #region Public Properties

        public string ConnectionString { get; private set; }

        #endregion

        #region Public Methods

        public SqlConnection GetConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            return connection;
        }

        public SqlConnection GetConnection()
        {
            return GetConnection(ConnectionString);
        }

        public SqlCommand GetCommand(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = new SqlCommand()
            {
                Connection = GetConnection(connectionString),
                CommandText = commandText,
                CommandType = commandType
            };
            command.Parameters.AddRange(parameters);

            return command;
        }

        public SqlCommand GetCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return GetCommand(ConnectionString, commandText, commandType, parameters);
        }

        public SqlCommand GetCommand(string commandText, params SqlParameter[] parameters)
        {
            return GetCommand(ConnectionString, commandText, CommandType.Text, parameters);
        }

        public SqlDataReader ExecuteReader(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = GetCommand(connectionString, commandText, commandType, parameters);
            command.Connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecuteReader(ConnectionString, commandText, commandType, parameters);
        }

        public SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
        {
            return ExecuteReader(ConnectionString, commandText, CommandType.Text, parameters);
        }

        public int ExecuteNonQuery(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = GetCommand(connectionString, commandText, commandType, parameters);
            int result;
            try
            {
                command.Connection.Open();
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                OnError?.Invoke("ExecuteNonQuery", ex);
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(ConnectionString, commandText, commandType, parameters);
        }

        public int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            return ExecuteNonQuery(ConnectionString, commandText, CommandType.Text, parameters);
        }

        public object ExecuteScalar(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = GetCommand(connectionString, commandText, commandType, parameters);
            object result;
            try
            {
                command.Connection.Open();
                result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                OnError?.Invoke("ExecuteScalar", ex);
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
            return result;
        }

        public object ExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return ExecuteScalar(ConnectionString, commandText, commandType, parameters);
        }

        public object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            return ExecuteScalar(ConnectionString, commandText, CommandType.Text, parameters);
        }

        public DataTable GetTable(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = GetCommand(connectionString, commandText, commandType, parameters);
            DataTable table = new DataTable();
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }

        public DataTable GetTable(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return GetTable(ConnectionString, commandText, commandType, parameters);
        }

        public DataTable GetTable(string commandText, params SqlParameter[] parameters)
        {
            return GetTable(ConnectionString, commandText, CommandType.Text, parameters);
        }

        #endregion

        public SqlDataAdapter DataAdapter(string connectionString, string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = GetCommand(connectionString, commandText, commandType, parameters);
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                //DataSet dataSet = new DataSet();
                //adapter.Fill(dataSet);
                command.Connection.Open();
                
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                command.Connection.Close();
            }
            return adapter;
        }

        public SqlDataAdapter DataAdapter(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            return DataAdapter(ConnectionString, commandText, commandType, parameters);
        }
        public SqlDataAdapter DataAdapter(string commandText, params SqlParameter[] parameters)
        {
            return DataAdapter(ConnectionString, commandText, CommandType.Text, parameters);
        }
    }
}
