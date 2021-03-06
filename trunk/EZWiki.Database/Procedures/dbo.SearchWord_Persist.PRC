SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SearchWord_Persist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SearchWord_Persist]
GO


-- =============================================
-- Author:		Mark Garner
-- Create date: 7-31-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SearchWord_Persist] 
	-- Add the parameters for the stored procedure here
	@SearchWordID int = null, 
	@Word varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @SearchWordID is null
	BEGIN
		INSERT INTO SearchWord
		(Word)
		VALUES
		(@Word)

		SELECT @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE SearchWord
		SET Word = @Word
		WHERE SearchWordID = @SearchWordID
	END
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

