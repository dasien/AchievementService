using Trestle.Core.Entities;

namespace AchievementService.Models;

public class UserAchievement : BaseEntity
{
    public int UserId { get; set; }
    public int AchievementId { get; set; }
    public int CurrentValue { get; set; }
    public DateTime AchievementDate { get; set; }
    public string AchievementName { get; set; }
    public string AchievementDescription { get; set; }
    public int ValueToAchieve { get; set; }

}