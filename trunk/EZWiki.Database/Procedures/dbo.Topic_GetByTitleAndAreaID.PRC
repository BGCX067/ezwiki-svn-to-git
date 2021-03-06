SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Topic_GetByTitleAndAreaID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Topic_GetByTitleAndAreaID]
GO








-- =============================================
-- Author:		Mark Garner
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Topic_GetByTitleAndAreaID] 
	-- Add the parameters for the stored procedure here
	@Title varchar(50),
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
	AND AreaID = @AreaID
	AND IsCurrent = 1
END








GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

