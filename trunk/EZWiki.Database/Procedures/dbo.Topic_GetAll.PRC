SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Topic_GetAll]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Topic_GetAll]
GO






-- =============================================
-- Author:		Mark Garner
-- Create date: 7-30-2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Topic_GetAll] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TopicID, Title, Content, IsLocked, Version, AreaID, IsCurrent, CreatedDate
	FROM Topic
END






GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

