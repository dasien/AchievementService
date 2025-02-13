using System.Diagnostics.CodeAnalysis;
using AchievementService.Models;
using AchievementService.Services;
using Microsoft.Extensions.Configuration;

namespace AchievementServiceCLI
{
    public class Program
    {
        private static IConfigurationRoot _config;
        private static string _dsn;
        
        static void Main(string[] args)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            _dsn = _config["Dsn"];
            
            
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Show achievements");
            Console.WriteLine("2) Add item to vault");
            Console.WriteLine("3) Add item to vault - Simulate Jan 1st.");
            Console.WriteLine("4) Add item to vault - Simulate Friday");
            Console.WriteLine("5) Add item to vault - Simulate late night");
            Console.WriteLine("6) Reset Achievements");
            Console.WriteLine("7) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowAchievements();
                    return true;
                case "2":
                    AddVaultItem();
                    return true;
                case "3":
                    AddVaultItem(new DateTime(2025, 01, 01));
                    return true;
                case "4":
                    AddVaultItem(new DateTime(2025, 02, 07));
                    return true;
                case "5":
                    AddVaultItem(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 01, 0, 0));
                    return true;
                case "6":
                    ResetAchievements();
                    return true;
                case "7":
                    return false;
                default:
                    return true;
            }
        }

        private static void ShowAchievements()
        {
            UserAchievementService service = new UserAchievementService(_dsn);

            List<UserAchievement> results = service.GetUserAchievements(1000);
            
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("---------------------------------Achievements-----------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------");

            if (results != null && results.Count > 0)
            {
                foreach (UserAchievement achievement in results)
                {
                    string completion = achievement.CurrentValue == achievement.ValueToAchieve ? "Completed" : 
                            achievement.CurrentValue + "/" + achievement.ValueToAchieve;
                    
                    Console.WriteLine("-- " + achievement.AchievementName + " -- " + achievement.AchievementDescription
                    + " -- " + completion + " --");
                }
            }

            else
            {
                Console.WriteLine("-- You do not have any achievements.");
            }
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("--------------------------------------------------------------------------------");
            
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void AddVaultItem(DateTime? customDate = null)
        {
            Console.WriteLine("(Login, Card, Identity, or Note");
            Console.Write("Enter Item Type (L/C/I/N): ");
            
            UserAchievementService service = new UserAchievementService(_dsn);
            List<Achievement> results = new List<Achievement>();
            DateTime userDate = customDate ?? DateTime.Now;
            
            switch (Console.ReadLine().ToUpper())
            {
                case "L":
                    results = service.CheckForAchievement(new UserAction(1000, UserActionType.NewLoginItemSave, userDate));
                    break;
                case "C":
                    results = service.CheckForAchievement(new UserAction(1000, UserActionType.NewCardItemSave, userDate));
                    break;
                case "I":
                    results = service.CheckForAchievement(new UserAction(1000, UserActionType.NewIdentityItemSave, userDate));
                    break;
                case "N":
                    results = service.CheckForAchievement(new UserAction(1000, UserActionType.NewNoteItemSave, userDate));
                    break;
            }
            
            Console.WriteLine();
            
            foreach (Achievement item in results) 
            {
                Console.WriteLine("You earned the achievement " + item.Name + ": " + item.Description);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void ResetAchievements()
        {
            UserAchievementService service = new UserAchievementService(_dsn);
            service.ResetAllUserAchievements(1000);
            
            Console.WriteLine("User Achievements Reset");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}