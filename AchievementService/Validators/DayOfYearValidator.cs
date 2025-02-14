using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class DayOfYearValidator : AchievementValidator
{
    public DayOfYearValidator(string dsn) : base(dsn)
    {
        
    }

    public override bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction)
    {
        // Temp flag to indicate if the user met achievement.
        bool achievementMet = false;
        
        // Check to see if the user has this achievement already.
        if (userAchievement == null)
        {
            // Check to see if the julian dates match.
            // TODO: Need to account for leap years with ActionDate.IsLeapYear
            if (achievement.SingleValue == userAction.ActionDate.DayOfYear)
            {
                achievementMet = true;
                
                // Create new achievement.
                CreateUserAchievement(1, DateTime.Now, userAction.UserId, achievement.Id);
            }
        }
        
        return achievementMet;
    }
}