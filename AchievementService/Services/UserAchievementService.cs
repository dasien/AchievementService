using System.Data;
using Microsoft.Data.SqlClient;
using AchievementService.Models;
using AchievementService.Repositories;
using AchievementService.Validators;

namespace AchievementService.Services
{
   public class UserAchievementService
   {
      private string _dsn;

      public UserAchievementService(string dsn)
      {
         _dsn = dsn;
      }

      public List<UserAchievement> GetUserAchievements(int userId)
      {
         // Data object.
         UserAchievementRepository repository = new UserAchievementRepository();
         
         // List of achievements for this user.
         List<UserAchievement> retVal = null;

         try
         {
            // Use connection context.
            using (IDbConnection con = new SqlConnection(_dsn))
            {
               con.Open();

               // Search for Achievements.
               retVal = repository.GetByUserId(con, userId) as List<UserAchievement>;
            }
         }

         catch (Exception ex)
         {
            throw new Exception("Unable to get user achievements.", ex);
         }

         // Return the list of Albums.
         return retVal;
      }

      public void ResetAllUserAchievements(int userId)
      {
         UserAchievementRepository repository = new UserAchievementRepository();

         try
         {
            // Use connection context.
            using (IDbConnection con = new SqlConnection(_dsn))
            {
               con.Open();
               repository.DeleteAll(con, userId);
            }
         }

         catch (Exception ex)
         {
            throw new Exception("Unable to reset user achievements.", ex);
         }
      }

      public List<Achievement> CheckForAchievement(UserAction action)
      {
         // The list of met achievements.
         List<Achievement> retVal = new List<Achievement>();
         
         // The Achievement respository.
         AchievementRepository repository = new AchievementRepository();
         
         try
         {
            List<Achievement>? achievements = null;
            
            // Get achievements for this user.
            List<UserAchievement> userAchievements = GetUserAchievements(action.UserId);
            
            // Get the achievements for this action.
            using (IDbConnection con = new SqlConnection(_dsn))
            {
               con.Open();

               // Search for Achievements.
               achievements = repository.GetAchievementsForUserAction(con, action.UserActionType) as List<Achievement>;
            }
            
            // Check to see if there are any achievements for this action.
            if (achievements != null)
            {
               // Loop through the possible achievements.
               foreach (Achievement achievement in achievements)
               {
                  // Check to see if the user has this achievement at all (in progress or completed).
                  UserAchievement? orgUserAchievement = userAchievements.Find(x => x.AchievementId == achievement.Id);
                  
                  // Create the appropriate validator.
                  IAchievementValidator validator =
                     AchievementValidatorFactory.CreateAchievementValidator(achievement.ValidatorType, _dsn);
                  
                  // Check for achievement progress.
                  bool result = validator.Validate(achievement, orgUserAchievement, action);
                  
                  // Add achievement to return list if it was met.
                  if (result)
                  {
                     retVal.Add(achievement);
                  }
               }
            }
         }

         catch (Exception ex)
         {
            throw new Exception("Unable to validate user achievement for action.", ex);
         }
         
         // Return results.
         return retVal;
      }
   }
}

/* Achievements

High level description...

-  User takes some action, for example saving a new Login vault item.
-  The client code would call the AchievementService with that action as a parameter, and some user context to 
   identify the user performing the action.
-  The UserAction is related to zero to many Achievements.  This could be done in a very hard coded way in the 
   AchievementService, or in a very dynamic way, by an association table in the database.
-  The AchievementService checks for the presence of conditions necessary to cause an Achievement to be triggered.
   Some of these will require more context (e.g. how many items of a certain type are in the vault already, what the
   current date is, etc.)
-  The AchievementService stores any achievements which have been met with the current request.
-  The AchievementService returns a collection of met Achievements to the client, which displays them in some way to 
   celebrate the action being taken, milestone reached, etc.
-  We would need a place for a user to see all their past achievements, progress toward next milestones, and 'locked'
   achievements.
   
 
VAULT ACTION 
First Vault Item Add (+10, +50, +100)
First Login Type Added (+10, +50, +100)
First Card Type Added (+10, +50, +100)
First ID Type Added (+10, +50, +100)
First Note Added (+10, +50, +100)
First Send (+10, +50, +100)
Vault Item Delete
Vault Item Add to Folder
Vault Item Collection Move
First Autofill (+100, +500, +1000)
Add > 1 URI to vault item
Add attachment to item
Use the generator (+10, +50, +100)
Vault size > 50, 500, 1000, 1999, 5000

VAULT ACTION + DATE
Add item on Dec 31st
Add item on Jan 1st
Add item on July 4th
Add item after midnight
Add item before 6am
Add item on Friday
Add item on Monday
Add item on Saturday/Sunday
Autofill after midnight
Autofill before 6am

ACCOUNT ACTION
Add 2nd device
Add 2FA
Join Organization
...

*/ 