SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Area_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Area_Delete]
GO


-- =============================================
-- Author:		Mark Garner
-- Create date: 8-11-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Area_Delete] 
	-- Add the parameters for the stored procedure here
	@AreaID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM Area
	WHERE AreaID = @AreaID 
END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

