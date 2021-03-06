USE [master]
GO
/****** Object:  Database [DBGames]    Script Date: 12/09/2017 22:31:42 ******/
CREATE DATABASE [DBGames]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBGames', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DBGames.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBGames_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DBGames_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBGames] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBGames].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBGames] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBGames] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBGames] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBGames] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBGames] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBGames] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBGames] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBGames] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBGames] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBGames] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBGames] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBGames] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBGames] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBGames] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBGames] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBGames] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBGames] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBGames] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBGames] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBGames] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBGames] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBGames] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBGames] SET RECOVERY FULL 
GO
ALTER DATABASE [DBGames] SET  MULTI_USER 
GO
ALTER DATABASE [DBGames] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBGames] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBGames] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBGames] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DBGames] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBGames', N'ON'
GO
USE [DBGames]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 12/09/2017 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[gameId] [bigint] NOT NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[gameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Player]    Script Date: 12/09/2017 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[playerId] [bigint] NOT NULL,
	[balance] [bigint] NULL,
	[lastUpdateDate] [datetime] NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[playerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ScoreGamesPlayers]    Script Date: 12/09/2017 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScoreGamesPlayers](
	[playerId] [bigint] NOT NULL,
	[gameId] [bigint] NULL,
	[win] [bigint] NULL,
	[timestamp] [datetime] NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ScoreGamesPlayers]  WITH CHECK ADD  CONSTRAINT [FK_ScoreGamesPlayers_Game] FOREIGN KEY([gameId])
REFERENCES [dbo].[Game] ([gameId])
GO
ALTER TABLE [dbo].[ScoreGamesPlayers] CHECK CONSTRAINT [FK_ScoreGamesPlayers_Game]
GO
ALTER TABLE [dbo].[ScoreGamesPlayers]  WITH CHECK ADD  CONSTRAINT [FK_ScoreGamesPlayers_Player] FOREIGN KEY([playerId])
REFERENCES [dbo].[Player] ([playerId])
GO
ALTER TABLE [dbo].[ScoreGamesPlayers] CHECK CONSTRAINT [FK_ScoreGamesPlayers_Player]
GO
/****** Object:  StoredProcedure [dbo].[GETTOP100PLAYERS]    Script Date: 12/09/2017 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GETTOP100PLAYERS]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 100 playerId
      ,balance
      ,lastUpdateDate
	FROM Player order by balance desc
END

GO
/****** Object:  StoredProcedure [dbo].[SETSCOREGAMES]    Script Date: 12/09/2017 22:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SETSCOREGAMES] 
	-- Add the parameters for the stored procedure here
	@PlayerId as bigint,
	@gameId as bigint,
	@Win as bigint,
    @Timestamp as bigint
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @EXIST AS INT

	SET @EXIST = (SELECT COUNT(gameId) FROM Game WHERE gameId = @gameId)

	IF(@EXIST = 0)
	BEGIN
		INSERT INTO [dbo].[Game]
           ([gameId])
     VALUES
           (@gameId)		   
	END

	SET @EXIST = (SELECT COUNT(playerId) FROM Player WHERE playerId = @PlayerId)
	IF(@EXIST = 0)
	BEGIN
		INSERT INTO [dbo].[Player]
           ([playerId]
           ,[balance]
           ,[lastUpdateDate])
		 VALUES
			   (@PlayerId
			   ,@Win
			   ,GETDATE())  
	END
	ELSE
		BEGIN
			UPDATE [dbo].[Player]
				SET balance += @Win
				,lastUpdateDate = GETDATE()
			WHERE playerId = @PlayerId
		END 


	INSERT INTO [dbo].[ScoreGamesPlayers]
           ([playerId]
           ,[gameId]
           ,[win]
           ,[timestamp])
     VALUES
           (@PlayerId
           ,@gameId
           ,@Win
           ,@Timestamp)


END

GO
USE [master]
GO
ALTER DATABASE [DBGames] SET  READ_WRITE 
GO
