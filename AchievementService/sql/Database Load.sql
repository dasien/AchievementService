use Achievement
GO

INSERT INTO [User] (UserName) VALUES('Test User')
GO

INSERT INTO AchievementValidatorType(Name, Description)
Values ('Single Use','This validator is satisfied the first time the user performs the associated action.'),
 ('Threshold','This validator is satisfied upon the user performing an action a certain number of times.'),
 ('Date Specific','This validator is satisfied upon the user performing an action on a specific on a specific day or at a specific time.')

GO

INSERT INTO Achievement (AchievementName,AchievementDescription,ValueToAchieve,ValidatorTypeId,IconName)
VALUES ('Start Me Up!', 'Add your first item to your vault.',1,1000,	NULL),
('Double Digits',	'Add ten items to your vault.',	10,	1001,	NULL),
('Half a Hundred', 'Add fifty items to your vault.',50,1001,NULL),
('Century Mark','Add one hundred items to your vault.',100,1001,NULL),
('Access Granted','Add your first Login item to your vault.',1,1000,NULL),
('Put it on My Card', 'Add your first Card item to your vault.',1,1000,NULL),
('Papers Please',	'Add your first Identity item to your vault.',1,1000,NULL),
('Jot it Down', 'Add your first Note item to your vault.',1,1000,NULL),
('Feeling the Love', 'Favorite an item',1,1000,NULL),
('Passing Notes', 'Use Send for the first time.',1,1000,NULL),
('Just Here to Help', 'Use Autofill for the first time',1,1000,NULL),
('Start the Year Off Right', 'Add an item on January 1st.',1,1002,NULL),
('End on a High Note', '	Add an item on December 31st',1,1002,NULL),
('Working for the Weekend', '	Add an item on a Friday',1,1002,NULL),
('Case of the Mondays', 'Add an item on a Monday',1,1002,NULL)

GO

INSERT INTO UserActionToAchievement (UserActionId, AchievementId)
values(1000,1000),
(1000,1001),
(1000,1002),
(1000,1003),
(1000,1004),
(1001,1000),
(1001,1001),
(1001,1002),
(1001,1003),
(1001,1005),
(1002,1000),
(1002,1001),
(1002,1002),
(1002,1003),
(1002,1006),
(1003,1000),
(1003,1001),
(1003,1002),
(1003,1003),
(1003,1007)

GO