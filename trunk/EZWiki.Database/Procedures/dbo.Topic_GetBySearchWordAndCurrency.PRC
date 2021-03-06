SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Topic_GetBySearchWordAndCurrency]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Topic_GetBySearchWordAndCurrency]
GO









-- =============================================
-- Author:		Mark Garner
-- Create date: 7-31-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Topic_GetBySearchWordAndCurrency] 
	-- Add the parameters for the stored procedure here
	@SearchWord varchar(50),
	@IsCurrent bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT t.TopicID, Title, Content, IsLocked, Version, AreaID, IsCurrent, CreatedDate --swim.SearchWordIndexMemberID, swim.SearchWordID, swim.SectionID
	FROM SearchWordIndexMember swim
	INNER JOIN SearchWord sw
	ON swim.SearchWordID = sw.SearchWordID
	INNER JOIN Topic t
	ON swim.TopicID = t.TopicID
	WHERE sw.Word = @SearchWord
	AND IsCurrent = @IsCurrent
	ORDER BY t.TopicID
END









GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

