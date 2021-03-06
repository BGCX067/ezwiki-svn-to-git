SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DBConnection_Persist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DBConnection_Persist]
GO






-- =============================================
-- Author:		Mark Garner
-- Create date: 8-11-06
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DBConnection_Persist] 
	-- Add the parameters for the stored procedure here
	@DBConnectionID int = null,
	@ConnectionName varchar(50),
	@ServerName varchar(20),
	@DatabaseName varchar(20),
	@AreaID int,
	@Username varchar(20),
	@Password varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF @DBConnectionID is null
	BEGIN
		INSERT INTO DBConnection
		(ConnectionName,
		ServerName,
		DatabaseName,
		AreaID,
		Username,
		Password)
		VALUES
		(@ServerName,
		@ConnectionName,
		@DatabaseName,
		@AreaID,
		@Username,
		@Password)

		SELECT @@IDENTITY
	END
	ELSE
	BEGIN
		UPDATE DBConnection
		SET ConnectionName = @ConnectionName,
		ServerName = @ServerName,
		DatabaseName = @DatabaseName,
		AreaID = @AreaID,
		Username = @Username,
		Password = @Password
		WHERE DBConnectionID = @DBConnectionID
	END
END






GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

