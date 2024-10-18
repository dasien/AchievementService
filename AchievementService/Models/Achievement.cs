using Trestle.Core.Entities;

namespace AchievementService.Models;

public class Achievement : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public int ValueToAchieve { get; set; }
    
    public int ValidatorType { get; set; }

    public Achievement()
    {
        
    }
    
    public Achievement(int id, string name, string description, string icon, int valueToAchieve, int validatorType)
    {
        Id = id;
        Name = name;
        Description = description;
        Icon = icon;
        ValueToAchieve = valueToAchieve;
        ValidatorType = validatorType;
    }
}