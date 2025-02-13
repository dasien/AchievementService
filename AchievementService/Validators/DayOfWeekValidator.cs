using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class DayOfWeekValidator : IAchievementValidator
{
    private string _dsn;
    
    public DayOfWeekValidator(string dsn)
    {
        _dsn = dsn;
    }
    public bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction)
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
                
                // Create new achievement for this user.
                userAchievement = new UserAchievement
                {
                    // Save values.
                    CurrentValue = 1,
                    AchievementDate = DateTime.Now,
                    UserId = userAction.UserId,
                    AchievementId = achievement.Id
                };

                // Insert new achievement for user.
                UserAchievementRepository repository = new UserAchievementRepository();
                
                // Use connection context.
                using (IDbConnection con = new SqlConnection(_dsn))
                {
                    con.Open();

                    repository.Insert(con, userAchievement);
                }
            }
        }
        
        return achievementMet;
    }
}