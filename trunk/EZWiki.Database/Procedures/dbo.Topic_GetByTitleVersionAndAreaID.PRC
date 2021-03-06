SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Topic_GetByTitleVersionAndAreaID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Topic_GetByTitleVersionAndAreaID]
GO









-- =============================================
-- Author:		Mark Garner
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Topic_GetByTitleVersionAndAreaID] 
	-- Add the parameters for the stored procedure here
	@Title varchar(50),
	@Version int,
	@AreaID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TopicID, Title, Content, IsLocked, Version, AreaID, IsCurrent, CreatedDate
	FROM Topic
	WHERE Title = @Title
	AND Version = @Version
	AND AreaID = @AreaID
END









GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

