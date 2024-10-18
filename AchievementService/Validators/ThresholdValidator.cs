using System.Data;
using System.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;

namespace AchievementService.Validators;

public class ThresholdValidator : IAchievementValidator
{
    private string _dsn;
    
    public ThresholdValidator(string dsn)
    {
        _dsn = dsn;
    }
    
    public bool Validate(Achievement achievement, UserAchievement? userAchievement, UserAction userAction)
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
                
                // Save it.
                UserAchievementRepository repository = new UserAchievementRepository();
                
                // Use connection context.
                using (IDbConnection con = new SqlConnection(_dsn))
                {
                    con.Open();

                    repository.Update(con, userAchievement);
                }
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
        
        // Return flag to indicate achievement reached.
        return achievementMet;
    }
}