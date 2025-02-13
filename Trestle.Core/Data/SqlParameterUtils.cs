
using System;
using System.Data;

namespace Trestle.Core.Data
{
	/// <summary>
	/// Helper methods for the IDbDataParameter types.
	/// </summary>
	public class SqlParameterUtils
	{
		public static IDbDataParameter CreateInputParam(IDbCommand cmd, string name, object value, DbType type)
		{
			// creates an input parameter
			return CreateParam(cmd, name, value, type, ParameterDirection.Input);
		}

		public static IDbDataParameter CreateOutputParam(IDbCommand cmd, string name, object value, DbType type)
		{
			// creates an output parameter
			return CreateParam(cmd, name, value, type, System.Data.ParameterDirection.Output);
		}

		public static IDbDataParameter CreateParam(IDbCommand cmd, string name, object value, 
			DbType type, ParameterDirection direction, int size)
		{
			// creates a parameter
			IDbDataParameter param = cmd.CreateParameter();
			param.ParameterName = name;
			param.DbType= type;
			param.Value = value;
			param.Direction = direction;
			param.Size = size;
			return param;
		}
		
		public static IDbDataParameter CreateParam(IDbCommand cmd, string name, object value, DbType type, ParameterDirection direction)
		{
			// creates a parameter
			IDbDataParameter param = cmd.CreateParameter();
			param.ParameterName = name;
			param.DbType= type;
			param.Value = value;
			param.Direction = direction;
			return param;
		}

      public static IDbDataParameter CreateParam(IDbCommand cmd, string name, object value, 
         DbType type, ParameterDirection direction, int size, byte precision, byte scale)
      {
         // creates a parameter
         IDbDataParameter param = cmd.CreateParameter();
         param.ParameterName = name;
         param.DbType= type;
         param.Value = value;
         param.Direction = direction;
         param.Size = size;
         param.Precision = precision;
         param.Scale = scale;
         return param;
      }
	}
}
