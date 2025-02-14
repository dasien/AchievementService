using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class ThresholdValidator : AchievementValidator
{
    public ThresholdValidator(string dsn) : base(dsn)
    {
        
    }
    
    public override bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction)
    {
        // Temp flag to indicate if the user met achievement.
        bool achievementMet = false;
        
        // Check to see if the user has this achievement already.
        if (userAchievement != null)
        {
            // Check to see if the user has already met the achievement.
            if (userAchievement.CurrentValue < achievement.ValueToAchieve)
            {
                // See if the user will meet the achievement this time.
                if ((userAchievement.CurrentValue + 1) == achievement.ValueToAchieve)
                {
                    // Flag that the user has met an achievement.
                    achievementMet = true;
                }
                
                // Update the user achievement.
                userAchievement.CurrentValue++;
                userAchievement.AchievementDate = DateTime.Now;
                
                // Update user achievement.
                UpdateUserAchievement(userAchievement);
            }
        }

        else
        {
            // Check to see if this achievement is met on first action.
            if (achievement.ValueToAchieve == 1)
            {
                // Flag that achievement is met.
                achievementMet = true;
            }

            // Create new achievement.
            CreateUserAchievement(1, DateTime.Now, userAction.UserId, achievement.Id);
        }
        
        // Return flag to indicate achievement reached.
        return achievementMet;
    }
}