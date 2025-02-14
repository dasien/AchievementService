using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class TimeOfDayRangeValidator : AchievementValidator
{
    public TimeOfDayRangeValidator(string dsn) : base(dsn)
    {
        
    }

    public override bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction)
    {
        // Temp flag to indicate if the user met achievement.
        bool achievementMet = false;
        
        // Check to see if the user has this achievement already.
        if (userAchievement == null)
        {
            // Check to see if the hour part of the date time is between the required values.
            if (achievement.LowRangeValue <= userAction.ActionDate.Hour && userAction.ActionDate.Hour <= achievement.HighRangeValue)
            {
                // Flag that the achievement has been reached.
                achievementMet = true;
                
                // Create new achievement.
                CreateUserAchievement(1, DateTime.Now, userAction.UserId, achievement.Id);
            }
        }
        
        // Return the flag.
        return achievementMet;
    }
}