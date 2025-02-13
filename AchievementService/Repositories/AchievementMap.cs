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
        achievement.LowRangeValue = rdr.IsDBNull(4) ? 0 : rdr.GetInt32(4);
        achievement.HighRangeValue = rdr.IsDBNull(5) ? 0 : rdr.GetInt32(5);
        achievement.SingleValue = rdr.IsDBNull(6) ? 0 : rdr.GetInt32(6);
        achievement.ValidatorType = rdr.GetInt32(7);
        achievement.Icon = rdr.IsDBNull(8) ? "" : rdr.GetString(8);
    }
}