using System.Data;
using Trestle.Core.Data;
using Trestle.Core.Entities;
using AchievementService.Models;

namespace AchievementService.Repositories;

public class UserAchievementRepository
{
    public IEnumerable<IEntity> GetByUserId(IDbConnection con, int userId)
    {
        return GetByUserId(con, null, 30, userId);
    }

    public IEnumerable<IEntity> GetByUserId(IDbConnection con, IDbTransaction txn, int timeout, int userId)
    {
        List<UserAchievement> retVal = new List<UserAchievement>();

        using (IDbCommand cmd = DBCommandUtils.CreateCommand(con, "GetAchievementsByUserId", CommandType.StoredProcedure, txn, timeout))
        {
            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@UserId", userId, DbType.Int32);
            
            using (IDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Add object to list.
                    retVal.Add((UserAchievement)BusinessEntityFactory.CreateObject(typeof(UserAchievement), new UserAchievementMap(), reader));
                }
            }
        }

        // Return the loaded list.
        return retVal;
    }

    public IEntity Insert(IDbConnection con, IEntity entity)
    {
        return Insert(con, null, 30, entity);
    }

    public IEntity Insert(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity)
    {
        UserAchievement userAchievement = entity as UserAchievement;

        // Get new command object.
        using (IDbCommand cmd = DBCommandUtils.CreateCommand(con, "InsertUserAchievement", CommandType.StoredProcedure, txn, timeout))
        {
            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@UserId", userAchievement.UserId, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@AchievementId", userAchievement.AchievementId, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@CurrentValue", userAchievement.CurrentValue, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@AchievementDate", userAchievement.AchievementDate, DbType.DateTime);
            
            // Execute query.
            cmd.ExecuteNonQuery();
        }

        return userAchievement;
    }

    public void Update(IDbConnection con, IEntity entity)
    {
        Update(con, null, 30, entity);
    }

    public void Update(IDbConnection con, IDbTransaction txn, int timeout, IEntity entity)
    {
        // Get new command object.
        using (IDbCommand cmd =
               DBCommandUtils.CreateCommand(con, "UpdateUserAchievement", CommandType.StoredProcedure, txn, timeout))
        {
            UserAchievement userAchievement = entity as UserAchievement;

            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@UserId", userAchievement.UserId, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@AchievementId", userAchievement.AchievementId, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@CurrentValue", userAchievement.CurrentValue, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@AchievementDate", userAchievement.AchievementDate, DbType.DateTime);

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
        using (IDbCommand cmd = DBCommandUtils.CreateCommand(con, "DeleteUserAchievement", CommandType.StoredProcedure, txn, timeout))
        {
            UserAchievement userAchievement = entity as UserAchievement;
            
            // Set parameters.
            DBCommandUtils.AddInputParam(cmd, "@UserId", userAchievement.UserId, DbType.Int32);
            DBCommandUtils.AddInputParam(cmd, "@AchievementId", userAchievement.AchievementId, DbType.Int32);

            // Execute query.
            cmd.ExecuteNonQuery();
        }
    }
}