using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class DayOfWeekValidator : AchievementValidator
{
    public DayOfWeekValidator(string dsn) : base(dsn)
    {
        
    }
    public override bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction)
    {
        // Temp flag to indicate if the user met achievement.
        bool achievementMet = false;
        
        // Check to see if the user has this achievement already.
        if (userAchievement == null)
        {
            // Get the day of week from the action.
            int dayOfWeek = (int)userAction.ActionDate.DayOfWeek;
            
            // Check to see if it matches the validator day.
            if (dayOfWeek == achievement.SingleValue)
            {
                achievementMet = true;
                
                // Create new achievement.
                CreateUserAchievement(1, DateTime.Now, userAction.UserId, achievement.Id);
            }
        }
        
        return achievementMet;
    }
}