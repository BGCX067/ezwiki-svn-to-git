SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DBConnection_GetByConnectionNameAndAreaID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DBConnection_GetByConnectionNameAndAreaID]
GO










-- =============================================
-- Author:		Mark Garner
-- Create date: 8-15-2006
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DBConnection_GetByConnectionNameAndAreaID] 
	-- Add the parameters for the stored procedure here
	@ConnectionName varchar(50),
	@AreaID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DBConnectionID, ConnectionName, ServerName, DatabaseName, AreaID, Username, Password
	FROM DBConnection
	WHERE ConnectionName = @ConnectionName
	AND AreaID = @AreaID
END










GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

