using System.Data;
using Trestle.Core.Data;
using Trestle.Core.Entities;
using AchievementService.Models;

namespace AchievementService.Repositories;

public class AchievementRepository
{
    public IEnumerable<IEntity> GetAchievementsForUserAction(IDbConnection con, UserActionType userActionType)
    {
        return GetAchievementsForUserAction(con, null, 30, userActionType);
    }
    
    public IEnumerable<IEntity> GetAchievementsForUserAction(IDbConnection con, IDbTransaction txn, int timeout, UserActionType userActionType)
    {
        List<Achievement> retVal = new List<Achievement>();

        using (IDbCommand cmd = DBCommandUtils.CreateCommand(con, "GetAchievementsForUserAction", CommandType.StoredProcedure, txn, timeout))
        {
            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@UserAction", userActionType, DbType.Int32);
            
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Add object to list.
                    retVal.Add((Achievement)BusinessEntityFactory.CreateObject(typeof(Achievement), new AchievementMap(), reader));
                }
            }
        }

        // Return the loaded list.
        return retVal;
    }

    public IEntity GetById(IDbConnection con, IDbTransaction txn, int timeout, int Id)
    {
        throw new NotImplementedException();
    }

        public IEntity Insert(IDbConnection con, IEntity entity)
    {
        return Insert(con, null, 30, entity);
    }

    public IEntity Insert(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity)
    {
        Achievement achievement = entity as Achievement;

        // Get new command object.
        using (IDbCommand cmd = DBCommandUtils.CreateCommand(con, "InsertAchievement", CommandType.StoredProcedure, txn, timeout))
        {
            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@Name", achievement.Name, DbType.String);
            DBCommandUtils.AddInputParam(cmd, "@Description", achievement.Description, DbType.String);
            DBCommandUtils.AddInputParam(cmd, "@ValueToAchieve", achievement.ValueToAchieve, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@ValidatorType", achievement.ValidatorType, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@Icon", achievement.Icon, DbType.String);

            // Execute query.
            int id = Convert.ToInt32(cmd.ExecuteScalar());

            // Update entity with new id.
            achievement.Id = id;

        }

        return achievement;
    }

    public void Update(IDbConnection con, IEntity entity)
    {
        Update(con, null, 30, entity);
    }

    public void Update(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity)
    {
        // Get new command object.
        using (IDbCommand cmd =
               DBCommandUtils.CreateCommand(con, "UpdateAchievement", CommandType.StoredProcedure, txn, timeout))
        {
            Achievement achievement = entity as Achievement;

            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@Id", achievement.Id, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@Name", achievement.Name, DbType.String);
            DBCommandUtils.AddInputParam(cmd, "@Description", achievement.Description, DbType.String);
            DBCommandUtils.AddInputParam(cmd, "@ValueToAchieve", achievement.ValueToAchieve, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@ValidatorType", achievement.ValidatorType, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@Icon", achievement.Icon, DbType.String);

            // Execute query.
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(IDbConnection con, IEntity entity)
    {
        Delete(con, null, 30, entity);
    }

    public void Delete(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity)
    {
        // Get new command object.
        using (IDbCommand cmd =
               DBCommandUtils.CreateCommand(con, "DeleteAchievement", CommandType.StoredProcedure, txn, timeout))
        {
            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@Id", entity.Id, DbType.Int32);

            // Execute query.
            cmd.ExecuteNonQuery();
        }
    }
}