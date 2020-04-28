using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteDBUI.Library.DataAccess
{
	public class SqlDataAccess
	{
		private bool isClosed = false;

		public string GetConnectionString(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				List<T> rows = connection.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();
				return rows;
			}
		}

		public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		public int SaveImportedData<T>(string storedProcedure, T parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using(IDbConnection connection = new SqlConnection(connectionString))
			{
				int output = connection.Query<int>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).Single();
				return output;
			}
		}

		public void DeleteData<T>(string storedProcedure, T parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using(IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		public void UpdateData<T>(string storedProcedure, T parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);

			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
			}
		}

		private IDbConnection _connection;
		private IDbTransaction _transaction;

		public void StartTransaction(string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);
			_connection = new SqlConnection(connectionString);
			_connection.Open();
			_transaction = _connection.BeginTransaction();
			isClosed = false;
		}

		public void SaveDataInTransAction<T>(string storedProcedure, T parameters)
		{
			_connection.Execute(storedProcedure,
				parameters,
				commandType: CommandType.StoredProcedure,
				transaction: _transaction);
		}

		public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
		{
			List<T> rows = _connection.Query<T>(storedProcedure,
				parameters,
				commandType: CommandType.StoredProcedure,
				transaction: _transaction)
				.ToList();
			return rows;
		}

		public void CommitTransAction()
		{
			_transaction?.Commit();
			_connection?.Close();
			isClosed = true;
		}

		public void RollbackTransAction()
		{
			_transaction?.Rollback();
			_connection?.Close();
			isClosed = true;
		}

		public void Dispose()
		{
			if (isClosed == false)
			{
				try
				{
					CommitTransAction();
				}
				catch
				{
					// TODO:  Log this issue
				}
			}

			_connection = null;
			_transaction = null;
		}
	}
}
