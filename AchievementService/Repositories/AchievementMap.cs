using System.Data;
using Trestle.Core.Data;
using Trestle.Core.Entities;
using AchievementService.Models;

namespace AchievementService.Repositories;

public class AchievementMap : IEntityMap
{
    public void MapData(BaseEntity obj, IDataReader rdr)
    {
        Achievement achievement = obj as Achievement;

        achievement.Id = rdr.GetInt32(0);
        achievement.Name = rdr.GetString(1);
        achievement.Description = rdr.GetString(2);
        achievement.ValueToAchieve = rdr.IsDBNull(3) ? 0 : rdr.GetInt32(3);
        achievement.ValidatorType = rdr.GetInt32(4);
        achievement.Icon = rdr.IsDBNull(5) ? "" : rdr.GetString(5);
    }
}