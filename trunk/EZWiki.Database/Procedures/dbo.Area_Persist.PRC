SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Area_Persist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Area_Persist]
GO



-- =============================================
-- Author:		Mark Garner
-- Create date: 8-11-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Area_Persist] 
	-- Add the parameters for the stored procedure here
	@AreaID int = null, 
	@Name varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @AreaID is null
	BEGIN
		INSERT INTO Area
		(Name)
		VALUES
		(@Name)

		SELECT @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE Area
		SET Name = @Name
		WHERE AreaID = @AreaID
	END
END



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

