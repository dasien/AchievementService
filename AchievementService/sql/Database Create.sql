USE [master]
GO
/****** Object:  Database [Achievement]    Script Date: 10/17/2024 4:28:26 PM ******/
CREATE DATABASE [Achievement]
    CONTAINMENT = NONE
WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Achievement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    begin
        EXEC [Achievement].[dbo].[sp_fulltext_database] @action = 'enable'
    end
GO
ALTER DATABASE [Achievement] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Achievement] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Achievement] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Achievement] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Achievement] SET ARITHABORT OFF
GO
ALTER DATABASE [Achievement] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Achievement] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Achievement] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Achievement] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Achievement] SET CURSOR_DEFAULT GLOBAL
GO
ALTER DATABASE [Achievement] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Achievement] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Achievement] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Achievement] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Achievement] SET DISABLE_BROKER
GO
ALTER DATABASE [Achievement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Achievement] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Achievement] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Achievement] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Achievement] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Achievement] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Achievement] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Achievement] SET RECOVERY FULL
GO
ALTER DATABASE [Achievement] SET MULTI_USER
GO
ALTER DATABASE [Achievement] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Achievement] SET DB_CHAINING OFF
GO
ALTER DATABASE [Achievement] SET FILESTREAM ( NON_TRANSACTED_ACCESS = OFF )
GO
ALTER DATABASE [Achievement] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO
ALTER DATABASE [Achievement] SET DELAYED_DURABILITY = DISABLED
GO
ALTER DATABASE [Achievement] SET ACCELERATED_DATABASE_RECOVERY = OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'Achievement', N'ON'
GO
ALTER DATABASE [Achievement] SET QUERY_STORE = OFF
GO
USE [Achievement]
GO
create table AchievementValidatorType
(
    Id          int identity (1000, 1)
        constraint PK_AchievementValidatorType
            primary key,
    Name        nvarchar(255)  not null,
    Description nvarchar(2000) not null
)
go

create table Achievement
(
    AchievementId          int identity (1000, 1)
        constraint Achievement_pk
            primary key,
    AchievementName        nvarchar(255)  not null,
    AchievementDescription nvarchar(2000) not null,
    ValueToAchieve         int,
    LowRangeValue          int,
    HighRangeValue         int,
    SingleValue            int,
    ValidatorTypeId        int            not null
        constraint FK_AchievementValidatorType
            references AchievementValidatorType,
    IconName               nvarchar(255)
)
go

create table [User]
(
    UserId   int identity (1000, 1)
        constraint OrgUser_pk
            primary key,
    UserName nvarchar(255) not null
)
go

create table UserAchievement
(
    UserId          int not null
        constraint FKOrgUser
            references [User],
    AchievementId   int not null
        constraint FKAchievement
            references Achievement,
    CurrentValue    int,
    AchievementDate datetime,
    constraint OrgUserAchievement_pk
        primary key (UserId, AchievementId)
)
go

create table UserActionToAchievement
(
    UserActionId  int not null,
    AchievementId int not null
        constraint AchievementIdFK
            references Achievement,
    constraint PK_UserActionToAchievement
        primary key (UserActionId, AchievementId)
)
go

CREATE VIEW [dbo].[UserAchievementView]
AS
SELECT   ua.UserId, a.AchievementId, ua.CurrentValue, ua.AchievementDate, a.AchievementName, a.AchievementDescription, a.ValueToAchieve
FROM     dbo.UserAchievement AS ua INNER JOIN
         dbo.Achievement AS a ON a.AchievementId = ua.AchievementId
go

exec sp_addextendedproperty 'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 180
               Right = 465
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ua"
            Begin Extent = 
               Top = 9
               Left = 522
               Bottom = 206
               Right = 765
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 3370
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', 'dbo', 'VIEW', 'UserAchievementView'
go

exec sp_addextendedproperty 'MS_DiagramPaneCount', 1, 'SCHEMA', 'dbo', 'VIEW', 'UserAchievementView'
go


CREATE PROCEDURE [dbo].[DeleteAchievement]
@Id int
AS

DELETE FROM [dbo].[Achievement]
WHERE
    [AchievementId] = @Id
go


CREATE PROCEDURE [dbo].[DeleteAllAchievementsForUser]
@UserId int
AS

DELETE FROM [dbo].[UserAchievement]
WHERE
    [UserId] = @UserId
go


CREATE PROCEDURE [dbo].[DeleteUserAchievement]
    @UserId int,
    @AchievementId int

AS

DELETE FROM [dbo].[UserAchievement]
WHERE
    [UserId] = @UserId AND [AchievementId] = @AchievementId
go


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAchievementsByUserId]
    -- Add the parameters for the stored procedure here
@UserId int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT UserId
         ,AchievementId
         ,CurrentValue
         ,AchievementDate
         ,AchievementName
         ,AchievementDescription
         ,ValueToAchieve
    FROM UserAchievementView

    WHERE UserId = @UserId
END
go


CREATE PROCEDURE [dbo].[GetAchievementsForUserAction]
@UserAction	int

AS
BEGIN
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT a.AchievementId
         ,[AchievementName]
         ,[AchievementDescription]
         ,[ValueToAchieve]
         ,[LowRangeValue]
         ,[HighRangeValue]
         ,[SingleValue]
         ,[ValidatorTypeId]
         ,[IconName]
    FROM Achievement a

             JOIN UserActionToAchievement ua

                  on a.AchievementId = ua.AchievementId
    WHERE ua.UserActionId = @UserAction
END
go



CREATE PROCEDURE [dbo].[InsertUserAchievement]
    @UserId int,
    @AchievementId int,
    @CurrentValue int,
    @AchievementDate datetime
AS

INSERT INTO [dbo].[UserAchievement]
([UserId]
,[AchievementId]
,[CurrentValue]
,[AchievementDate])
VALUES
    (@UserId,
     @AchievementId,
     @CurrentValue,
     @AchievementDate)
go


CREATE PROCEDURE [dbo].[UpdateAchievement]
    @Id int,
    @Name nvarchar(255),
    @Description nvarchar(2000),
    @ValueToAchieve int,
    @LowRangeValue int,
    @HighRangeValue int,
    @ValidatorType int,
    @Icon nvarchar(255)
AS

UPDATE [dbo].[Achievement]

SET
    [AchievementName] = @Name,
    [AchievementDescription] = @Description,
    [ValueToAchieve] = @ValueToAchieve,
    [LowRangeValue] = @LowRangeValue,
    [HighRangeValue] = @HighRangeValue,
    [ValidatorTypeId] = @ValidatorType,
    [IconName] = @Icon

WHERE AchievementId = @Id
go



CREATE PROCEDURE [dbo].[UpdateUserAchievement]
    @UserId int,
    @AchievementId int,
    @CurrentValue int,
    @AchievementDate datetime

AS

UPDATE [dbo].[UserAchievement]
SET
    [CurrentValue] = @CurrentValue,
    [AchievementDate] = @AchievementDate
WHERE
    [UserId] = @UserId AND [AchievementId] = @AchievementId
go

