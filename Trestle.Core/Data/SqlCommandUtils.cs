
using System.Text;
using System.Data;

namespace Trestle.Core.Data
{
	/// <summary>
	/// Builder class for IDbCommand
	/// </summary>
	public class SqlCommandUtils
	{
        public static bool AppendCriteria(StringBuilder sql, string columnName, bool appendAnd, string criteria)
        {
            // Set the return value to the current value of the 'appendAnd' variable.
            bool retVal = appendAnd;

            // Check to see if the criteria is null.
            if (!string.IsNullOrEmpty(criteria))
            {
                // Check to see if previous criteria were entered.
                if (appendAnd)
                {
                    // Append and.
                    sql.Append("AND ");
                }

                // Append name search.
                sql.Append(columnName + " = '")
                    .Append(criteria.Replace("'", "''")) // convert for safe-sql
                    .Append("' ");

                // Set 'and' flag.
                retVal = true;
            }

            //Return value indicating if the next attempt should prepend 'and' to the where clause.
            return retVal;            
        }

        public static bool AppendCriteria(StringBuilder sql, string columnName, bool appendAnd, int criteria)
        {
            // Set the return value to the current value of the 'appendAnd' variable.
            bool retVal = appendAnd;

            // Check to see if the criteria is null.
            if (criteria != int.MinValue)
            {
                // Check to see if previous criteria were entered.
                if (appendAnd)
                {
                    // Append and.
                    sql.Append("AND ");
                }

                // Append name search.
                sql.Append(columnName + " = ")
                    .Append(criteria)
                    .Append(" ");

                // Set 'and' flag.
                retVal = true;
            }

            //Return value indicating if the next attempt should prepend 'and' to the where clause.
            return retVal;
        }

        public static bool AppendCriteria(StringBuilder sql, string columnName, bool appendAnd, DateTime criteria)
        {
            // Set the return value to the current value of the 'appendAnd' variable.
            bool retVal = appendAnd;

            // Check to see if the criteria is null.
            if (criteria != DateTime.MinValue)
            {
                // Check to see if previous criteria were entered.
                if (appendAnd)
                {
                    // Append and.
                    sql.Append("AND ");
                }

                // Append name search.
                sql.Append(columnName + " = '")
                    .Append(criteria.ToString())
                    .Append("' ");

                // Set 'and' flag.
                retVal = true;
            }

            //Return value indicating if the next attempt should prepend 'and' to the where clause.
            return retVal;
        }
        
        public static IDbCommand CreateCommand(IDbConnection cn, string commandText, CommandType commandType)
		{
			IDbCommand cmd = cn.CreateCommand();
			cmd.CommandText = commandText;
			cmd.CommandType = commandType;
			return cmd;
		}

        public static IDbCommand CreateCommand(IDbConnection cn, string commandText, CommandType commandType, IDbTransaction? txn, int timeout)
        {
            IDbCommand cmd = cn.CreateCommand();

            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            cmd.Transaction = txn;
            cmd.CommandTimeout = timeout;

            return cmd;
        }

        public static IDbDataParameter AddInputParam(IDbCommand cmd, string name, object value, DbType type)
        {
         return AddParam(cmd, name, value, type, ParameterDirection.Input);
        }

        public static IDbDataParameter AddInputParam(IDbCommand cmd, string name, object value, DbType type, int size, byte precision, byte scale)
        {
         return AddParam(cmd, name, value, type, ParameterDirection.Input, size, precision, scale);
        }

		public static IDbDataParameter AddOutputParam(IDbCommand cmd, string name, object value, DbType type)
		{
			return AddParam(cmd, name, value, type, ParameterDirection.Output);
		}

        public static IDbDataParameter AddOutputParam(IDbCommand cmd, string name, object value, DbType type, int size, byte precision, byte scale)
        {
         return AddParam(cmd, name, value, type, ParameterDirection.Output, size, precision, scale);
        }

		public static IDbDataParameter AddParam(IDbCommand cmd, string name, object value, DbType type, ParameterDirection direction)
		{
			IDbDataParameter param = SqlParameterUtils.CreateParam(cmd, name, value, type, direction);
			AddParam(cmd, param);
			return param;
		}

		public static IDbDataParameter AddParam(IDbCommand cmd, string name, object value, DbType type, ParameterDirection direction, int size)
		{
			IDbDataParameter param = SqlParameterUtils.CreateParam(cmd, name, value, type, direction, size);
			AddParam(cmd, param);
			return param;
		}

		public static void AddParam(IDbCommand cmd, IDbDataParameter param)
		{
			try
			{
				cmd.Parameters.Add(param);
			}
			catch (System.Exception e)
			{
				string msg = String.Format("Error adding parameter to command; dsn={0}",
					cmd.Connection.ConnectionString);
				throw new Exception(msg, e);
			}
		}

      public static IDbDataParameter AddParam(IDbCommand cmd, string name, object value, DbType type, ParameterDirection direction, int size, byte precision, byte scale)
      {
         IDbDataParameter param = SqlParameterUtils.CreateParam(cmd, name, value, type, direction, size, precision, scale);
         AddParam(cmd, param);
         return param;
      }

      private static string GetParmValue(IDbDataParameter param)
      {
         if (param.Value is string)
         {
            string str = param.Value as string;
            
            // fix embedded single quotes
            str = str.Replace("'", "''");

            return "'" + str + "'";
         }
         else if (param.Value is DateTime)
         {
            return "'" + param.Value + "'";
         }
         else
         {
            return param.Value.ToString();
         }
      }
   }
}