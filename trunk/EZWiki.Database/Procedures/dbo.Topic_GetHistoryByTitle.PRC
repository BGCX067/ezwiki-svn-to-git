SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Topic_GetHistoryByTitle]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Topic_GetHistoryByTitle]
GO









-- =============================================
-- Author:		Mark Garner
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Topic_GetHistoryByTitle] 
	-- Add the parameters for the stored procedure here
	@Title varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TopicID, Title, Content, IsLocked, Version, AreaID, IsCurrent, CreatedDate
	FROM Topic
	WHERE Title = @Title
	ORDER BY Version DESC
END









GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

