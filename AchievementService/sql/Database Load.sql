
use Achievement
GO

insert into [user] ( [UserName] )
values ( N'Test User');
GO

insert into achievementvalidatortype ( [Name] , [Description] )
values (N'Threshold' , N'This validator is satisfied upon the user performing an action a certain number of times, including one.'),
       (N'Range' , N'This validator is satisfied if the user action falls between two values, inclusive.'),
       (N'Day of Week' , N'This validator is satisfied when the user performs an action on a particular day of the week.'),
       (N'Day of Year' , N'This validator is satisfied when the user performs an action on a particular day of the year.');
GO

insert into achievement ( [AchievementName] , [AchievementDescription] , [ValueToAchieve] , [LowRangeValue] , [HighRangeValue] , [SingleValue] , [ValidatorTypeId] , [IconName] )
values (N'Start Me Up!' , N'Add your first item to your vault.' , 1 , null , null , null , 1000 , null),
       (N'Double Digits' , N'Add ten items to your vault.' , 10 , null , null , null , 1000 , null),
       (N'Half a Hundred' , N'Add fifty items to your vault.' , 50 , null , null , null , 1000 , null),
       (N'Century Mark' , N'Add one hundred items to your vault.' , 100 , null , null , null , 1000 , null),
       (N'Access Granted' , N'Add your first Login item to your vault.' , 1 , null , null , null , 1000 , null),
       (N'Put it on My Card' , N'Add your first Card item to your vault.' , 1 , null , null , null , 1000 , null),
       (N'Papers Please' , N'Add your first Identity item to your vault.' , 1 , null , null , null , 1000 , null),
       (N'Jot it Down' , N'Add your first Note item to your vault.' , 1 , null , null , null , 1000 , null),
       (N'Feeling the Love' , N'Favorite an item' , 1 , null , null , null , 1000 , null),
       (N'Passing Notes' , N'Use Send for the first time.' , 1 , null , null , null , 1000 , null),
       (N'Just Here to Help' , N'Use Autofill for the first time' , 1 , null , null , null , 1000 , null),
       (N'Start the Year Off Right' , N'Add an item on January 1st.' , 1 , null , null , 1 , 1003 , null),
       (N'End on a High Note' , N'Add an item on December 31st' , 1 , null , null , 365 , 1003 , null),
       (N'Working for the Weekend' , N'Add an item on a Friday' , 1 , null , null , 4 , 1002 , null),
       (N'Case of the Mondays' , N'Add an item on a Monday' , 1 , null , null , null , 1002 , null),
       (N'Night Owl' , N'Add a vault item between midnight and 4am.' , 1 , 1 , 4 , null , 1001 , null);
GO

insert into useractiontoachievement ( [UserActionId] , [AchievementId] )
values (1000 , 1000),(1000 , 1001), (1000 , 1002), (1000 , 1003),
       (1000 , 1004), (1000 , 1011), (1000 , 1012), (1000 , 1013),
       (1000 , 1014), (1000 , 1015), (1001 , 1000), (1001 , 1001),
       (1001 , 1002), (1001 , 1003), (1001 , 1005), (1001 , 1011),
       (1001 , 1012), (1001 , 1013), (1001 , 1014), (1001 , 1015),
       (1002 , 1000), (1002 , 1001), (1002 , 1002), (1002 , 1003),
       (1002 , 1006), (1002 , 1011), (1002 , 1012), (1002 , 1013),
       (1002 , 1014), (1002 , 1015), (1003 , 1000), (1003 , 1001),
       (1003 , 1002), (1003 , 1003), (1003 , 1007), (1003 , 1011),
       (1003 , 1012), (1003 , 1013), (1003 , 1014), (1003 , 1015);
GO