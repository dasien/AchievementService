using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class TimeOfDayRangeValidator : IAchievementValidator
{
    private string _dsn;
    
    public TimeOfDayRangeValidator(string dsn)
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
            // Check to see if the hour part of the date time is between the required values.
            if (achievement.LowRangeValue <= userAction.ActionDate.Hour && userAction.ActionDate.Hour <= achievement.HighRangeValue)
            {
                // Flag that the achievement has been reached.
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
        
        // Return the flag.
        return achievementMet;
    }
}