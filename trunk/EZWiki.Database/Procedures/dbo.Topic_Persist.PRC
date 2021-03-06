SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Topic_Persist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Topic_Persist]
GO








-- =============================================
-- Author:		Mark Garner
-- Create date: 7-31-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Topic_Persist] 
	-- Add the parameters for the stored procedure here
	@TopicID int = null, 
	@Title varchar(50),
	@Content text,
	@IsLocked bit,
	@Version int,
	@AreaID int,
	@IsCurrent bit,
	@CreatedDate smalldatetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @TopicID is null
	BEGIN
		INSERT INTO Topic
		(Title,
		Content,
		IsLocked,
		Version,
		AreaID,
		IsCurrent,
		CreatedDate)
		VALUES
		(@Title,
		@Content,
		@IsLocked,
		@Version,
		@AreaID,
		@IsCurrent,
		@CreatedDate)

		SELECT @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE Topic
		SET Title = @Title,
			Content = @Content,
			IsLocked = @IsLocked,
			Version = @Version,
			AreaID = @AreaID,
			IsCurrent = @IsCurrent,
			CreatedDate = @CreatedDate
		WHERE TopicID = @TopicID
	END
END








GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

