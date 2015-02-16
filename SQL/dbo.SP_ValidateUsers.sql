CREATE PROCEDURE [dbo].[SP_ValidateUsers]
	@UserName varchar(50),
	@Password varchar(50),
	@Status int OUTPUT
AS
	IF(@UserName = NULL OR @Password = NULL)
	BEGIN
		SET @Status = 0
	END
	IF NOT EXISTS(SELECT 1 FROM 
				  Users WHERE UserName=@UserName AND Password = @Password)
		BEGIN
			SET @Status = 0
		END
	
	SET @Status = 1

	RETURN @Status