using AchievementService.Models;

namespace AchievementService.Validators;

public interface IAchievementValidator
{
    public bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction);
}