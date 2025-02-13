using System.Data;
using Trestle.Core.Data;
using Trestle.Core.Entities;
using AchievementService.Models;

namespace AchievementService.Repositories;

public class UserAchievementMap : IEntityMap
{
    public void MapData(BaseEntity obj, IDataReader rdr)
    {
        UserAchievement userAchievement = obj as UserAchievement;

        userAchievement.UserId = rdr.GetInt32(0);
        userAchievement.AchievementId = rdr.GetInt32(1);
        userAchievement.CurrentValue = rdr.IsDBNull(2) ? 0 : rdr.GetInt32(2);
        userAchievement.AchievementDate = rdr.IsDBNull(3) ? DateTime.MinValue : rdr.GetDateTime(3);
        userAchievement.AchievementName = rdr.IsDBNull(4) ? "" : rdr.GetString(4);
        userAchievement.AchievementDescription = rdr.IsDBNull(5) ? "" : rdr.GetString(5);
        userAchievement.ValueToAchieve = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6);
    }
}