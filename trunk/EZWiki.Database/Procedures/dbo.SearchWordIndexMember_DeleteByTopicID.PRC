SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SearchWordIndexMember_DeleteByTopicID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SearchWordIndexMember_DeleteByTopicID]
GO



-- =============================================
-- Author:		Mark Garner
-- Create date: 7-31-07
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SearchWordIndexMember_DeleteByTopicID] 
	-- Add the parameters for the stored procedure here
	@TopicID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM SearchWordIndexMember
	WHERE TopicID = @TopicID 
END



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

