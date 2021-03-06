SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SearchWordIndexMember_Persist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SearchWordIndexMember_Persist]
GO





-- =============================================
-- Author:		Mark Garner
-- Create date: 7-31-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SearchWordIndexMember_Persist] 
	-- Add the parameters for the stored procedure here
	@SearchWordIndexMemberID int = null, 
	@SearchWordID int,
	@TopicID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @SearchWordIndexMemberID is null
	BEGIN
		INSERT INTO SearchWordIndexMember
		(SearchWordID,
		TopicID)
		VALUES
		(@SearchWordID,
		@TopicID)

		SELECT @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE SearchWordIndexMember
		SET SearchWordID = @SearchWordID,
			TopicID = @TopicID
		WHERE SearchWordIndexMemberID = @SearchWordIndexMemberID
	END
END





GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

