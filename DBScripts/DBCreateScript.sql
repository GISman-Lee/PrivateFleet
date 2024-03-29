IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'dotnet')
CREATE USER [dotnet] FOR LOGIN [dotnet] WITH DEFAULT_SCHEMA=[dbo]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_deactivate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Deactivate record in the input table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_deactivate]
	@id			varchar(100),
	@tableName	varchar(200)
AS
BEGIN
		
			DECLARE @Querry Varchar(500)
			SET @Querry=''''
			SET @Querry=@Querry +''UPDATE ''+@tableName+'' SET IsActive = 0 WHERE ID = ''''''+@id +''''''''
		
		EXEC(@Querry)
END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Delete record in the input table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_delete]
	@id			int,
	@tableName	varchar(200)
AS
BEGIN
	EXEC(	
			''DELETE FROM ''+@tableName+
			''WHERE ID = ''+@id
		)
END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_ConfigValues]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tbl_ConfigValues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Key] [varchar](50) NOT NULL,
	[Value] [varchar](50) NOT NULL,
	[Description] [varchar](500) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblConfigValues_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Tbl_ConfigValues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSeriesMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSeriesMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModelID] [int] NOT NULL,
	[Series] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblSeriesMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Tbl_SeriesMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStatusMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblStatusMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [varchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblStatusMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblAccessoriesMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblAccessoriesMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[IsParameter] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblAccessoriesMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_accessories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_setActionAccess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Set action level access to user/role
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_setActionAccess]
	@accessId		int, 
	@actionsIds		varchar(max)
AS
BEGIN

	-- activate/deactivate existing access information
	UPDATE  ACU_ActionAccess
	SET		IsActive = CASE
			WHEN	ActionID IN	(
									SELECT OrderId as pageId
									FROM   SplitIDs(@actionsIds)
								)
			THEN 1   -- activate 
			ELSE 0   -- deactivate
			END
	WHERE AccessID = @AccessId
	
	-- insert access details if not already present
	INSERT INTO ACU_ActionAccess
				(AccessID
				,ActionID
				,IsActive)
	SELECT 
				@AccessId as AccessID
				,orderid
				,1
	FROM		SplitIds(@actionsIds)
	WHERE		orderid NOT IN  ( 
									SELECT	ActionID 
									FROM	ACU_ActionAccess
									WHERE	AccessID=@AccessId
								)	
END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_setPageAccess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Set page level access to user/role
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_setPageAccess]
	@accessFor		int, 
	@accessTypeId	int, 
	@pageIds		varchar(max)
AS
BEGIN

	-- activate/deactivate existing access information
	UPDATE  ACU_Access
	SET		IsActive = CASE
			WHEN	pageId IN	(
									SELECT OrderId as pageId
									FROM   SplitIDs(@pageIds)
								)
			THEN 1   -- activate 
			ELSE 0   -- deactivate
			END
	WHERE accessFor = @accessFor
	
	-- insert access details if not already present
	INSERT INTO ACU_ACCESS
				(AccessFor
				,AccessTypeID
				,PageID
				,IsActive)
	SELECT 
				@accessFor as accessFor
				,@accessTypeId as accessTypeID
				,orderid
				,1
	FROM		SplitIds(@pageIds)
	WHERE		orderid NOT IN  ( 
									SELECT	PageID 
									FROM	acu_access 
									WHERE	accessFor=@accessFor 
								)	
END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleSeriesMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleSeriesMaster] 
	(
		@ID int,
		@IsActive bit,
		@DBOperation varchar(50),
		@ModelID int,
		@Series varchar(50)
	
		
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblSeriesMaster]
						   ([ModelID]
						   ,[Series])
					 VALUES
						   (@ModelID
						   ,@Series)
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE [dbo].[tblSeriesMaster]
							   SET [ModelID] = @ModelID
								  ,[Series] = @Series
								  ,[IsActive] = @IsActive
							 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblSeriesMaster]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleDealerMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleDealerMaster] 
	(
		@ID int,
		@IsActive bit,
		@DBOperation varchar(50),
		@Name varchar(200)
		,@Company varchar(200)
		,@Email varchar(200)
		,@Fax varchar(50)
		,@Phone varchar(50)
		,@Location int
		,@City int
		,@State int
		,@PCode varchar(50)

	
		
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblDealerMaster]
					   ([Name]
					   ,[Company]
					   ,[Email]
					   ,[Fax]
					   ,[Phone]
					   ,[Location]
					   ,[City]
					   ,[State]
					   ,[PCode]
					   ,[IsActive])
				 VALUES
					   (@Name 
					   ,@Company 
					   ,@Email 
					   ,@Fax 
					   ,@Phone 
					   ,@Location 
					   ,@City 
					   ,@State 
					   ,@PCode 
					   ,@IsActive )
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE [dbo].[tblDealerMaster]
							   SET [Name] = @Name 
								  ,[Company] = @Company 
								  ,[Email] = @Email 
								  ,[Fax] = @Fax 
								  ,[Phone] = @Phone 
								  ,[Location] = @Location 
								  ,[City] = @City 
								  ,[State] = @State 
								  ,[PCode] = @PCode 
							 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblDealerMaster]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_LockMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_LockMaster](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[LockKey] [nvarchar](250) NULL,
	[LockDescription] [varchar](150) NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_Tbl_LockMaster_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Tbl_LockMaster_CreatedDate]  DEFAULT (getdate()),
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_LockMaster] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_UnlockMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_UnlockMaster](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[LockKey] [nvarchar](250) NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_Tbl_UnlockMaster_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Tbl_UnlockMaster_CreatedDate]  DEFAULT (getdate()),
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_UnlockMaster] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleChargeTypeMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleChargeTypeMaster] 
	(
		@ID int,
		@IsActive bit,
		@DBOperation varchar(50),
		@Type varchar(100)

	
		
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblChargesTypesMaster]
						   ([Type])
					 VALUES
						   (@Type  )
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							
						UPDATE [dbo].[tblChargesTypesMaster]
						   SET [Type] = @Type 
							  ,[IsActive] = @IsActive 
						 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblChargesTypesMaster]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleMenuMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleMenuMaster] 
	(
		@ID int,
		@IsActive bit,
		@DBOperation varchar(50),
		@MenuText varchar(100),
		@MenuLink varchar(200)
	
		
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblMenuMaster]
					   ([MenuText]
					   ,[MenuLink])
				 VALUES
					   (@MenuText 
					   ,@MenuLink )
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							
						
						UPDATE [dbo].[tblMenuMaster]
						   SET [MenuText] = @MenuText 
							  ,[MenuLink] = @MenuLink 
						 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblMenuMaster]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitIDs]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'


create FUNCTION [dbo].[SplitIDs]
(
	@IDList varchar(max)
)
RETURNS 
@ParsedList table
(
	OrderID int
)
AS
BEGIN
	DECLARE @ID varchar(10), @Pos int
	SET @IDList = LTRIM(RTRIM(@IDList))+ '',''
	SET @Pos = CHARINDEX('','', @IDList, 1)
	IF REPLACE(@IDList, '','', '''') <> ''''
	BEGIN
		WHILE @Pos > 0
		BEGIN
			SET @ID = LTRIM(RTRIM(LEFT(@IDList, @Pos - 1)))
			IF @ID <> ''''
			BEGIN
				INSERT INTO @ParsedList (OrderID) 
				VALUES (CAST(@ID AS int)) 
			END
			SET @IDList = RIGHT(@IDList, LEN(@IDList) - @Pos)
			SET @Pos = CHARINDEX('','', @IDList, 1)
		END
	END	
	RETURN
END


----------------------------------------------------














































































' 
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblRoleMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblRoleMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Tbl_RoleMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateSeries]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateSeries]
	(
			@ID int,
			@IsActive bit
	)
AS
BEGIN
		UPDATE [dbo].[tblSeriesMaster]
		SET [IsActive] = @IsActive
		WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddSeries]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddSeries]
	(
			@ModelID int,
		@Series varchar(50)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblSeriesMaster]
						   ([ModelID]
						   ,[Series])
					 VALUES
						   (@ModelID
						   ,@Series)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateSeries]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateSeries]
	(
			@ID int,
			@ModelID int,
			@Series varchar(50)
	)
AS
BEGIN
		UPDATE [dbo].[tblSeriesMaster]
		SET 
				[ModelID] = @ModelID
				,[Series] = @Series
		WHERE	ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateChargeType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateChargeType]
	(
		@ID	int,	
		@Type varchar(100)
	)
AS
BEGIN
		UPDATE [dbo].[tblChargesTypesMaster]
		SET [Type] = @Type 
		WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIFChargeTypeExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIFChargeTypeExists]
	(
		@Type varchar(100)
	)
AS
BEGIN
		SELECT	
					ID	
		FROM
					 [dbo].[tblChargesTypesMaster]
		WHERE 
					[Type] = @Type 
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddChargeType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddChargeType]
	(
		
		@Type varchar(100)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblChargesTypesMaster]
						   ([Type])
					 VALUES
						   (@Type  )
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateChargeType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateChargeType]
	(
		@ID	int,	
		@IsActive bit
	)
AS
BEGIN
		UPDATE [dbo].[tblChargesTypesMaster]
		SET [IsActive] = @IsActive
		WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VWForQuotationData]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[VWForQuotationData]
AS
SELECT     TOP (100) PERCENT ID, Type AS [Key], '''' AS Specification, ''false'' AS IsAccessory, ''true'' AS IsChargeType
FROM         dbo.tblChargesTypesMaster
WHERE     (IsActive = 1) AND (Type <> ''Recommended Retail Price Exc GST'')
ORDER BY ID
' 
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
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
         Begin Table = "tblChargesTypesMaster"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 106
               Right = 190
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
         Column = 1440
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
' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VWForQuotationData'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VWForQuotationData'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateDealer]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblDealerMaster]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfSeriesExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfSeriesExists]
	(
			@ModelID int,
			@Series varchar(50)
	)
AS
BEGIN
		SELECT
				ID
		FROM
				 [dbo].[tblSeriesMaster]
		WHERE 
				[ModelID] = @ModelID
				AND [Series] = @Series
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPerticularQuotation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPerticularQuotation] --Use Sp as Prefix to Stored Procedure Name
	(
		@Query varchar(max)
	)	
AS
BEGIN
				exec (''''+ @Query )
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllActiveSeries]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllActiveSeries]
	

AS
BEGIN
	SELECT 
				SM.ID, SM.Series
	FROM
				dbo.tblSeriesMaster  as SM
				
	WHERE
				IsActive=1
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_Aactivate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Deactivate record in the input table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_Aactivate]
	@id			varchar(100),
	@tableName	varchar(200)
AS
BEGIN
		
			DECLARE @Querry Varchar(500)
			SET @Querry=''''
			SET @Querry=@Querry +''UPDATE ''+@tableName+'' SET IsActive = 1 WHERE ID = ''''''+@id +''''''''
		
		EXEC(@Querry)
END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[acu_Sp_CheckUserAlreadyExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[acu_Sp_CheckUserAlreadyExists]
	(
		@UserName varchar(200)	
	)
AS
BEGIN
	
	SELECT 
			ID
	FROM
			dbo.ACU_UserMaster
	WHERE
			UserName=@UserName
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddConfigData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddConfigData]
	(
		@Name varchar(100),
		@Value varchar(100)
	)
AS
BEGIN
	INSERT INTO [dbo].[tblConfigValues]
           ([Name]
           ,[Value])
     VALUES
           (@Name 
           ,@Value)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_UserMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_UserMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Name] [varchar](100) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](20) NULL,
	[Address] [varchar](200) NULL,
	[UsernameExpiryDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_UserMaster_isActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_UserMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_AccessTypeMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_AccessTypeMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
	[Priority] [int] NULL,
	[Description] [varchar](200) NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_ACU_AccessTypeMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_AccessTypeMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_PageMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_PageMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [varchar](100) NULL,
	[PageUrl] [varchar](255) NULL,
	[ParentID] [int] NOT NULL CONSTRAINT [DF_ACU_PageMaster_ParentID]  DEFAULT ((0)),
	[IsInternalLink] [bit] NOT NULL CONSTRAINT [DF_ACU_PageMaster_IsInternalLink]  DEFAULT ((0)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_PageMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_PageMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_ActionMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_ActionMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Action] [varchar](50) NULL,
	[Description] [varchar](200) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_ActionMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_ActionMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStateMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblStateMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblStateMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_tblStateMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMakeMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblMakeMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Make] [varchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblMakeMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_make] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_PageActionDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_PageActionDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageID] [int] NULL,
	[ActionID] [int] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_PageActionDetails_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_PageActionDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_RoleMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_RoleMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [varchar](50) NULL,
	[Description] [varchar](200) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_RoleMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_RoleMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblChargesTypesMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblChargesTypesMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[SortNo] [int] NULL CONSTRAINT [DF_tblChargesTypesMaster_SortNo]  DEFAULT ((0)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblChargesTypesMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Tbl_ChargesTypesMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_UserRoleDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_UserRoleDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_UserRoleDetails_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_UserRoleDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddPointsToDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 June 2010
-- Description:	Add points to dealer account
-- =============================================
CREATE PROCEDURE [dbo].[SpAddPointsToDealer]
	@RequestId		int,
	@DealerId		int,
	@Points			int
AS
BEGIN
	INSERT INTO [dbo].[tblDealerPoints]
           ([RequestID]
           ,[DealerID]
           ,[Points]
           ,[CreatedDate]
           ,[IsActive])
     VALUES
           (@RequestId
           ,@DealerId
           ,@Points
           ,getdate()
           ,1)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblRequestHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblRequestHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SeriesId] [int] NOT NULL,
	[ConsultantID] [int] NOT NULL,
	[Status] [int] NULL,
	[RequestDate] [datetime] NOT NULL CONSTRAINT [DF_Tbl_RequestHeader_RequestDate]  DEFAULT (getdate()),
	[ExpectedResponseDate] [datetime] NULL,
	[ConsultantNotes] [varchar](200) NULL,
	[Reason] [varchar](500) NULL,
	[QuotationID] [bigint] NULL,
	[OptionId] [bigint] NULL,
	[ShortListedDate] [datetime] NULL,
	[DealDoneDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_RequestHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSeriesAccessories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSeriesAccessories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccessoryID] [int] NOT NULL,
	[SeriesID] [int] NOT NULL,
	[Specification] [varchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblSeriesAccessories_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Tbl_Series_Accessories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_LockDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_LockDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[LockId] [int] NOT NULL,
	[LockType] [char](1) NOT NULL CONSTRAINT [DF_Tbl_LockDetails_LockType]  DEFAULT ('V'),
	[LockFrom] [datetime] NOT NULL,
	[LockTo] [datetime] NULL,
	[RequestFrom] [varchar](50) NULL,
	[RequestTo] [varchar](50) NOT NULL,
	[RequestReason] [varchar](250) NULL,
	[RequestStatus] [char](1) NOT NULL CONSTRAINT [DF_Tbl_LockDetails_RequestStatus]  DEFAULT ('P'),
	[StatusReason] [varchar](250) NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_Tbl_LockDetails_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Tbl_LockDetails_CreatedDate]  DEFAULT (getdate()),
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_LockDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'V-View,E-Edit,D-Delete' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Tbl_LockDetails', @level2type=N'COLUMN', @level2name=N'LockType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'P-Pending,A-Approved,R-Rejected' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Tbl_LockDetails', @level2type=N'COLUMN', @level2name=N'RequestStatus'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tbl_UnlockDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tbl_UnlockDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UnlockId] [int] NOT NULL,
	[UnlockFrom] [datetime] NULL,
	[UnlockTo] [datetime] NULL,
	[RequestFrom] [varchar](50) NULL,
	[RequestTo] [varchar](50) NOT NULL,
	[RequestReason] [varchar](250) NULL,
	[RequestStatus] [char](1) NOT NULL CONSTRAINT [DF_Tbl_UnlockDetails_RequestStatus]  DEFAULT ('P'),
	[StatusReason] [varchar](250) NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_Tbl_UnlockDetails_IsActive]  DEFAULT ((1)),
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Tbl_UnlockDetails_CreatedDate]  DEFAULT (getdate()),
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_UnlockDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'P-Pending,A-Approved,R-Rejected' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Tbl_UnlockDetails', @level2type=N'COLUMN', @level2name=N'RequestStatus'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUserMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Address] [varchar](500) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Tbl_UserMaster_CreatedDate]  DEFAULT (getdate()),
	[ModifiedBy] [varchar](50) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Tbl_UserMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuotationDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblQuotationDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuotationID] [int] NOT NULL,
	[RequestDetailID] [int] NOT NULL,
	[ACCIDorCHARGETYPEID] [int] NOT NULL,
	[OptionID] [int] NOT NULL,
	[QuoteValue] [numeric](18, 2) NOT NULL,
	[IsChargeType] [bit] NOT NULL,
 CONSTRAINT [PK_Tbl_QuotationDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDealerUserMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDealerUserMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DealerID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblDealerUserMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_tblDealerUserMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblMakeDealer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblMakeDealer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MakeID] [int] NOT NULL,
	[DealerID] [int] NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblMakeDealer_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_Tbl_Make_Dealer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblRequestDealer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblRequestDealer](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestId] [int] NOT NULL,
	[DealerID] [int] NOT NULL,
	[ShortListedDate] [datetime] NULL,
	[DealDoneDate] [datetime] NULL,
	[QuotationID] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Tbl_Request_Dealer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDealerPoints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDealerPoints](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [int] NOT NULL,
	[DealerID] [int] NOT NULL,
	[Points] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblDealerPoints_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_tblDealerPoints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_ActionAccess]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_ActionAccess](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccessID] [int] NULL,
	[ActionID] [int] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_ActionAccess_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_ActionAccess] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACU_Access]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ACU_Access](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccessFor] [int] NULL,
	[AccessTypeID] [int] NULL,
	[PageID] [int] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_ACU_Access_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_ACU_Access] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'this can be roleid or userid depending upon access type' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACU_Access', @level2type=N'COLUMN', @level2name=N'AccessFor'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'this will be type id which specifies type as role-based or user-based' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ACU_Access', @level2type=N'COLUMN', @level2name=N'AccessTypeID'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblModelMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblModelMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MakeID] [int] NOT NULL,
	[Model] [varchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblModelMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_model] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCityMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCityMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StateID] [int] NOT NULL,
	[City] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblCityMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_tblCityMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDealerMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDealerMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
	[Company] [varchar](200) NULL,
	[Email] [varchar](200) NULL,
	[Fax] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Location] [int] NOT NULL,
	[City] [int] NOT NULL,
	[State] [int] NOT NULL,
	[PCode] [varchar](50) NOT NULL,
	[IsHotDealer] [bit] NOT NULL CONSTRAINT [DF_tblDealerMaster_IsHotDealer]  DEFAULT ((0)),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_dealers_active]  DEFAULT ((1)),
 CONSTRAINT [PK_dealers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuotationHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblQuotationHeader](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [int] NOT NULL,
	[DealerID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[DealerNotes] [varchar](200) NULL,
	[EstimatedDeleveryDates] [datetime] NOT NULL,
	[ExStock] [numeric](18, 2) NOT NULL,
	[Order] [numeric](18, 2) NOT NULL,
	[ComplianceDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Tbl_QuotationHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblRequestAccessories]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblRequestAccessories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RequestID] [int] NOT NULL,
	[AccessoryID] [int] NOT NULL,
	[AccessorySpecification] [varchar](200) NULL,
 CONSTRAINT [PK_Tbl_RequestDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLocationMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLocationMaster](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CityID] [int] NOT NULL,
	[Location] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tblLocationMaster_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_tblLocationMaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllConfigValues]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllConfigValues]
AS
BEGIN
	
	SELECT 
			ID,[Key], Value, IsActive,Description
	FROM
			 [dbo].[tbl_ConfigValues]
	
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateConfigValue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateConfigValue]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tbl_ConfigValues]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfKeyExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfKeyExists]
	(
		@Name	varchar(100),
		@Value	varchar(500)
	)
AS
BEGIN
	
	SELECT 
			ID, [Key], Value, IsActive
	FROM
			 [dbo].[tbl_ConfigValues]
	WHERE 
			[Key]=@Name
			AND [Value]=@Value
			
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateConfigData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateConfigData]
	(
		@ID		int,
--		@Name varchar(100),
		@Value varchar(100)
	)
AS
BEGIN
	UPDATE [dbo].[tbl_ConfigValues]
	   SET [Value] = @Value 
--		   ,[Name] = @Name 
		  
	WHERE  ID=@ID
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpViewRecivedQuoteRequestForDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpViewRecivedQuoteRequestForDealer]
	(
		@UserID	int
	)
AS
BEGIN
	
		Select
					 RH.ID
					,RH.SeriesId AS SeriesID
					,isnull(RH.Status,'''') AS RequestStatus 
					,SM.Series 
					,MD.Model
					,MK.Make
					,RH.ConsultantNotes
					,convert(Varchar,RH.RequestDate,103) as RequestDate 
		From 
				dbo.tblRequestHeader as RH JOIN tblRequestDealer RD on RH.ID=RD.RequestID 
				JOIN  tblDealerUserMaster  DUM on DUM.DealerID=RD.DealerID AND RD.QuotationID is null 
				JOIN tblSeriesMaster SM ON RH.SeriesId = SM.ID
				JOIN tblModelMaster MD ON SM.ModelID = MD.ID
				JOIN tblMakeMaster MK ON MD.MakeID = MK.ID
where DUM.UserID=@UserID




END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetSentQuoteRequests]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get sent quote requests for consultant
-- =============================================
CREATE PROCEDURE [dbo].[SpGetSentQuoteRequests]
	@consultantId		INT
AS
BEGIN

--	DECLARE @temp table(RequestDate varchar(max),SeriesID int
--	,RequestStatus varchar(max),Series varchar(max),Model varchar(max),Make  varchar(max)
--	,ConsultantNotes varchar(max),ID int)
--
--	INSERT INTO @temp

	SELECT 
			 convert(Varchar,RH.RequestDate,103) as RequestDate
			,RH.SeriesId AS SeriesID
			,isnull(RH.Status,'''') AS RequestStatus 
			,SM.Series 
			,MD.Model
			,MK.Make
			,RH.ConsultantNotes
			,RH.ID
			,isnull(RH.QuotationID,0) AS QuotationID
			,isnull(RH.OptionID,0) AS OptionID
			,case when RH.ShortListedDate is not null
			 then convert(varchar,RH.ShortListedDate,103) 
			 else ''''
			 end ShortListedDate
	FROM	
			tblRequestHeader RH
			JOIN tblSeriesMaster SM ON RH.SeriesId = SM.ID
			JOIN tblModelMaster MD ON SM.ModelID = MD.ID
			JOIN tblMakeMaster MK ON MD.MakeID = MK.ID
	WHERE
			RH.ConsultantId = @consultantId
	ORDER BY 
			RH.RequestDate DESC


--	select * from @temp
--
--	select Temp.*,isNull(QH.ID,0) as QuotaionID
--	from @temp as Temp   JOIN tblQuotationHeader as QH ON Temp.ID=QH.requestID
	
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetRequestHeaderInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get quote request details
-- =============================================
CREATE PROCEDURE [dbo].[SpGetRequestHeaderInfo]
	@requestId		INT
AS
BEGIN
	SELECT 
			RH.SeriesId AS SeriesID
			,RH.ConsultantId AS ConsultantID
			,RH.Status AS RequestStatus 
			,SM.Series 
			,MD.Model
			,MK.Make
			,RH.ConsultantNotes
	FROM	
			tblRequestHeader RH
			JOIN tblSeriesMaster SM ON RH.SeriesId = SM.ID
			JOIN tblModelMaster MD ON SM.ModelID = MD.ID
			JOIN tblMakeMaster MK ON MD.MakeID = MK.ID
	WHERE
			RH.ID = @requestId
	
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetShortListedQuotationsRequests]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get sent quote requests for consultant
-- =============================================
CREATE PROCEDURE [dbo].[SpGetShortListedQuotationsRequests]
	@consultantId		INT
AS
BEGIN

--	DECLARE @temp table(RequestDate varchar(max),SeriesID int
--	,RequestStatus varchar(max),Series varchar(max),Model varchar(max),Make  varchar(max)
--	,ConsultantNotes varchar(max),ID int)
--
--	INSERT INTO @temp

	SELECT 
			 convert(Varchar,RH.RequestDate,103) as RequestDate
			,RH.SeriesId AS SeriesID
			,isnull(RH.Status,'''') AS RequestStatus 
			,SM.Series 
			,MD.Model
			,MK.Make
			,RH.ConsultantNotes
			,RH.ID
			,isnull(RH.QuotationID,0) AS QuotationID
			,isnull(RH.OptionID,0) AS OptionID
			,case when RH.ShortListedDate is not null
			 then convert(varchar,RH.ShortListedDate,103) 
			 else ''''
			 end ShortListedDate
	FROM	
			tblRequestHeader RH
			JOIN tblSeriesMaster SM ON RH.SeriesId = SM.ID
			JOIN tblModelMaster MD ON SM.ModelID = MD.ID
			JOIN tblMakeMaster MK ON MD.MakeID = MK.ID
			JOIN tblQuotationHeader QH ON QH.ID=RH.QuotationID
	WHERE
			RH.ConsultantId = @consultantId
	ORDER BY 
			RH.RequestDate DESC


--	select * from @temp
--
--	select Temp.*,isNull(QH.ID,0) as QuotaionID
--	from @temp as Temp   JOIN tblQuotationHeader as QH ON Temp.ID=QH.requestID
	
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllShortListedQuotationsRequests]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get sent quote requests for consultant
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllShortListedQuotationsRequests]
AS
BEGIN

--	DECLARE @temp table(RequestDate varchar(max),SeriesID int
--	,RequestStatus varchar(max),Series varchar(max),Model varchar(max),Make  varchar(max)
--	,ConsultantNotes varchar(max),ID int)
--
--	INSERT INTO @temp

	SELECT 
			 convert(Varchar,RH.RequestDate,103) as RequestDate
			,RH.SeriesId AS SeriesID
			,isnull(RH.Status,'''') AS RequestStatus 
			,SM.Series 
			,MD.Model
			,MK.Make
			,RH.ConsultantNotes
			,RH.ID
			,RH.ConsultantID
			,isnull(RH.QuotationID,0) AS QuotationID
			,isnull(RH.OptionID,0) AS OptionID
			,case when RH.ShortListedDate is not null
			 then convert(varchar,RH.ShortListedDate,103) 
			 else ''''
			 end ShortListedDate
	FROM	
			tblRequestHeader RH
			JOIN tblSeriesMaster SM ON RH.SeriesId = SM.ID
			JOIN tblModelMaster MD ON SM.ModelID = MD.ID
			JOIN tblMakeMaster MK ON MD.MakeID = MK.ID
			JOIN tblQuotationHeader QH ON QH.ID=RH.QuotationID
	
	ORDER BY 
			RH.RequestDate DESC


--	select * from @temp
--
--	select Temp.*,isNull(QH.ID,0) as QuotaionID
--	from @temp as Temp   JOIN tblQuotationHeader as QH ON Temp.ID=QH.requestID
	
END






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetActiveParameters]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 26 May 2010
-- Description:	Get active parameters
-- =============================================
CREATE PROCEDURE [dbo].[SpGetActiveParameters]
AS
BEGIN
		SELECT
					convert(Varchar,AM.ID) as ID, AM.[Name] as accessoryname, [Specification]=''''
		FROM	
					dbo.tblAccessoriesMaster as AM
		WHERE
					AM.IsActive=1
					AND AM.IsParameter = 1
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAdditionalAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 25 May 2010
-- Description:	Get additional accessories for series
--				(which are not already mapped to series)
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAdditionalAccessories]
	@SeriesId	int
AS
BEGIN
	SELECT
				DISTINCT(AM.ID) AS ID,AM.[Name]As Accessory ,SA.Specification, SA.IsActive
	FROM
				dbo.tblAccessoriesMaster as AM
				left JOIN dbo.tblSeriesAccessories as SA ON SA.AccessoryID=AM.ID 
	WHERE
				isnull(SA.SeriesID,0) <> @SeriesID
				AND AM.IsParameter = 0
	
END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetRequestAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get quote request accessories
-- =============================================
CREATE PROCEDURE [dbo].[SpGetRequestAccessories]
	@requestId		INT
AS
BEGIN
	SELECT 
			AM.ID as ID
			,AM.[Name] AS AccessoryName
			,RA.AccessorySpecification AS Specification
	FROM	
			tblRequestHeader RH
			JOIN tblRequestAccessories RA ON RH.ID = RA.RequestID
			JOIN tblAccessoriesMaster AM ON AM.ID = RA.AccessoryID
	WHERE
			RH.ID = @requestId
			AND AM.IsParameter = 0
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateAccessories]
	(
		@ID	int,	
		@IsActive bit
	)
AS
BEGIN
		UPDATE [dbo].[tblAccessoriesMaster]
		SET [IsActive] = @IsActive
		WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateAccessories]
	(
		@ID	int,	
		@Name varchar(100),
        @IsParameter bit
	)
AS
BEGIN
		UPDATE [dbo].[tblAccessoriesMaster]
		SET		[Name] = @Name 
				,[IsParameter] = @IsParameter 
		 WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddAccessories]
	(
		@Name varchar(100),
        @IsParameter bit
	)
AS
BEGIN
		INSERT INTO [dbo].[tblAccessoriesMaster]
						   ([Name]
						   ,[IsParameter])
					 VALUES
						   (@Name 
						   ,@IsParameter)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfAccessoryExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfAccessoryExists]
	(
		@Name varchar(100),
        @IsParameter bit
	)
AS
BEGIN
		SELECT 
					ID
		FROM		
					[dbo].[tblAccessoriesMaster]
		
		WHERE		[Name]=@Name AND
					[IsParameter]=@IsParameter
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllAccessories]
	
AS
BEGIN
		SELECT
					ID, AM.Name, IsParameter, IsActive
		FROM	
					dbo.tblAccessoriesMaster as AM
--		WHERE
--					IsActive=1
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleAccessoriesMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleAccessoriesMaster] 
	(
		@ID int,
		@IsActive bit,
		@DBOperation varchar(50),
		@Name varchar(100),
        @IsParameter bit
	
		
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblAccessoriesMaster]
						   ([Name]
						   ,[IsParameter]
						   ,[IsActive])
					 VALUES
						   (@Name 
						   ,@IsParameter 
						   ,@IsActive )
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							
						
						UPDATE [dbo].[tblAccessoriesMaster]
						   SET [Name] = @Name 
							  ,[IsParameter] = @IsParameter 
						 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblAccessoriesMaster]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetRequestParameters]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get quote request parameters
-- =============================================
create PROCEDURE [dbo].[SpGetRequestParameters]
	@requestId		INT
AS
BEGIN
	SELECT 
			AM.[Name] AS Parameter
			,RA.AccessorySpecification AS ParamValue
	FROM	
			tblRequestHeader RH
			JOIN tblRequestAccessories RA ON RH.ID = RA.RequestID
			JOIN tblAccessoriesMaster AM ON AM.ID = RA.AccessoryID
	WHERE
			RH.ID = @requestId
			AND AM.IsParameter = 1
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetDataForQuotation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get quote request accessories
-- =============================================
CREATE PROCEDURE [dbo].[SpGetDataForQuotation]  
	@requestId		INT
AS
BEGIN

	

	SELECT 
			ID as ID,''Recommended Retail Price Exc GST'' as [Key],'''' as Specification,''false'' as IsAccessory,''true'' as IsChargeType
	FROM
			dbo.tblChargesTypesMaster
	WHERE	
				Type=''Recommended Retail Price Exc GST''
	
	UNION ALL

	SELECT 
			'''' as ID,''Additional Accessories'' as [Key],'''' as Specification,''false'' as IsAccessory,''false'' as IsChargeType
	
	UNION ALL
	SELECT 
			AM.ID as ID, AM.[Name] AS [Key]
			,RA.AccessorySpecification AS Specification,''true'' as IsAccessory,''false'' as IsChargeType--, '''' as  Value1,'''' as  Value2
			
	FROM	
			tblRequestHeader RH
			JOIN tblRequestAccessories RA ON RH.ID = RA.RequestID
			JOIN tblAccessoriesMaster AM ON AM.ID = RA.AccessoryID
	WHERE
			RH.ID = @requestId
			AND AM.IsParameter = 0


	UNION ALL
	
		SELECT 
			
			'''' as ID,''Fixed Charges'' as [Key],'''' as Specification,''false'' as IsAccessory,''false'' as IsChargeType

	UNION ALL

		SELECT 
					*
		FROM
						dbo.VWForQuotationData
		
	
	
END



--select * from dbo.tblRequestAccessories' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetQuoteComparisonInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude	
-- Create date: 08 June 2010
-- Description:	Get dealers and options for particular quote request
-- =============================================
CREATE PROCEDURE [dbo].[SpGetQuoteComparisonInfo]
	@requestId		int,
	@consultantId	int
AS
BEGIN
	
	-- get fixed charge types info 
	SELECT 
			RH.ID as RequestID
			,RH.SeriesId
			,RD.DealerID
			,DM.[Name] as DealerName
			,QH.DealerNotes
			,QH.ExStock
			,QH.[Order]
			,QD.OptionID 
			,QD.QuotationID
			,''$''+convert(varchar,isnull(QD.QuoteValue,0)) as QuoteValue
			,CT.ID	as ChargeTypeID
			,CT.[Type] as ChargeType
	FROM 
			tblRequestHeader RH
			JOIN tblRequestDealer RD ON RH.ID = RD.RequestId
			JOIN tblDealerMaster DM ON DM.ID = RD.DealerID
			JOIN tblQuotationHeader QH ON RH.ID = QH.RequestID AND RD.DealerID = QH.DealerID		
			JOIN tblQuotationDetails QD ON QH.ID = QD.QuotationID
			JOIN tblChargesTypesMaster CT ON CT.ID=QD.ACCIDorCHARGETYPEID
	WHERE
			RH.ID = @RequestId
			AND RH.ConsultantID = @ConsultantId
			AND QD.IsChargeType = 1


	-- get accessories info
	SELECT 
			RD.DealerID
			,DM.[Name] as DealerName
			,QD.OptionID
			,''$''+convert(varchar,isnull(QD.QuoteValue,0)) as QuoteValue
			,QD.QuotationID		
			,RA.AccessoryID
			,AM.[Name] as AccessoryName
	FROM 
			tblRequestHeader RH
			JOIN tblRequestDealer RD ON RH.ID = RD.RequestId
			JOIN tblDealerMaster DM ON DM.ID = RD.DealerID
			JOIN tblQuotationHeader QH ON RH.ID = QH.RequestID AND RD.DealerID = QH.DealerID		
			JOIN tblQuotationDetails QD ON QH.ID = QD.QuotationID
			JOIN tblRequestAccessories RA ON RA.RequestID=@RequestId 
			JOIN tblAccessoriesMaster AM ON RA.AccessoryID = AM.ID AND AM.ID=QD.ACCIDorCHARGETYPEID
	WHERE
			RH.ID = @RequestId
			AND RH.ConsultantID = @ConsultantId
			AND QD.IsChargeType = 0


	-- get dealer notes
--	SELECT 
--			RD.DealerID
--			,DM.[Name] as DealerName
--			,QH.DealerNotes
--			,QD.OptionID
--			,''$''+convert(varchar,isnull(QD.QuoteValue,0)) as QuoteValue
--			,QD.QuotationID		
--			,RA.AccessoryID
--	FROM 
--			tblRequestHeader RH
--			JOIN tblRequestDealer RD ON RH.ID = RD.RequestId
--			JOIN tblDealerMaster DM ON DM.ID = RD.DealerID
--			JOIN tblQuotationHeader QH ON RH.ID = QH.RequestID AND RD.DealerID = QH.DealerID		
--			JOIN tblQuotationDetails QD ON QH.ID = QD.QuotationID
--			JOIN tblRequestAccessories RA ON RA.RequestID=@RequestId 
--			JOIN tblAccessoriesMaster AM ON RA.AccessoryID = AM.ID AND AM.ID=QD.ACCIDorCHARGETYPEID
--	WHERE
--			RH.ID = @RequestId
--			AND RH.ConsultantID = @ConsultantId

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetSelectedQuotation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude	
-- Create date: 10 June 2010
-- Description:	Get selected quotation details
-- =============================================
CREATE PROCEDURE [dbo].[SpGetSelectedQuotation]
	@RequestId		int,
	@ConsultantId	int
AS
BEGIN

	SELECT 
			''Additional Accessories'' as Description
			,'''' as OptionID
			,'''' as QuotionID
			,'''' as QuoteValue

	UNION ALL

	-- get accessories info
	SELECT 
			AM.[Name] as Description
			,QD.OptionID
			,QD.QuotationID		
			,''$''+convert(varchar,isnull(QD.QuoteValue,0)) as QuoteValue
			
	FROM 
			tblRequestHeader RH
			JOIN tblRequestDealer RD ON RH.ID = RD.RequestId
			JOIN tblDealerMaster DM ON DM.ID = RD.DealerID
			JOIN tblQuotationHeader QH	ON RH.ID = QH.RequestID 
										AND RD.DealerID = QH.DealerID		
										AND RH.QuotationID = QH.ID		
			JOIN tblQuotationDetails QD ON QH.ID = QD.QuotationID
										AND RH.OptionID = QD.OptionID
			JOIN tblRequestAccessories RA ON RA.RequestID=@RequestId 
			JOIN tblAccessoriesMaster AM ON RA.AccessoryID = AM.ID AND AM.ID=QD.ACCIDorCHARGETYPEID
	WHERE
			RH.ID = @RequestId
			AND RH.ConsultantID = @ConsultantId
			AND QD.IsChargeType = 0

	UNION ALL

	SELECT 
			''Fixed Charges'' as Description
			,'''' as OptionID
			,'''' as QuotionID
			,'''' as QuoteValue

	UNION ALL
	
	-- get fixed charge types info 
	SELECT 
			CT.[Type] as Description
			,QD.OptionID 
			,QD.QuotationID
			,''$''+convert(varchar,isnull(QD.QuoteValue,0)) as QuoteValue
	FROM 
			tblRequestHeader RH
			JOIN tblRequestDealer RD ON RH.ID = RD.RequestId
			JOIN tblDealerMaster DM ON DM.ID = RD.DealerID
			JOIN tblQuotationHeader QH	ON RH.ID = QH.RequestID 
										AND RD.DealerID = QH.DealerID		
										AND RH.QuotationID = QH.ID	
			JOIN tblQuotationDetails QD	ON QH.ID = QD.QuotationID
										AND RH.OptionID = QD.OptionID
			JOIN tblChargesTypesMaster CT ON CT.ID=QD.ACCIDorCHARGETYPEID
	WHERE
			RH.ID = @RequestId
			AND RH.ConsultantID = @ConsultantId
			AND QD.IsChargeType = 1

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllSeriesAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllSeriesAccessories]    --Use Sp as Prefix to Stored Procedure Name
	(
		@SeriesID	int
	)
AS
BEGIN
			SELECT
						SA.ID,AM.[Name]As Accessory ,SA.Specification, SA.IsActive
			FROM
						dbo.tblSeriesAccessories as SA JOIN dbo.tblAccessoriesMaster as AM
						ON SA.AccessoryID=AM.ID 
			WHERE
						SA.SeriesID=@SeriesID
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetActiveAllocatableAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetActiveAllocatableAccessories] --Use Sp as Prefix to Stored Procedure Name
	(
		@SeriesID	int
	)
AS
BEGIN
	
--	DECLARE @COUNT int
--	SET @COUNT=(
--					SELECT     Count(AM.ID)
--					FROM         dbo.tblAccessoriesMaster as AM
--								 JOIN dbo.tblSeriesAccessories as SA ON SA.SeriesID=AM.ID 
--					WHERE
--								AM.IsActive=1 and AM.ID NOT IN (SELECT AccessoryID FROM dbo.tblSeriesAccessories WHERE SeriesID=@SeriesID and AM.IsParameter=0) and SA.SeriesID=@SeriesID AND AM.IsParameter=0
--				)
--
--		if((@COUNT) =0)
--		BEGIN
				SELECT     DISTINCT AM.ID, AM.[Name] as Accessory
					FROM         dbo.tblAccessoriesMaster as AM
								 LEFT OUTER JOIN dbo.tblSeriesAccessories as SA ON SA.SeriesID=AM.ID 
					WHERE
								AM.IsActive=1 and AM.ID NOT IN (SELECT AccessoryID FROM dbo.tblSeriesAccessories WHERE SeriesID=@SeriesID and AM.IsParameter=0)  and AM.IsParameter=0
--		END
--		ELSE
--		BEGIN
--				SELECT     DISTINCT AM.ID, AM.[Name] as Accessory
--					FROM         dbo.tblAccessoriesMaster as AM
--								 JOIN dbo.tblSeriesAccessories as SA ON SA.SeriesID=AM.ID 
--					WHERE
--								AM.IsActive=1 and AM.ID NOT IN (SELECT AccessoryID FROM dbo.tblSeriesAccessories WHERE SeriesID=@SeriesID and AM.IsParameter=0) and AM.IsParameter=0
--		END	
END











' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateSeriesAccessory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateSeriesAccessory]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblSeriesAccessories]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddSeriesAccessory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddSeriesAccessory]
	(
			
			@AccessoryID int,
			@SeriesID int,
			@Specification varchar(500)
	)
AS
BEGIN
			INSERT INTO [dbo].[tblSeriesAccessories]
					   ([AccessoryID]
					   ,[SeriesID]
					   ,[Specification])
				 VALUES
					   (@AccessoryID 
					   ,@SeriesID 
					   ,@Specification)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateSeriesAccessory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateSeriesAccessory]
	(
			@ID int,
			@AccessoryID int,
			@SeriesID int,
			@Specification varchar(500)
	)
AS
BEGIN
			UPDATE [dbo].[tblSeriesAccessories]
			  SET [Specification] = @Specification 
			WHERE ID=@ID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpHandleSeriesAccessories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpHandleSeriesAccessories] 
	(
		@ID int,
		@IsActive bit,
		@DBOperation varchar(50),
		@AccessoryID int,
		@SeriesID int,
		@Specification varchar(500)
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblSeriesAccessories]
					   ([AccessoryID]
					   ,[SeriesID]
					   ,[Specification])
				 VALUES
					   (@AccessoryID 
					   ,@SeriesID 
					   ,@Specification)

		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							
						
							UPDATE [dbo].[tblSeriesAccessories]
							   SET [AccessoryID] = @AccessoryID 
								  ,[SeriesID] = @SeriesID 
								  ,[Specification] = @Specification 
							 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblSeriesAccessories]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpSearchDealersForMake]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 22 May 2010
-- Description:	Search dealers for particular make location wise or city wise
-- =============================================
CREATE PROCEDURE [dbo].[SpSearchDealersForMake]
	@CityIds			varchar(max),
	@LocationIds		varchar(max),
	@SearchCriteria		int,			-- (0 -> city, 1 -> location)
	@MakeID			int 
AS
BEGIN
	
	IF @SearchCriteria = 0   
	  BEGIN
		-- search dealers city wise
		SELECT		DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode
					,DM.Location as LocationID, DM.City as CityID, DM.State as StateID
					,DM.IsActive, LM.Location AS Location,  CM.City AS City
					,SM.State AS State, DM.IsHotDealer
					,(SELECT isnull(Sum(Points),0) FROM tblDealerPoints DP WHERE DP.DealerID = DM.ID) as TotalPoints
		FROM        tblDealerMaster as DM INNER JOIN
					tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
					tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
					tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID
					INNER JOIN tblMakeDealer MD ON DM.ID = MD.DealerID 
		WHERE
					DM.IsActive=1	
					AND MD.MakeID = @MakeID
					AND DM.City IN  (
										SELECT OrderID 
										FROM SplitIds(@CityIds)
									)
		ORDER BY	TotalPoints DESC
	  END
	ELSE IF @SearchCriteria = 1 
	  BEGIN
		-- search dealers location wise
		SELECT		DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode
					,DM.Location as LocationID, DM.City as CityID, DM.State as StateID
					,DM.IsActive, LM.Location AS Location,  CM.City AS City
					,SM.State AS State, DM.IsHotDealer
					,(SELECT isnull(Sum(Points),0) FROM tblDealerPoints DP WHERE DP.DealerID = DM.ID) as TotalPoints
		FROM        tblDealerMaster as DM INNER JOIN
					tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
					tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
					tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID
					INNER JOIN tblMakeDealer MD ON DM.ID = MD.DealerID 
		WHERE
					DM.IsActive=1
					AND MD.MakeID = @MakeID
					AND DM.Location IN  (
										SELECT OrderID 
										FROM SplitIds(@LocationIds)
									)
		ORDER BY	TotalPoints DESC

	  END

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetLocationsInCities]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 22 May 2010
-- Description:	Get locations in particular cities
-- =============================================
create PROCEDURE [dbo].[SpGetLocationsInCities] 
	(
		@CityIds	varchar(max)  -- comma separated string of city ids
	)
AS
BEGIN
	
	SELECT 
				ID, Location
	FROM
				dbo.tblLocationMaster
	WHERE		
				IsActive=1
				AND CityID IN(
								SELECT OrderID 
								FROM SplitIDs(@CityIds)
							)
	
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpSaveQuoteRequest]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 25 May 2010
-- Description:	Save quotation request deatails
-- =============================================
CREATE PROCEDURE [dbo].[SpSaveQuoteRequest]
	@SeriesId			int,
	@ConsultantId		int,
	@ConsultantNotes	varchar(200),
	@DealerIds			varchar(max),   -- comma separated string
	@AccessoryIds		varchar(max),	-- comma separated string
	@XmlDocument		xml
AS
BEGIN
	
--set @XmlDocument = ''<Accessory><AccessoryID>1</AccessoryID><Specification>testing</Specification></Accessory>''

	DECLARE @RequestId	int
	DECLARE @DocHandle int

	BEGIN TRANSACTION
	
		BEGIN TRY
			-- insert request header information
			INSERT INTO tblRequestHeader (SeriesId, ConsultantID, ConsultantNotes, RequestDate)
			VALUES (@SeriesId, @ConsultantId, @ConsultantNotes, getdate())

			-- get last inserted request id
			SELECT @RequestId = SCOPE_IDENTITY()

			-- insert request accessories information
--			INSERT INTO tblRequestAccessories (RequestID, AccessoryID, IsDefault)
--			SELECT @RequestId, OrderID, 1 as AccessoryID 
--			FROM SplitIds(@AccessoryIds)

			-- insert request dealer information
			INSERT INTO tblRequestDealer (RequestId, DealerID, CreatedDate)
			SELECT @RequestId, OrderID, getdate() as DealerID 
			FROM SplitIds(@DealerIds)

			--prepare the XML Document by executing a system stored procedure
			EXEC sp_xml_preparedocument @DocHandle OUTPUT, @XmlDocument

			--insert into table request accessories table
			Insert Into tblRequestAccessories 
					(	
						RequestID
						,AccessoryID
						,AccessorySpecification
					)
			SELECT 
						@RequestId as RequestId
						,ID 
						,Specification
			FROM OPENXML (@DocHandle, ''/Accessoryds/Table1'',2)
			WITH (
						[ID] int
						,[Specification] varchar(200)
				 )


			COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
			ROLLBACK TRANSACTION
		END CATCH

END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpSearchDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 22 May 2010
-- Description:	Search dealers location wise or city wise
-- =============================================
CREATE PROCEDURE [dbo].[SpSearchDealers]
	@CityIds			varchar(max),
	@LocationIds		varchar(max),
	@SearchCriteria		int			-- (0 -> city, 1 -> location)
AS
BEGIN
	
	IF @SearchCriteria = 0   
	  BEGIN
		-- search dealers city wise
		SELECT		DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode
					,DM.Location as LocationID, DM.City as CityID, DM.State as StateID
					,DM.IsActive, LM.Location AS Location,  CM.City AS City
					,SM.State AS State, DM.IsHotDealer
					,(SELECT isnull(Sum(Points),0) FROM tblDealerPoints DP WHERE DP.DealerID = DM.ID) as TotalPoints
		FROM        tblDealerMaster as DM INNER JOIN
					tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
					tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
					tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID 
		WHERE
					DM.IsActive=1	
					AND DM.City IN  (
										SELECT OrderID 
										FROM SplitIds(@CityIds)
									)
		ORDER BY	TotalPoints DESC
	  END
	ELSE IF @SearchCriteria = 1 
	  BEGIN
		-- search dealers location wise
		SELECT		DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode
					,DM.Location as LocationID, DM.City as CityID, DM.State as StateID
					,DM.IsActive, LM.Location AS Location,  CM.City AS City
					,SM.State AS State, DM.IsHotDealer
					,(SELECT isnull(Sum(Points),0) FROM tblDealerPoints DP WHERE DP.DealerID = DM.ID) as TotalPoints
		FROM        tblDealerMaster as DM INNER JOIN
					tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
					tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
					tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID
		WHERE
					DM.IsActive=1
					AND DM.Location IN  (
										SELECT OrderID 
										FROM SplitIds(@LocationIds)
									)
		ORDER BY	TotalPoints DESC

	  END

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleRoleMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleRoleMaster] 
		(
				@ID int,
				@IsActive bit,
				@DBOperation varchar(50),
				@Role varchar(50)
		)
AS
BEGIN
		if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblRoleMaster]
						   ([Role]
						   ,[IsActive])
					 VALUES
						   (@Role
						   ,@IsActive)
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE 
											[dbo].[tblRoleMaster]
								SET 
											[Role] = @Role
										   ,[IsActive] = @IsActive
								 WHERE
											 ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE 
												[dbo].[tblRoleMaster]
									   SET 
												[IsActive] = @IsActive
									 WHERE 
												ID=@ID
						END
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetDealersAndOptions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude	
-- Create date: 08 June 2010
-- Description:	Get dealers and options for particular quote request
-- =============================================
CREATE PROCEDURE [dbo].[SpGetDealersAndOptions]
	@RequestId		int
AS
BEGIN
	select 
			Distinct(QD.OptionID)	
			,QH.DealerID
			,DM.[Name] as DealerName
			,QH.DealerNotes
	from
			tblQuotationHeader QH
			join tblQuotationDetails QD on QH.ID = QD.QuotationID
			join tblDealerMaster DM on DM.ID = QH.DealerID
	where
			QH.RequestID = @RequestId
	order by
		QH.DealerID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetNoOfOptionsOfQuotation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetNoOfOptionsOfQuotation] --Use Sp as Prefix to Stored Procedure Name
	(
		@QuotationID	int
	)
AS
BEGIN
	SELECT 
			COUNT(DISTINCT OptionID)
	FROM 
			tblQuotationDetails
	WHERE 
			QuotationID=@QuotationID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddQuotationDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddQuotationDetails] --Use Sp as Prefix to Stored Procedure Name
(
	 @QuotationID int
	,@RequestDetailID int 
	,@ACCIDorCHARGETYPEID	int	 
	,@OptionID int	
	,@QuoteValue	float	
	,@IsChargeType	bit
)
	
AS
BEGIN
		
		INSERT INTO [tblQuotationDetails]
           ([QuotationID]
           ,[RequestDetailID]
           ,[ACCIDorCHARGETYPEID]
           ,[OptionID]
           ,[QuoteValue]
           ,[IsChargeType])
     VALUES
           (@QuotationID 
           ,@RequestDetailID 
           ,@ACCIDorCHARGETYPEID 
           ,@OptionID 
           ,@QuoteValue
           ,@IsChargeType)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddQuotationHeader]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Prasad Raskar
-- Create date: 28.05.2010
-- Description:	to add quotation header details
-- =============================================
CREATE PROCEDURE [dbo].[SpAddQuotationHeader] --Use Sp as Prefix to Stored Procedure Name
 (
			@RequestID int 
			,@UserID int
           ,@Date datetime 
           ,@DealerNotes varchar(200) 
           ,@EstimatedDeleveryDates datetime 
           ,@ExStock numeric 
           ,@Order numeric 
           ,@ComplianceDate datetime
 )
	
AS
BEGIN


			
		
				DECLARE  @DealerID	int
	
				SET @DealerID=	(
									select DealerID
									FROM	tblDealerUserMaster
									Where	UserID=@UserID
								)

				
		
		INSERT INTO [tblQuotationHeader]
           ([RequestID]
			,[DealerID]
           ,[Date]
           ,[DealerNotes]
           ,[EstimatedDeleveryDates]
           ,[ExStock]
           ,[Order]
           ,[ComplianceDate])
     VALUES
           (@RequestID 
			,@DealerID
           ,@Date  
           ,@DealerNotes  
           ,@EstimatedDeleveryDates  
           ,@ExStock  
           ,@Order  
           ,@ComplianceDate  )
		
	SELECT @@IDENTITY
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetQuotationHeaderDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		prasad raskar
-- Create date: 29.5.10
-- Description:	to get the quotation header details
-- =============================================
CREATE PROCEDURE [dbo].[SpGetQuotationHeaderDetails]  
	(
			@QuotationID	int
	)
AS
BEGIN
	SELECT  
			 Convert(varchar(10),QH.Date,101) as Date,  
			Convert(varchar(10),QH.EstimatedDeleveryDates,101) as EstimatedDeliveryDate, 
			QH.ExStock, QH.[Order],  Convert(varchar(10),QH.ComplianceDate,101) as ComplianceDate
			,UM.[Name] ,QH.DealerNotes as DealerNotes 
			,DM.[Name] as DealerName
			,RH.ConsultantNotes
	FROM 
			tblQuotationHeader as QH JOIN  dbo.tblRequestHeader As RH
			on QH.RequestID=RH.ID JOIN dbo.ACU_UserMaster as UM 
			on RH.ConsultantID=UM.ID
			JOIN tblDealerMaster DM on QH.DealerID = DM.ID
	WHERE 
			QH.ID=@QuotationID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpViewCreatedQuotationsByDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Prasad Raskar
-- Create date: 04.05.10
-- Description:	this procedure returns the quotation headers made by the all users of the perticular dealer
-- =============================================
CREATE PROCEDURE		[dbo].[SpViewCreatedQuotationsByDealer]
	(
			@UserID	int	
	)
AS
BEGIN
		
Select  QH.ID,QH.RequestID,QH.DealerID,Convert(varchar(10),(QH.Date),103) as CreatedDate,QH.DealerNotes,Convert(varchar(10),QH.EstimatedDeleveryDates,103) as EstimatedDeleveryDates,QH.ExStock,QH.[Order],QH.ComplianceDate From 
dbo.tblQuotationHeader as QH  JOIN  tblDealerUserMaster  DUM
on DUM.DealerID=QH.DealerID
where DUM.UserID=@UserID
		
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetQuotationCountForReq]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 June 2010
-- Description:	Get quotation count for a particular request
-- =============================================
CREATE PROCEDURE [dbo].[SpGetQuotationCountForReq]
	@RequestId		int
AS
BEGIN
	SELECT 
			COUNT(ID) AS QuotationCount
	FROM
			tblQuotationHeader
	WHERE
			RequestID = @RequestId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetUserDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Prasad Raskar
-- Create date: 31 May 2010
-- Description:	Sp to get user details
-- =============================================
CREATE PROCEDURE [dbo].[SpGetUserDetails]
(
	@UserID  int 
)
AS
BEGIN
		SELECT 
					UM.ID, UM.Username, UM.Password, UM.[Name], UM.Email, UM.Phone, UM.Address, Convert(varchar(10),UM.UsernameExpiryDate,101) as UsernameExpiryDate, UM.IsActive,RM.ID as RoleID,RM.[Role],DUM.DealerID as DealerID
		FROM
					dbo.ACU_UserMaster as UM  JOIN acu_userroledetails as URD
					ON UM.ID=URD.UserID JOIN dbo.ACU_RoleMaster as RM ON RM.ID=URD.RoleID
					LEFT OUTER JOIN dbo.tblDealerUserMaster as DUM ON  DUM.UserID=UM.ID
					
		WHERE
					UM.ID=@UserID
					
END
----------------------------------------------------






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetConsultantBasicInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetConsultantBasicInfo]
	(
			@ConsultantID	int
	)
AS
BEGIN
	select ''Consultant Name'' as Header , Name as Details From ACU_UserMaster Where ID=@ConsultantID
	UNION
	select ''Email'' as Header, Email as Details From ACU_UserMaster Where ID=@ConsultantID
	UNION
	select ''Contact No'' as Header, Phone as Details From ACU_UserMaster Where ID=@ConsultantID

	END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getAllUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Get all users from the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getAllUsers]
	@isActive	bit = null
AS
BEGIN
	IF @isActive is null
	  BEGIN
			SELECT	DISTINCT UM.[ID]
					  ,UM.[Username]
					  ,UM.[Password]
					  ,UM.[Name]
					  ,UM.[Email]
					  ,UM.[Phone]
					  ,UM.[Address]
					  ,Convert(varchar(10),UM.[UsernameExpiryDate],103) as ExpriryDate
					  ,UM.[IsActive]
					  ,RM.ID as RoleID
					  ,RM.[Role]
			FROM	dbo.ACU_UserMaster as UM  
					JOIN acu_userroledetails as URD	ON UM.ID=URD.UserID 
					JOIN dbo.ACU_RoleMaster as RM ON RM.ID=URD.RoleID
	  END
	ELSE
	  BEGIN
			SELECT	DISTINCT UM.[ID]
				  ,UM.[Username]
				  ,UM.[Password]
				  ,UM.[Name]
				  ,UM.[Email]
				  ,UM.[Phone]
				  ,UM.[Address]
				  ,Convert(varchar(10),UM.[UsernameExpiryDate],103) as ExpriryDate
				  ,UM.[IsActive]
				  ,RM.ID as RoleID
				  ,RM.[Role]
			FROM	dbo.ACU_UserMaster as UM  
					JOIN acu_userroledetails as URD	ON UM.ID=URD.UserID 
					JOIN dbo.ACU_RoleMaster as RM ON RM.ID=URD.RoleID
			WHERE UM.IsActive = @isActive
	  END

END
----------------------------------------------------







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_saveUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Save user information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_saveUser]
	@username				varchar(50),
    @password				varchar(50),
    @name					varchar(100),
    @email					varchar(50),
    @phone					varchar(20),
    @address				varchar(200),
    @usernameExpiryDate		datetime,
    @isActive				bit
AS
BEGIN
	INSERT INTO [dbo].[ACU_UserMaster]
           ([Username]
           ,[Password]
           ,[Name]
           ,[Email]
           ,[Phone]
           ,[Address]
           ,[UsernameExpiryDate])
     VALUES
           (@username,
		    @password,
		    @name,
		    @email,
		    @phone,
		    @address,
		    @usernameExpiryDate)
	
	SELECT SCOPE_IDENTITY()
END
----------------------------------------------------





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllConsultant]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllConsultant] --Use Sp as Prefix to Stored Procedure Name
	
AS
BEGIN
		SELECT UM.ID,UM.Name 
		FROM ACU_userROleDetails as URD 
			JOIN ACU_RoleMaster as RM ON URD.RoleID=Rm.ID
			JOIN ACU_UserMaster as UM ON URD.UserID=UM.ID	
		WHERE RM.Role=''Consultant''
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_updateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Update user information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_updateUser]
	@id						int,
	@Password				varchar(100),
    @name					varchar(100),
    @email					varchar(50),
    @phone					varchar(20),
    @address				varchar(200),
    @usernameExpiryDate		datetime,
    @isActive				bit
AS
BEGIN
	 UPDATE [dbo].[ACU_UserMaster]
     SET   [Name] = @name
           ,[Email] = @email
           ,[Phone] = @phone
           ,[Address] = @address
		,[UsernameExpiryDate] = @usernameExpiryDate
         
     WHERE ID = @id
           
END

----------------------------------------------------

--------------- Functions --------------------------







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddDealerUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddDealerUser] --Use Sp as Prefix to Stored Procedure Name
	(
			@UserID int
           ,@DealerID int
	)
AS
BEGIN
	INSERT INTO [dbo].[tblDealerUserMaster]
           ([UserID]
           ,[DealerID])
     VALUES
           (@UserID 
           ,@DealerID )


	SELECT @@IDENTITY

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateDealerUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateDealerUser] --Use Sp as Prefix to Stored Procedure Name
	(
			@UserID int
           ,@DealerID int
	)
AS
BEGIN

	UPDATE [dbo].[tblDealerUserMaster]
	   SET [DealerID] = @DealerID 
	 WHERE [UserID] = @UserID 

	

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateRequestDealerMapping]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Prasad Raskar
-- Create date: 31.05.10
-- Description:	to update the mapping in the request dealer table that will indicate the current status of the request from the dealer side
--				and ID of the quotation made against that request by perticular dealer			
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateRequestDealerMapping]
	(
		@QuotationID	int,
		@UserID	int,
		@RequestID	int

	)
AS
BEGIN

				DECLARE  @DealerID	int
	
				SET @DealerID=	(
									select DealerID
									FROM	tblDealerUserMaster
									Where	UserID=@UserID
								)

				
				Update 
							tblRequestDealer
				SET	
							QuotationID=@QuotationID	
				Where 
							RequestID=@RequestID AND		
							DealerID=@DealerID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetHotDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 22 May 2010
-- Description:	Retrieve hot dealers
-- =============================================
CREATE PROCEDURE [dbo].[SpGetHotDealers] 
	
AS
BEGIN
	SELECT     DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode,
                      DM.Location as LocationID, DM.City as CityID, DM.State as StateID,  DM.IsActive, LM.Location AS Location,  CM.City AS City, 
                      SM.State AS State
				,DM.IsHotDealer
				,(SELECT isnull(Sum(Points),0) FROM tblDealerPoints DP WHERE DP.DealerID = DM.ID) as TotalPoints
	FROM         tblDealerMaster as DM INNER JOIN
                      tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
                      tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
                      tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID
	WHERE
				DM.IsActive=1
				AND DM.IsHotDealer = 1
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllActiveDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Prasad Raskar
-- Create date: 301.05.10
-- Description:	To get All active dealers
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllActiveDealers] --Use Sp as Prefix to Stored Procedure Name
	
AS
BEGIN
	SELECT		
					ID ,Company as Dealer --,Name as Dealer 
	FROM         tblDealerMaster
	WHERE
				IsActive=1
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddDealer]
	(
		@Name varchar(200)
		,@Company varchar(200)
		,@Email varchar(200)
		,@Fax varchar(50)
		,@Phone varchar(50)
		,@Location int
		,@City int
		,@State int
		,@PCode varchar(50)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblDealerMaster]
					   ([Name]
					   ,[Company]
					   ,[Email]
					   ,[Fax]
					   ,[Phone]
					   ,[Location]
					   ,[City]
					   ,[State]
					   ,[PCode])
				 VALUES
					   (@Name 
					   ,@Company 
					   ,@Email 
					   ,@Fax 
					   ,@Phone 
					   ,@Location 
					   ,@City 
					   ,@State 
					   ,@PCode 
					 )
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateDealer]
	(
		@ID	int
		,@Name varchar(200)
		,@Company varchar(200)
		,@Email varchar(200)
		,@Fax varchar(50)
		,@Phone varchar(50)
		,@Location int
		,@City int
		,@State int
		,@PCode varchar(50)
	)
AS
BEGIN
		UPDATE [dbo].[tblDealerMaster]
		  SET [Name] = @Name 
			  ,[Company] = @Company 
			  ,[Email] = @Email 
			  ,[Fax] = @Fax 
			  ,[Phone] = @Phone 
			  ,[Location] = @Location 
			  ,[City] = @City 
			  ,[State] = @State 
			  ,[PCode] = @PCode 
		WHERE ID=@ID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetActiveAllocatableDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'







-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetActiveAllocatableDealers] --Use Sp as Prefix to Stored Procedure Name
	(
		@MakeID	int
	)
AS
BEGIN
	
--	DECLARE @COUNT int
--	SET @COUNT=(
--					SELECT     Count (DISTINCT( DM.ID))
--					FROM        
--								 dbo.tblMakeDealer as MD ,tblDealerMaster as DM
--					WHERE
--								DM.IsActive=1 and DM.ID NOT IN (SELECT DealerID FROM dbo.tblMakeDealer WHERE MakeID=@MakeID )
--
--					)

--	if((@COUNT) =0)
--	BEGIN
			SELECT     DISTINCT DM.ID, DM.Name as Dealer
			FROM         tblDealerMaster as DM 
						 LEFT OUTER JOIN dbo.tblMakeDealer as MD ON MD.DealerID=DM.ID 
			WHERE
						DM.IsActive=1 and DM.ID NOT IN (SELECT DealerID FROM dbo.tblMakeDealer WHERE MakeID=@MakeID )

--	END
--	ELSE
--	BEGIN
--			SELECT      DISTINCT DM.ID, DM.Name as Dealer
--					FROM         tblDealerMaster as DM 
--								 JOIN dbo.tblMakeDealer as MD ON MD.DealerID=DM.ID 
--					WHERE
--								DM.IsActive=1 and DM.ID NOT IN (SELECT DealerID FROM dbo.tblMakeDealer WHERE MakeID=@MakeID )
--	END	
	
END









' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfDealerExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfDealerExists]
	(
		
		@Name varchar(200)
		,@Company varchar(200)
		,@Email varchar(200)
		,@Fax varchar(50)
		,@Phone varchar(50)
		,@Location int
		,@City int
		,@State int
		,@PCode varchar(50)
	)
AS
BEGIN
	
		SELECT	
					ID
		FROM
					[dbo].[tblDealerMaster]
		WHERE 
					[Name] = @Name 
			  AND	[Company] = @Company 
			  AND	[Email] = @Email 
			  AND	[Fax] = @Fax 
			  AND	[Phone] = @Phone 
			  AND	[Location] = @Location 
			  AND	[City] = @City 
			  AND	[State] = @State 
		
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetActiveDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetActiveDealers] --Use Sp as Prefix to Stored Procedure Name
	
AS
BEGIN
	SELECT     DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode,
                      DM.Location as LocationID, DM.City as CityID, DM.State as StateID,  DM.IsActive, LM.Location AS Location,  CM.City AS City, 
                      SM.State AS State, DM.IsHotDealer
	FROM         tblDealerMaster as DM INNER JOIN
                      tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
                      tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
                      tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID
	WHERE
				DM.IsActive=1
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetDealerDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetDealerDetails]
	(
		@ID int
	)
AS
BEGIN
	
	SELECT     DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode,
                      DM.Location as LocationID, DM.City as CityID, DM.State as StateID,  DM.IsActive, LM.Location AS Location,  CM.City AS City, 
                      SM.State AS State
					  ,DM.IsHotDealer
	FROM         tblDealerMaster as DM INNER JOIN
                      tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
                      tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
                      tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID
	WHERE
				DM.ID=@ID
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllDealers] --Use Sp as Prefix to Stored Procedure Name
	
AS
BEGIN
	SELECT		DM.ID, DM.Name, DM.Company, DM.Email, DM.Fax, DM.Phone, DM.PCode
                ,DM.Location as LocationID, DM.City as CityID, DM.State as StateID
				,DM.IsActive, LM.Location AS Location,  CM.City AS City
                ,SM.State AS State, DM.IsHotDealer
				,(SELECT isnull(Sum(Points),0) FROM tblDealerPoints DP WHERE DP.DealerID = DM.ID) as TotalPoints
	FROM         tblDealerMaster as DM INNER JOIN
                  tblCityMaster as CM ON DM.City = CM.ID INNER JOIN
                  tblLocationMaster as LM ON DM.Location = LM.ID AND CM.ID = LM.CityID INNER JOIN
                  tblStateMaster as SM ON DM.State = SM.ID AND CM.StateID = SM.ID
--	WHERE
--				DM.IsActive=1
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetRequestDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 27 May 2010
-- Description:	Get quote request selected dealers
-- =============================================
CREATE PROCEDURE [dbo].[SpGetRequestDealers]
	@requestId		INT
AS
BEGIN
	SELECT 
			DM.[NAME] AS DealerName
			,DM.Company
			,DM.Email
			,DM.Fax
			,DM.Phone
			,LM.Location
			,CM.City
			,SM.State
	FROM	
			tblRequestHeader RH
			JOIN tblRequestDealer RD ON RH.ID = RD.RequestID
			JOIN tblDealerMaster DM ON DM.ID = RD.DealerID
			JOIN tblLocationMaster LM ON LM.ID = DM.Location
			JOIN tblCityMaster CM ON CM.ID = DM.City
			JOIN tblStateMaster SM ON SM.ID = DM.State
	WHERE
			RH.ID = @requestId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpMarkDealerAsHotOrNormal]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Swati Shirude
-- Create date: 11 June 2010
-- Description:	Mark dealer as hot or normal
-- =============================================
CREATE PROCEDURE [dbo].[SpMarkDealerAsHotOrNormal]
	@DealerID		int,
	@IsHotDealer	bit
AS
BEGIN
	UPDATE 
			tblDealerMaster
	SET
			IsHotDealer = @IsHotDealer
	WHERE 
			ID = @DealerID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_checkPageAccess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Check page access for user/role
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_checkPageAccess]
	@accessFor		int,
	@accessTypeId	int,
	@pageId			int
AS
BEGIN
	SELECT	COUNT(ID) as Cnt
	FROM	ACU_Access
	WHERE	AccessFor = @accessFor
			AND AccessTypeID = @accessTypeId
			AND PageID = @pageId
END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getPageAccess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Get page level access to user/role
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getPageAccess]
	@accessFor		int, 
	@accessTypeId	int
AS
BEGIN
	-- Retrieve all accessible pages
	SELECT		PM.ID, PM.PageName, PM.PageUrl, PM.ParentID, AA.ID AS AccessID
				,PM.IsInternalLink
	FROM		ACU_Access AA
				JOIN ACU_PageMaster PM ON AA.PageID = PM.ID
	WHERE		accessFor=@accessFor 
				AND	accessTypeID = @accessTypeId
				AND AA.IsActive = 1
END
----------------------------------------------------






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_setAccess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




-- =============================================
-- Author:		Swati Shirude
-- Create date: 29 May 2010
-- Description:	Set page and action access to role/user
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_setAccess]
	@accessFor		int, 
	@accessTypeId	int, 
	@XmlDocument xml
AS
BEGIN

		DECLARE @DocHandle int

		--prepare the XML Document by executing a system stored procedure
		EXEC sp_xml_preparedocument @DocHandle OUTPUT, @XmlDocument
		
		DECLARE @XmlData TABLE(XMLPageID int, ActionID int)

--		DECLARE @XmlData TABLE(AccessID int, XMLPageID int, ActionID int)
--
--		INSERT INTO @XmlData(AccessID, XMLPageID,ActionID)
--		SELECT 
--					ID
--					,XMLPageID
--					,ActionID
--		FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
--		WITH (
--					
--					[XMLPageID] int
--					,[ActionID] int
--			 ) 
--		JOIN ACU_ACCESS ON XMLPageID = PageID
--		WHERE AccessFor = @accessFor		


		INSERT INTO @XmlData(XMLPageID,ActionID)
		SELECT 
					XMLPageID
					,ActionID
		FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
		WITH (
					
					[XMLPageID] int
					,[ActionID] int
			 ) 
			
		select * from @XmlData

		-- activate/deactivate existing access information
		UPDATE  ACU_Access
		SET		IsActive = CASE
				WHEN	PageId IN	(
										SELECT DISTINCT(XMLPageID)	
										FROM @XmlData
									)
				THEN 1   -- activate 
				ELSE 0   -- deactivate
				END
		WHERE accessFor = @accessFor

		--insert page ids in access table
		INSERT INTO ACU_Access 
				(	
					PageID
					,AccessFor
					,AccessTypeID
				)
		SELECT	DISTINCT(XMLPageID)
				,@accessFor as AccessFor
				,@accessTypeID as AccessTypeID
		FROM	@XmlData
		WHERE	XMLPageID NOT IN( 
									SELECT	PageID 
									FROM	acu_access 
									WHERE	accessFor=@accessFor 
								)	


		-- activate/deactivate existing access information
--		UPDATE  ACU_ActionAccess
--		SET		IsActive = CASE
--				WHEN	AccessID IN	(
--										SELECT 
--													DISTINCT(AA.ID)
--										FROM		@XmlData localXml
--													JOIN ACU_Access AA ON AA.PageID = localXml.XMLPageID
--										WHERE		AA.accessFor = @accessFor
--									)
--						AND ActionID IN (
--											SELECT 
--														ActionID
--											FROM		@XmlData localXml
--														JOIN ACU_Access AA ON AA.PageID = localXml.XMLPageID
--											WHERE		AA.accessFor = @accessFor
--										)
--				THEN 1   -- activate 
--				ELSE 0   -- deactivate
--				END
		
		UPDATE  ACU_ActionAccess
		SET		IsActive = CASE
				WHEN	ID IN	( 
									SELECT 
												DISTINCT(AC.ID)
									FROM		@XmlData localXml
												JOIN ACU_Access AA ON AA.PageID = localXml.XMLPageID
												JOIN ACU_ActionAccess AC ON AA.ID = AC.AccessID
																		 AND AC.ActionID = localXml.ActionID
									WHERE		AA.accessFor = @accessFor
								)
				THEN 1   -- activate 
				ELSE 0   -- deactivate
				END				


		--insert action ids in action-access table
		INSERT INTO ACU_ActionAccess 
				(	
					AccessID
					,ActionID
				)
		SELECT 
					DISTINCT(AA.ID) AS AccessID
					,localXml.ActionID
		FROM		@XmlData localXml
					JOIN ACU_Access AA ON AA.PageID = localXml.XMLPageID
		WHERE		AA.accessFor = @accessFor
					AND ActionID <> 0
					AND ActionID NOT IN (
											SELECT	DISTINCT(AC.ActionID)
											FROM	ACU_ActionAccess AC
													JOIN ACU_Access AA ON AA.ID = AC.AccessID
													JOIN @XmlData localXml ON AA.PageID = localXml.XMLPageID
											WHERE	AC.IsActive = 1
										)

























--		DECLARE @DocHandle int

		--set @XmlDocument = ''<Selectedds><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>3</XMLPageID><ActionID>13</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>3</XMLPageID><ActionID>14</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>8</XMLPageID><ActionID>0</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>9</XMLPageID><ActionID>0</ActionID></Table1></Selectedds>''

--		-- activate/deactivate existing access information
--		UPDATE  ACU_Access
--		SET		IsActive = CASE
--				WHEN	pageId IN	(
--										SELECT 
--													distinct(XMLPageID)
--										FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
--										WITH (
--													
--													[XMLPageID] int
--											 )	
--									)
--				THEN 1   -- activate 
--				ELSE 0   -- deactivate
--				END
--		WHERE accessFor = @accessFor
--
--
--		--prepare the XML Document by executing a system stored procedure
--		EXEC sp_xml_preparedocument @DocHandle OUTPUT, @XmlDocument
--
--		--insert page ids in access table
--		--Insert Into ACU_Access 
--		--		(	
--		--			PageID
--		--			,AccessFor
--		--			,AccessTypeID
--		--		)
--		SELECT 
--					distinct(XMLPageID)
--					,@accessFor
--					,@accessTypeId
--		FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
--		WITH (
--					
--					[XMLPageID] int
--			 )
--
--
--		-- activate/deactivate existing access information
----		UPDATE  ACU_ActionAccess
----		SET		IsActive = CASE
----				WHEN	ActionID IN	(
----										SELECT 
----													ActionID
----										FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
----										WITH (
----													
----													[XMLPageID] int
----													,[ActionID] int
----											 )
----										--WHERE [XMLPageID] = 
----									)
----				THEN 1   -- activate 
----				ELSE 0   -- deactivate
----				END
----		WHERE accessFor = @accessFor
--	
--
--
--		-- insert action ids in action-access table
--		--INSERT INTO ACU_ActionAccess 
--		--		(	
--		--			AccessID
--		--			,ActionID
--		--		)
--		SELECT 
--					(	SELECT ID AS AccessID 
--						FROM ACU_Access 
--						WHERE PageID=[XMLPageID] AND AccessFor=@accessFor
--					) AS AccessID
--					,ActionID
--		FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
--		WITH (
--					
--					[XMLPageID] int
--					,[ActionID] int
--			 )
--		WHERE ActionID <> 0


END





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_temp_mahesh]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 29 May 2010
-- Description:	Set page and action access to role/user
-- =============================================
CREATE PROCEDURE [dbo].[sp_temp_mahesh]
	@accessFor		int, 
	@accessTypeId	int, 
	@XmlDocument xml
AS
BEGIN
		DECLARE @DocHandle int

		DECLARE @XMLData TABLE (XMLAccessFor int,XMLPageId int, XMLActionID int)

		--set @XmlDocument = ''<Selectedds><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>3</XMLPageID><ActionID>13</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>3</XMLPageID><ActionID>14</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>8</XMLPageID><ActionID>0</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>9</XMLPageID><ActionID>0</ActionID></Table1></Selectedds>''

		-- Step 1. Activate/Deactivate existing Pages for a role or User
		 
		UPDATE  ACU_Access
		SET		IsActive = CASE
				WHEN	pageId IN	(
										SELECT 
													distinct(XMLPageID)
										FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
										WITH (
													
													[XMLPageID] int
											 )	
									)
				THEN 1   -- activate 
				ELSE 0   -- deactivate
				END
		WHERE accessFor = @accessFor

		-- Step 2. Add Pages that does not exisits for that ROLE / USER

		-- Step 3. Update all actions for De-Activated pages -- set to Isactive = false.
		
		-- Step 4. Write Query to get all active pages
		
		DECLARE @TempActionAccess TABLE (AccessID int, PageId int)
		
		INSERT INTO @TempActionAccess
			SELECT	ID as ''ACCESSID'',PageID
			FROM	ACU_Access
			WHERE	AccessFor = 9 AND IsActive = 1

		-- Update Existing Actions
		UPDATE	aa
		SET		aa.IsActive = 1
		FROM	
				ACU_ActionAccess aa
				INNER JOIN @TempActionAccess temp ON temp.AccessID = aa.AccessID
				INNER JOIN @XMLData localxml ON localxml.XMLPageId = temp.PageId

		UPDATE	aa
		SET		aa.IsActive = 0
		FROM	
				ACU_ActionAccess aa
		WHERE
				aa.AccessID in (SELECT ID FROM ACU_Access WHERE AccessFor = 9 AND IsActive = 1)
				AND aa.AccessID NOT IN (SELECT AccessID FROM @TempActionAccess)
				
		-- Read data from XML and join using PageID from Temp. table and update actions accordingly. -- SET to TRUE.
		-- If Action doesnot exist for that page .. INSERT NEW RECORD.


END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_temp_swati]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 29 May 2010
-- Description:	Set page and action access to role/user
-- =============================================
CREATE PROCEDURE [dbo].[sp_temp_swati]
	@accessFor		int, 
	@accessTypeId	int, 
	@XmlDocument xml
AS
BEGIN
		DECLARE @DocHandle int

		DECLARE @XMLData TABLE (XMLAccessFor int,XMLPageId int, XMLActionID int)

		--set @XmlDocument = ''<Selectedds><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>3</XMLPageID><ActionID>13</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>3</XMLPageID><ActionID>14</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>8</XMLPageID><ActionID>0</ActionID></Table1><Table1><XMLAccessFor>9</XMLAccessFor><XMLPageID>9</XMLPageID><ActionID>0</ActionID></Table1></Selectedds>''

		-- Step 1. Activate/Deactivate existing Pages for a role or User
		 
		UPDATE  ACU_Access
		SET		IsActive = CASE
				WHEN	pageId IN	(
										SELECT 
													distinct(XMLPageID)
										FROM OPENXML (@DocHandle, ''/Selectedds/Table1'',2)
										WITH (
													
													[XMLPageID] int
											 )	
									)
				THEN 1   -- activate 
				ELSE 0   -- deactivate
				END
		WHERE accessFor = @accessFor

		-- Step 2. Add Pages that does not exist for that ROLE / USER

		INSERT INTO ACU_Access
				(
					AccessFor
					,PageID
				)
		SELECT 	@accessFor
				,X.XMLPageId
		FROM	@XMLData X
		WHERE	X.XMLPageId	
		NOT IN	(
					SELECT	PageID 
					FROM	ACU_Access
					WHERE	AccessFor = @accessFor
				)
										
		-- Step 3. Update all actions for De-Activated pages -- set to Isactive = false.

		UPDATE	ACU_ActionAccess
		SET		IsActive = 0
		WHERE	AccessID IN (
								SELECT	AccessID
								FROM	ACU_Access
								WHERE	AccessFor = @accessFor
								AND		PageID NOT IN	(
															SELECT XMLPageId
															FROM @XMLData
														)
							)
		
	
		
		-- Step 4. Write Query to get all active pages
		
		DECLARE @TempActionAccess TABLE (AccessID int, PageId int)
		
		INSERT INTO @TempActionAccess
			SELECT	ID as ''ACCESSID'',PageID
			FROM	ACU_Access
			WHERE	AccessFor = @accessFor AND IsActive = 1

		-- Update Existing Actions
		-- activate existing actions
		UPDATE	aa
		SET		aa.IsActive = 1
		FROM	
				ACU_ActionAccess aa
				INNER JOIN @TempActionAccess temp ON temp.AccessID = aa.AccessID
				INNER JOIN @XMLData localxml ON localxml.XMLPageId = temp.PageId

		-- deactivate existing actions
		UPDATE	aa
		SET		aa.IsActive = 0
		FROM	
				ACU_ActionAccess aa
		WHERE
				aa.AccessID in (SELECT ID FROM ACU_Access WHERE AccessFor = @accessFor AND IsActive = 1)
				AND aa.AccessID NOT IN (SELECT AccessID FROM @TempActionAccess)
				
		-- Read data from XML and join using PageID from Temp. table and update actions accordingly. -- SET to TRUE.
		-- If Action does not exist for that page .. INSERT NEW RECORD.
		SELECT	AccessID 
		FROM	@TempActionAccess temp
				JOIN @XMLData localxml ON temp.PageID = localxml.XMLPageId

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_deactivatePageAccessForAll]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Swati Shirude
-- Create date: 04 Jun 2010
-- Description:	Deactivate page access for all users/roles if page is deactivated
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_deactivatePageAccessForAll]
	@pageId			int
AS
BEGIN
	UPDATE	ACU_Access
	SET		IsActive = 0
	WHERE	PageID = @pageId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAccessIDForPage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAccessIDForPage]
		(	
			@PageURL varchar(255),
			@AccessFor int,
			@AccessTypeID	int
		)
AS
BEGIN
			SELECT		isnull(AA.ID,0) AS AccessID
			FROM		ACU_Access AA
						JOIN ACU_PageMaster PM ON AA.PageID = PM.ID
			WHERE		Pm.PageUrl=@PageURL
						AND AA.AccessFor=@AccessFor
						AND AA.AccessTypeID=@AccessTypeID--@AccessFor,@AccessTypeID

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_saveAccessType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Save access type information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_saveAccessType]
	@type			varchar(50)
   ,@priority		int
   ,@description	varchar(200)
AS
BEGIN
	INSERT INTO [dbo].[ACU_AccessTypeMaster]
           ([Type]
           ,[Priority]
           ,[Description])
     VALUES
           (@type
           ,@priority
           ,@description)

	SELECT SCOPE_IDENTITY()

END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getAllAccessTypes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Get all access types information from table
-- =============================================
create PROCEDURE [dbo].[sp_acu_getAllAccessTypes]
	
AS
BEGIN
	SELECT [ID]
      ,[Type]
      ,[Priority]
      ,[Description]
      ,[IsActive]
    FROM [dbo].[ACU_AccessTypeMaster]
END

----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_updateAccessType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Updte access type information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_updateAccessType]
	@id				int
   ,@type			varchar(50)
   ,@priority		int
   ,@description	varchar(200)
AS
BEGIN
	UPDATE [dbo].[ACU_AccessTypeMaster]
    SET		[Type] = @type
           ,[Priority] = @priority
           ,[Description] = @description
     WHERE ID = @id

END

----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllSeries]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllSeries]
	

AS
BEGIN
	SELECT 
				SM.ID, SM.ModelID, SM.Series, SM.IsActive,MM.Model
	FROM
				dbo.tblSeriesMaster as SM JOIN dbo.tblModelMaster as MM
				ON SM.ModelID=MM.ID
--	WHERE
--				SM.IsActive=1
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfModelExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfModelExists]
	(
			@MakeID int,
			@Model varchar(50)
	)
AS
BEGIN
		SELECT	
					ID
		FROM
				   [dbo].[tblModelMaster]
		 WHERE  
				   [MakeID] = @MakeID
				  AND [Model] = @Model
		  
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetSeriesOfModel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 21 May 2010
-- Description:	Retrieve series of a particular model
-- =============================================
create PROCEDURE [dbo].[SpGetSeriesOfModel]
	@modelId		int
AS
BEGIN
	SELECT 
				SM.ID, SM.ModelID, SM.Series, SM.IsActive,MM.Model
	FROM
				dbo.tblSeriesMaster as SM JOIN dbo.tblModelMaster as MM
				ON SM.ModelID=MM.ID
	WHERE
				SM.IsActive=1
				AND SM.ModelID = @modelId
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleModelMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleModelMaster] 
		(
				@ID int,
				@IsActive bit,
				@DBOperation varchar(50),
				@MakeID varchar(50),
				@Model varchar(50)
		)
AS
BEGIN
		if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblModelMaster]
						   ([MakeID]
						   ,[Model]
						   ,[IsActive])
					 VALUES
						   (@MakeID
						   ,@Model
						   ,@IsActive)
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE 
									   [dbo].[tblModelMaster]
							   SET 
									   [MakeID] = @MakeID
									  ,[Model] = @Model
									  ,[IsActive] = @IsActive
							 WHERE 
										ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE 
												[dbo].[tblModelMaster]
									   SET 
												[IsActive] = @IsActive
									 WHERE 
												ID=@ID
						END
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetModels]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetModels] --Use Sp as Prefix to Stored Procedure Name
	@isActive	bit	= null  --optional parameter
AS
BEGIN
	IF @isActive IS NULL
	  BEGIN
			-- retrieve all models
			SELECT 
						MOM.ID, MOM.MakeID,MAM.Make ,MOM.Model, MOM.IsActive
			FROM
						dbo.tblModelMaster as MOM JOIN dbo.tblMakeMaster MAM
						ON MOM.MakeID=MAM.ID
	  END
	ELSE
	  BEGIN
			-- retrieve active/inactive models depending upon @isActive parameter
			SELECT 
						MOM.ID, MOM.MakeID,MAM.Make ,MOM.Model, MOM.IsActive
			FROM
						dbo.tblModelMaster as MOM JOIN dbo.tblMakeMaster MAM
						ON MOM.MakeID=MAM.ID
			WHERE		
						MOM.IsActive = @isActive
	  END
--	WHERE
--				MOM.IsActive=1
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetModelsOfMake]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Swati Shirude
-- Create date: 21 May 2010
-- Description:	Retrieve models of a particular make
-- =============================================
CREATE PROCEDURE [dbo].[SpGetModelsOfMake]
	@makeId		int
AS
BEGIN
	SELECT 
				MOM.ID, MOM.MakeID,MAM.Make ,MOM.Model, MOM.IsActive
	FROM
				dbo.tblModelMaster as MOM JOIN dbo.tblMakeMaster MAM
				ON MOM.MakeID=MAM.ID
	WHERE	
				MOM.IsActive = 1 
				AND MOM.MakeID = @makeId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddModel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddModel]
	(
			@MakeID int,
			@Model varchar(50)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblModelMaster]
						   ([MakeID]
						   ,[Model])
					 VALUES
						   (@MakeID
						   ,@Model)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateModel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateModel]
	(
			@ID int,
			@MakeID int,
			@Model varchar(50)
	)
AS
BEGIN
		UPDATE 
				   [dbo].[tblModelMaster]
		   SET 
				   [MakeID] = @MakeID
				  ,[Model] = @Model
		 WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateModel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateModel]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblModelMaster]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getActionAccess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Get action level access to user/role
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getActionAccess]
	@accessId		int
AS
BEGIN
	-- Retrieve all accessible actions
	SELECT		
				AA.ActionID
				,AM.[Action]
	FROM		
				ACU_ActionAccess AA
				JOIN ACU_ActionMaster AM ON AM.ID = AA.ActionID
	WHERE		
				AA.AccessID=@accessId 
				AND AA.IsActive = 1
END
----------------------------------------------------




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_checkActionAccess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Check action access for user/role
-- =============================================
create PROCEDURE [dbo].[sp_acu_checkActionAccess]
	@accessId		int,
	@actionId		int
AS
BEGIN
	SELECT	COUNT(ID)
	FROM	ACU_ActionAccess
	WHERE	AccessID = @accessId
			AND ActionID = @actionId
END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getAllPages]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Get all pages from table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getAllPages]
	@isActive bit	= null
AS
BEGIN
	IF @isActive is null
		BEGIN
			
			SELECT AM.[ID]
				  ,AM.[PageName]
				  ,AM.[PageUrl]
				  ,AM.[IsActive]
				  ,AM.[ParentID]
				  ,PM.PageName AS ParentPageName
				  ,AM.[IsInternalLink]
			FROM [dbo].[ACU_PageMaster] PM
			RIGHT JOIN [dbo].[ACU_PageMaster] AM
			ON PM.ID = AM.ParentID
			ORDER BY AM.ParentID ASC
		END
	ELSE
		BEGIN
			SELECT AM.[ID]
				  ,AM.[PageName]
				  ,AM.[PageUrl]
				  ,AM.[IsActive]
				  ,AM.[ParentID]
				  ,PM.PageName AS ParentPageName
				  ,AM.[IsInternalLink]
			FROM [dbo].[ACU_PageMaster] PM
			RIGHT JOIN [dbo].[ACU_PageMaster] AM
			ON PM.ID = AM.ParentID
			WHERE AM.IsActive = @isActive
			ORDER BY AM.ParentID ASC
		END

END
----------------------------------------------------







' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_savePage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Save page information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_savePage]
	@pageName			varchar(50)
   ,@pageUrl			varchar(200)
   ,@parentID			int	
   ,@isInternalLink		bit = false
AS
BEGIN
	INSERT INTO [dbo].[ACU_PageMaster]
           ([PageName]
           ,[PageUrl]
		   ,[ParentID]
		   ,[IsInternalLink])
     VALUES
           (@pageName
           ,@pageUrl
		   ,@parentID
		   ,@isInternalLink)	

	SELECT SCOPE_IDENTITY()
END
----------------------------------------------------






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_updatePage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Update page information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_updatePage]
	@id					int
   ,@pageName			varchar(100)
   ,@pageUrl			varchar(255)
   ,@parentID			int
   ,@isInternalLink		bit = false
AS
BEGIN
	UPDATE [dbo].[ACU_PageMaster]
    SET    [PageName] = @pageName
           ,[PageUrl] = @pageUrl
		   ,[ParentID] = @parentID
		   ,[IsInternalLink] = @isInternalLink
    WHERE ID = @id 

END

----------------------------------------------------






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleMakeMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleMakeMaster] 
		(
			@ID int,
			@IsActive bit,
			@DBOperation varchar(50),
			@Make varchar(50)
		)													
AS
BEGIN
		if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblMakeMaster]
						   ([Make]
						   ,[IsActive])
					 VALUES
						   (@Make
						   ,@IsActive)
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE 
										[dbo].[tblMakeMaster]
							 SET 
										[Make] = @Make
										,[IsActive] = @IsActive
							WHERE	
										ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE 
												[dbo].[tblMakeMaster]
									   SET 
												[IsActive] = @IsActive
									 WHERE 
												ID=@ID
						END
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetMakes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetMakes]
	
AS
BEGIN

		SELECT 		
					ID, Make, IsActive
		FROM
					dbo.tblMakeMaster
--		WHERE
--					IsActive=1
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateMake]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateMake]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblMakeMaster]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetActiveMakes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetActiveMakes]
	
AS
BEGIN

		SELECT 		
					ID, Make, IsActive
		FROM
					dbo.tblMakeMaster
		WHERE
					IsActive=1
END




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllMakeDealers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllMakeDealers]    --Use Sp as Prefix to Stored Procedure Name
	(
		@MakeID	int
	)
AS
BEGIN
			SELECT
					MD.ID, MD.MakeID, MD.DealerID, MD.IsActive,DM.Name as Dealer,MM.Make
			FROM
					 [dbo].[tblMakeDealer] as MD JOIN dbo.tblDealerMaster as DM
						ON MD.DealerID=DM.ID JOIN dbo.tblMakeMaster as MM 
						ON MD.MakeID=MM.ID
			WHERE
					MakeID=@MakeID
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddMake]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddMake]
	(
		
			@Make varchar(50)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblMakeMaster]
						   ([Make])
					 VALUES
						   (@Make)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateMake]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateMake]
	(
			@ID int,
			@Make varchar(50)
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblMakeMaster]
		 SET 
					[Make] = @Make
		WHERE	
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfMakeExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfMakeExists]
	(
			
			@Make varchar(50)
	)
AS
BEGIN
		SELECT 
					ID
		FROM
					[dbo].[tblMakeMaster]
		WHERE	  
					[Make] = @Make
		
				
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getAllActions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Get all actions from table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getAllActions]
	@isActive bit	= null
AS
BEGIN
	IF @isActive is null
		BEGIN
			SELECT [ID]
			  ,[Action]
			  ,[Description]
			  ,[IsActive]
		    FROM [dbo].[ACU_ActionMaster]
		END
	ELSE
		BEGIN
			SELECT [ID]
			  ,[Action]
			  ,[Description]
			  ,[IsActive]
		    FROM [dbo].[ACU_ActionMaster]
			WHERE IsActive = @isActive
		END

END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_saveAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Save action information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_saveAction]
	@action			varchar(50)
   ,@description	varchar(200)
AS
BEGIN
	INSERT INTO [dbo].[ACU_ActionMaster]
           ([Action]
           ,[Description])
     VALUES
           (@action
           ,@description)

	SELECT SCOPE_IDENTITY()
END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getPageActions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	To retrieve actions associated with page
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getPageActions]
	@pageId		int,
	@isActive	bit = null
AS
BEGIN
	IF @isActive is null
		BEGIN
			SELECT	PA.PageID
					,PA.ActionID
					,AM.[Action]	
					,PA.IsActive
			FROM	ACU_PageActionDetails PA
					JOIN ACU_ActionMaster AM ON PA.ActionID = AM.id
			WHERE	PageID = @pageId
		END
	ELSE
		BEGIN
			SELECT	PA.PageID
					,PA.ActionID
					,AM.[Action]	
					,PA.IsActive
			FROM	ACU_PageActionDetails PA
					JOIN ACU_ActionMaster AM ON PA.ActionID = AM.id
			WHERE	PA.PageID = @pageId
					AND PA.IsActive = @isActive
		END
END
----------------------------------------------------





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_updateAction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Update page information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_updateAction]
	@id				int
   ,@action			varchar(50)
   ,@description	varchar(200)
AS
BEGIN
	UPDATE [dbo].[ACU_ActionMaster]
    SET    [Action] = @action
           ,[Description] = @description
    WHERE ID = @id 

END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllCity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllCity]
	
AS
BEGIN
	
	SELECT 
				CM.ID, CM.StateID, CM.City, CM.IsActive,SM.State
	FROM
				dbo.tblCityMaster as CM JOIN dbo.tblStateMaster	 as SM 
				ON CM.StateID=SM.ID				
--	WHERE
--				CM.IsActive=1
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllStates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllStates]
	
AS
BEGIN
	
	SELECT 
				DISTINCT ID, State, IsActive
	FROM
				dbo.tblStateMaster		
--	WHERE
--				IsActive=1	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfStateExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfStateExists]
	(
		@State varchar(100)
	)
AS
BEGIN

	SELECT 
			ID
	FROM
			dbo.tblStateMaster
	WHERE
			State=@State
			
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllActiveStates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllActiveStates]
	
AS
BEGIN
	
	SELECT 
				DISTINCT ID, State, IsActive
	FROM
				dbo.tblStateMaster		
	WHERE
				IsActive=1	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateState]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblStateMaster]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateState]
	(
			@ID int,
			@State varchar(50)
	)
AS
BEGIN
		UPDATE [dbo].[tblStateMaster]
		  SET [State] = @State
		WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddState]
	(
			
			@State varchar(50)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblStateMaster]
					   ([State])
				 VALUES
					   (@State)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleStateMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleStateMaster] 
	(
			@ID int,
			@State varchar(50),
			@IsActive bit,
			@DBOperation varchar(50)
	)
AS
BEGIN
		if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblStateMaster]
					   ([State]
					   ,[IsActive])
				 VALUES
					   (@State
					   ,@IsActive)
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE [dbo].[tblStateMaster]
							   SET [State] = @State
									,[IsActive] = @IsActive
							 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblStateMaster]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
				
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_savePageActionDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Save user-role mapping
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_savePageActionDetails]
	@pageId		int
   ,@actionIds	varchar(max)   
AS
BEGIN
	-- activate existing inactive mapping which is in updated active mapping ids
	UPDATE  ACU_PageActionDetails
	SET		IsActive = 1
	WHERE	pageID = @pageId
			AND actionID IN (
								SELECT OrderId as ActionId
								FROM   SplitIDs(@actionIds)
							)

	-- deactivate existing mapping which is not in updated active mapping ids
	UPDATE  ACU_PageActionDetails
	SET		IsActive = 0
	WHERE	pageID = @pageId
			AND actionID NOT IN (
									SELECT OrderId AS ActionId
									FROM   SplitIDs(@actionIds)
								)

	-- insert the active mapping which is not present in the mapping table	
	DECLARE @Temp1 table(ID int,PageID int,Status bit)

	INSERT INTO	@Temp1
	SELECT 		@pageID,ActionId,1
	FROM 		(
					SELECT	OrderId AS ActionId
					FROM	SplitIDs(@actionIds)) AS Temp
	WHERE		Temp.ActionId not IN (
								SELECT	APD.ActionID
								FROM 	dbo.ACU_PageActionDetails AS APD
								WHERE 	APD.PageID	=@pageId
							)

	INSERT INTO ACU_PageActionDetails
				(pageID,actionID,IsActive)
	SELECT		ID,pageID,Status 
	FROM 		@Temp1

END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_saveRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Save role information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_saveRole]
	@role			varchar(50),
	@description	varchar(200)
AS
BEGIN
	INSERT INTO [dbo].[ACU_RoleMaster]
           ([Role]
           ,[Description]
           )
    VALUES
           (@role
           ,@description
           )

	SELECT SCOPE_IDENTITY()
END
----------------------------------------------------




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getAllRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Retrieve roles information from the table
--				(Retrieve active roles if @isActive is set to true)
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getAllRoles]
	@isActive bit = null
AS
BEGIN
	IF @isActive is null
	  BEGIN
		SELECT [ID]
			  ,[Role]
			  ,[Description]
			  ,[IsActive]
		FROM [dbo].[ACU_RoleMaster]
	  END
	ELSE
	  BEGIN
		SELECT [ID]
			  ,[Role]
			  ,[Description]
			  ,[IsActive]
		FROM [dbo].[ACU_RoleMaster]
		WHERE IsActive = @isActive
	  END
END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_updateRole]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Update role information in the table
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_updateRole]
	@id				int,
	@role			varchar(50),
	@description	varchar(200)
AS
BEGIN
	UPDATE [dbo].[ACU_RoleMaster]
	SET	  [Role] = @role
		  ,[Description] = @description
	WHERE ID = @id
END
----------------------------------------------------



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getActiveAllocatableRolesForUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

---------------------------
-- Procedure not in use
---------------------------

-- =============================================
-- Author:		Prasad Raskar
-- Create date: 15 May 2010
-- Description:	Get all active allocatable roles for the perticular user
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getActiveAllocatableRolesForUser]
	@userId		int
AS
BEGIN
	
	DECLARE @COUNT int
	SET @COUNT=(
					SELECT	COUNT(URD.ID)
					FROM	ACU_UserRoleDetails as URD  JOIN dbo.ACU_RoleMaster As RM 
							ON URD.RoleID=RM.ID
					WHERE	RM.isActive = 1 AND RM.ID NOT IN (SELECT ACU_UserRoleDetails.RoleID FROM  ACU_UserRoleDetails WHERE ACU_UserRoleDetails.UserID=@USerID)

				)
		if((@COUNT) =0)
		BEGIN
				SELECT	DISTINCT RM.ID,RM.[Role]
					FROM	ACU_UserRoleDetails as URD  JOIN dbo.ACU_RoleMaster As RM 
							ON URD.RoleID=RM.ID
					WHERE	RM.isActive = 1 AND RM.ID NOT IN (SELECT ACU_UserRoleDetails.RoleID FROM  ACU_UserRoleDetails WHERE ACU_UserRoleDetails.UserID=@USerID)	
		END
		ELSE
		BEGIN
				SELECT	DISTINCT RM.ID,RM.[Role]
					FROM	ACU_UserRoleDetails as URD RIGHT OUTER JOIN dbo.ACU_RoleMaster As RM 
							ON URD.RoleID=RM.ID
					WHERE	RM.isActive = 1 AND RM.ID NOT IN (SELECT ACU_UserRoleDetails.RoleID FROM  ACU_UserRoleDetails WHERE ACU_UserRoleDetails.UserID=@USerID)
		END
END
----------------------------------------------------





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getRolesOfUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		Prasad Raskar
-- Create date: 15 May 2010
-- Description:	Get roles of user
-- =============================================



CREATE PROCEDURE [dbo].[sp_acu_getRolesOfUser]
(
	@userId		int
)
AS
BEGIN
	SELECT	URD.ID,URD.RoleID,RM.[Role],URD.IsActive
	FROM	ACU_UserRoleDetails as URD JOIN dbo.ACU_RoleMaster As RM 
			ON URD.RoleID=RM.ID
	WHERE	URD.userId = @userId
			--AND isActive = 1
END
----------------------------------------------------




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_GetUserDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Check action access for user/role
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_GetUserDetails]
(
	@UserID  int 
)
AS
BEGIN
		SELECT 
					UM.ID, UM.Username, UM.Password, UM.[Name], UM.Email, UM.Phone, UM.Address, Convert(varchar(10),UM.UsernameExpiryDate,101) as UsernameExpiryDate, UM.IsActive,RM.ID as RoleID,RM.[Role]
		FROM
					dbo.ACU_UserMaster as UM  JOIN acu_userroledetails as URD
					ON UM.ID=URD.UserID JOIN dbo.ACU_RoleMaster as RM ON RM.ID=URD.RoleID
		WHERE
					UM.ID=@UserID
					
END
----------------------------------------------------





' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpLogin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Prasad Raskar
-- Create date: 19.05.10
-- Description:	To Validate user log in
-- =============================================
CREATE PROCEDURE [dbo].[SpLogin]
	(
		@UserName varchar(200),
		@Password varchar(200)
	)
AS
BEGIN
		SELECT     UM.ID,UM.Username, UM.Name,UM.Password, COnvert(varchar(10),UM.UsernameExpiryDate,101)as ExpiryDate ,RM.Role, RM.ID as RoleID
		FROM         ACU_UserMaster as UM INNER JOIN
							  ACU_UserRoleDetails as URD ON UM.ID = URD.UserID INNER JOIN
							  ACU_RoleMaster as RM  ON URD.RoleID = RM.ID
		WHERE
					UM.UserName=@UserName
					AND	UM.Password=@Password

END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spShortListQuotation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spShortListQuotation]
	(
			@QuotationId int,
			@OptionID int,
			@Id int	
			
	)
AS
BEGIN
		UPDATE [dbo].[tblRequestHeader]
		  SET [QuotationID] = @QuotationID 
		  ,[optionId] = @optionId 
			,[ShortListedDate]=getutcdate()
		 WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetConsultantNoteForrequest]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetConsultantNoteForrequest]  --Use Sp as Prefix to Stored Procedure Name
	(
		@requestId INT
	)
AS
BEGIN
	
			SELECT 
					RH.ConsultantNotes 
					
			FROM	
					tblRequestHeader  as RH
					
			WHERE
					RH.ID = @requestId
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllLocation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllLocation]
AS
BEGIN
	
	SELECT 
				LM.ID, LM.CityID, LM.Location, LM.IsActive,CM.City
	FROM	
				dbo.tblLocationMaster as LM JOIN dbo.tblCityMaster as CM
				ON	LM.CityID=CM.ID
--	WHERE
--				LM.IsActive=1		
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfCityExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfCityExists]
	(
		@StateID int,
		@City varchar(100)
	)
AS
BEGIN

	SELECT 
			ID, StateID, City, IsActive
	FROM
			dbo.tblCityMaster
	WHERE
			StateID=@StateID AND
			City=@CIty
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetCitiesOfState]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetCitiesOfState]
	(
		@StateId	int
	)
AS
BEGIN
	
	SELECT 
				ID,  City, IsActive
	FROM
				dbo.tblCityMaster	
	WHERE
				StateID=@StateId AND
				IsActive=1
				

	
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateCity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateCity]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblCityMaster]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddCity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddCity]
	(
		
		@StateID int,
		@City varchar(50)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblCityMaster]
						   ([StateID]
						   ,[City])
					 VALUES
						   (@StateID
						   ,@City)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateCity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateCity]
	(
		@ID	int,
		@StateID int,
		@City varchar(50)
	)
AS
BEGIN
		UPDATE 
				[dbo].[tblCityMaster]
		SET 
				[StateID] = @StateID
				,[City] = @City
		WHERE 
				ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleCityMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleCityMaster] 
	(
		@ID int,
		@StateID int,
		@City varchar(50),
		@IsActive bit,
		@DBOperation varchar(50)
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblCityMaster]
						   ([StateID]
						   ,[City])
					 VALUES
						   (@StateID
						   ,@City)
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE 
										[dbo].[tblCityMaster]
							SET 
										[StateID] = @StateID
										,[City] = @City
										,[IsActive] = @IsActive
							WHERE 
										ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE 
												[dbo].[tblCityMaster]
									SET 
												[IsActive]=@IsActive
									WHERE 
												ID=@ID
						END
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetAllChargeType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAllChargeType]
	
AS
BEGIN

	SELECT 
				ID, [Type], IsActive
	FROM
					dbo.tblChargesTypesMaster
	ORDER BY SortNo ASC
--	WHERE
--				IsActive=1
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_updateRoleOfUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Update role of user
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_updateRoleOfUser]
	@userId		int,
	@roleId		int
AS
BEGIN
	UPDATE	ACU_UserRoleDetails
	SET		roleID = @roleId
	WHERE	userId = @userId
			AND isActive = 1
END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_saveUserRoleDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Swati Shirude
-- Create date: 07 May 2010
-- Description:	Save user role details
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_saveUserRoleDetails]
	@userId		int,
	@roleId		int,
	@isActive	bit = null
AS
BEGIN
	INSERT INTO ACU_UserRoleDetails
				(UserID,
				 RoleID)
	VALUES		(@userId,
				 @roleId)

	SELECT @@IDENTITY
END
----------------------------------------------------




' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getRoleOfUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	Get role of user
-- =============================================
create PROCEDURE [dbo].[sp_acu_getRoleOfUser]
	@userId		int
AS
BEGIN
	SELECT	roleID 
	FROM	ACU_UserRoleDetails
	WHERE	userId = @userId
			AND isActive = 1
END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_acu_getRoleUsers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Swati Shirude
-- Create date: 10 May 2010
-- Description:	To retrieve users associated with role
-- =============================================
CREATE PROCEDURE [dbo].[sp_acu_getRoleUsers]
	@roleId		int
AS
BEGIN
	SELECT	UserID	
	FROM	ACU_UserRoleDetails
	WHERE	RoleID = @roleId
			AND IsActive = 1
END
----------------------------------------------------


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateLocation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateLocation]
	(
			@ID int,
			@CityID		int,
			@Location	varchar(100)
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblLocationMaster]
		   SET 
					[CityID] = @CityID
					,[Location] = @Location
		 WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateLocation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateLocation]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblLocationMaster]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddLocation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddLocation]
	(
			@CityID		int,
			@Location	varchar(100)
	)
AS
BEGIN
		INSERT INTO [dbo].[tblLocationMaster]
						   ([CityID]
						   ,[Location])
					 VALUES
						   (@CityID
						   ,@Location)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HandleLocationMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[HandleLocationMaster] 
	(
			@ID int,
			@IsActive bit,
			@DBOperation varchar(50),
			@CityID		int,
			@Location	varchar(100)
	)
AS
BEGIN
		if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblLocationMaster]
						   ([CityID]
						   ,[Location]
						   ,[IsActive])
					 VALUES
						   (@CityID
						   ,@Location
						   ,@IsActive)
		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE 
										[dbo].[tblLocationMaster]
							   SET 
										[CityID] = @CityID
										,[Location] = @Location
										,[IsActive] = @IsActive
							 WHERE 
										ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE 
												[dbo].[tblLocationMaster]
									   SET 
												[IsActive] = @IsActive
									 WHERE 
												ID=@ID
						END
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpGetLocationsOfCity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpGetLocationsOfCity] 
	(
		@CityId	int
	)
AS
BEGIN
	
	SELECT 
				ID, Location
	FROM
				dbo.tblLocationMaster
	WHERE
				CityID=@CItyId AND
				IsActive=1

	
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpCheckIfLocationExists]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpCheckIfLocationExists]
	(
		@CityID int,
		@Location varchar(100)
	)
AS
BEGIN

	SELECT 
			ID
	FROM
			dbo.tblLocationMaster
	WHERE
			CityID=@CityID AND
			Location=@Location
			
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpSaveReqAdditionalAcc]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Swati Shirude
-- Create date: 25 May 2010
-- Description:	Save request additional accessories
-- =============================================
CREATE PROCEDURE [dbo].[SpSaveReqAdditionalAcc]
	@requestId				int,
	@accessoryId			int,
	@specification			varchar(200)
AS
BEGIN
	INSERT INTO tblRequestAccessories (RequestID, AccessoryID, AccessorySpecification)
	VALUES (@requestId, @accessoryId, @specification)
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddMakeDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddMakeDealer]
	(
			
			@MakeID  int,
			@DealerID int
	)
AS
BEGIN
		INSERT INTO [dbo].[tblMakeDealer]
						   ([MakeID]
						   ,[DealerID])
					 VALUES
						   (@MakeID 
						   ,@DealerID )
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpUpdateMakeDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateMakeDealer]
	(
			@ID int,
			@MakeID  int,
			@DealerID int
	)
AS
BEGIN
		UPDATE [dbo].[tblMakeDealer]
		SET		[MakeID] = @MakeID 
			  ,[DealerID] = @DealerID 
		WHERE ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpActivateInactivateMakeDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpActivateInactivateMakeDealer]
	(
		@ID	int,
		@IsActive bit
	)
AS
BEGIN
		UPDATE 
					[dbo].[tblMakeDealer]
		SET 
					[IsActive]=@IsActive
		WHERE 
					ID=@ID
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpHandleMakeDealer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpHandleMakeDealer] 
	(
		@ID int,
		@IsActive bit,
		@DBOperation varchar(50),
		@MakeID  int,
		@DealerID int
		
	)
AS
BEGIN
	if(@DBOperation=''INSERT'')
		BEGIN
				INSERT INTO [dbo].[tblMakeDealer]
						   ([MakeID]
						   ,[DealerID])
					 VALUES
						   (@MakeID 
						   ,@DealerID )


		END
		ELSE	
				IF(@DBOperation=''UPDATE'')
				BEGIN
							UPDATE [dbo].[tblMakeDealer]
							   SET [MakeID] = @MakeID 
								  ,[DealerID] = @DealerID 
							 WHERE ID=@ID
				END
				ELSE
						IF(@DBOperation=''ACTIVE'')
						BEGIN
									UPDATE [dbo].[tblMakeDealer]
									   SET [IsActive] = @IsActive
									 WHERE ID=@ID
						END
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpHandleAgingOfDeal]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpHandleAgingOfDeal] --Use Sp as Prefix to Stored Procedure Name
		(
			@ActualPointModificationDate	datetime,
			@DateToCompare	datetime,
			@Points	int
		)
AS
BEGIN


		UPDATE	
					[dbo].[tblDealerPoints]
		SET 
					[Points] = [Points ]- @Points
					,[ModifiedDate] = Convert(datetime,@ActualPointModificationDate )
		WHERE 
					Convert(varchar(10),convert(datetime,[ModifiedDate]),101) =Convert(varchar(10),Convert(datetime,@DateToCompare) ,101)
					AND ((Points-@Points)>0)
			
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpAddPointDealerAfterShortListing]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SpAddPointDealerAfterShortListing] --Use Sp as Prefix to Stored Procedure Name
	(
		@RequestID	int,
		@DealerID	int,
		@Points		int
	)
AS
BEGIN
		INSERT INTO [dbo].[tblDealerPoints]
           (
			[RequestID]
           ,[DealerID]
           ,[Points]
           ,[CreatedDate]
			,[ModifiedDate]
		  )
		 VALUES
			   (@RequestID 
				,@DealerID 
				,@Points 
				,getutcdate()
				,getutcdate()
				 )
END


' 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblRequestHeader_tblStatusMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblRequestHeader]'))
ALTER TABLE [dbo].[tblRequestHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblRequestHeader_tblStatusMaster] FOREIGN KEY([Status])
REFERENCES [dbo].[tblStatusMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Series_Accessories_Tbl_AccessoriesMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSeriesAccessories]'))
ALTER TABLE [dbo].[tblSeriesAccessories]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Series_Accessories_Tbl_AccessoriesMaster] FOREIGN KEY([AccessoryID])
REFERENCES [dbo].[tblAccessoriesMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_LockDetails_Tbl_LockMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_LockDetails]'))
ALTER TABLE [dbo].[Tbl_LockDetails]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_LockDetails_Tbl_LockMaster] FOREIGN KEY([LockId])
REFERENCES [dbo].[Tbl_LockMaster] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_UnlockDetails_Tbl_UnlockMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tbl_UnlockDetails]'))
ALTER TABLE [dbo].[Tbl_UnlockDetails]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_UnlockDetails_Tbl_UnlockMaster] FOREIGN KEY([UnlockId])
REFERENCES [dbo].[Tbl_UnlockMaster] ([id])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_UserMaster_Tbl_RoleMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserMaster]'))
ALTER TABLE [dbo].[tblUserMaster]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_UserMaster_Tbl_RoleMaster] FOREIGN KEY([RoleID])
REFERENCES [dbo].[tblRoleMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_QuotationDetails_Tbl_QuotationHeader]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuotationDetails]'))
ALTER TABLE [dbo].[tblQuotationDetails]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_QuotationDetails_Tbl_QuotationHeader] FOREIGN KEY([QuotationID])
REFERENCES [dbo].[tblQuotationHeader] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDealerUserMaster_ACU_UserMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDealerUserMaster]'))
ALTER TABLE [dbo].[tblDealerUserMaster]  WITH CHECK ADD  CONSTRAINT [FK_tblDealerUserMaster_ACU_UserMaster] FOREIGN KEY([UserID])
REFERENCES [dbo].[ACU_UserMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDealerUserMaster_tblDealerMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDealerUserMaster]'))
ALTER TABLE [dbo].[tblDealerUserMaster]  WITH CHECK ADD  CONSTRAINT [FK_tblDealerUserMaster_tblDealerMaster] FOREIGN KEY([DealerID])
REFERENCES [dbo].[tblDealerMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblMakeDealer_tblDealerMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblMakeDealer]'))
ALTER TABLE [dbo].[tblMakeDealer]  WITH CHECK ADD  CONSTRAINT [FK_tblMakeDealer_tblDealerMaster] FOREIGN KEY([DealerID])
REFERENCES [dbo].[tblDealerMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblMakeDealer_tblMakeMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblMakeDealer]'))
ALTER TABLE [dbo].[tblMakeDealer]  WITH CHECK ADD  CONSTRAINT [FK_tblMakeDealer_tblMakeMaster] FOREIGN KEY([MakeID])
REFERENCES [dbo].[tblMakeMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Request_Dealer_Tbl_DealerMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblRequestDealer]'))
ALTER TABLE [dbo].[tblRequestDealer]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Request_Dealer_Tbl_DealerMaster] FOREIGN KEY([DealerID])
REFERENCES [dbo].[tblDealerMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_Request_Dealer_Tbl_RequestHeader]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblRequestDealer]'))
ALTER TABLE [dbo].[tblRequestDealer]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Request_Dealer_Tbl_RequestHeader] FOREIGN KEY([RequestId])
REFERENCES [dbo].[tblRequestHeader] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDealerPoints_tblDealerMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDealerPoints]'))
ALTER TABLE [dbo].[tblDealerPoints]  WITH CHECK ADD  CONSTRAINT [FK_tblDealerPoints_tblDealerMaster] FOREIGN KEY([DealerID])
REFERENCES [dbo].[tblDealerMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDealerPoints_tblDealerPoints]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDealerPoints]'))
ALTER TABLE [dbo].[tblDealerPoints]  WITH CHECK ADD  CONSTRAINT [FK_tblDealerPoints_tblDealerPoints] FOREIGN KEY([RequestID])
REFERENCES [dbo].[tblRequestHeader] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ACU_ActionAccess_ACU_Access]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACU_ActionAccess]'))
ALTER TABLE [dbo].[ACU_ActionAccess]  WITH CHECK ADD  CONSTRAINT [FK_ACU_ActionAccess_ACU_Access] FOREIGN KEY([AccessID])
REFERENCES [dbo].[ACU_Access] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ACU_ActionAccess_ACU_ActionMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACU_ActionAccess]'))
ALTER TABLE [dbo].[ACU_ActionAccess]  WITH CHECK ADD  CONSTRAINT [FK_ACU_ActionAccess_ACU_ActionMaster] FOREIGN KEY([ActionID])
REFERENCES [dbo].[ACU_ActionMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ACU_Access_ACU_PageMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[ACU_Access]'))
ALTER TABLE [dbo].[ACU_Access]  WITH CHECK ADD  CONSTRAINT [FK_ACU_Access_ACU_PageMaster] FOREIGN KEY([PageID])
REFERENCES [dbo].[ACU_PageMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_model_make]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblModelMaster]'))
ALTER TABLE [dbo].[tblModelMaster]  WITH CHECK ADD  CONSTRAINT [FK_model_make] FOREIGN KEY([MakeID])
REFERENCES [dbo].[tblMakeMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCityMaster_tblStateMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCityMaster]'))
ALTER TABLE [dbo].[tblCityMaster]  WITH CHECK ADD  CONSTRAINT [FK_tblCityMaster_tblStateMaster] FOREIGN KEY([StateID])
REFERENCES [dbo].[tblStateMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDealerMaster_tblCityMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDealerMaster]'))
ALTER TABLE [dbo].[tblDealerMaster]  WITH CHECK ADD  CONSTRAINT [FK_tblDealerMaster_tblCityMaster] FOREIGN KEY([City])
REFERENCES [dbo].[tblCityMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDealerMaster_tblLocationMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDealerMaster]'))
ALTER TABLE [dbo].[tblDealerMaster]  WITH CHECK ADD  CONSTRAINT [FK_tblDealerMaster_tblLocationMaster] FOREIGN KEY([Location])
REFERENCES [dbo].[tblLocationMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblDealerMaster_tblStateMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblDealerMaster]'))
ALTER TABLE [dbo].[tblDealerMaster]  WITH CHECK ADD  CONSTRAINT [FK_tblDealerMaster_tblStateMaster] FOREIGN KEY([State])
REFERENCES [dbo].[tblStateMaster] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_QuotationHeader_Tbl_Request_Dealer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuotationHeader]'))
ALTER TABLE [dbo].[tblQuotationHeader]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_QuotationHeader_Tbl_Request_Dealer] FOREIGN KEY([RequestID])
REFERENCES [dbo].[tblRequestHeader] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tbl_RequestDetails_Tbl_Request_Dealer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblRequestAccessories]'))
ALTER TABLE [dbo].[tblRequestAccessories]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_RequestDetails_Tbl_Request_Dealer] FOREIGN KEY([RequestID])
REFERENCES [dbo].[tblRequestHeader] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLocationMaster_tblCityMaster]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLocationMaster]'))
ALTER TABLE [dbo].[tblLocationMaster]  WITH CHECK ADD  CONSTRAINT [FK_tblLocationMaster_tblCityMaster] FOREIGN KEY([CityID])
REFERENCES [dbo].[tblCityMaster] ([ID])
