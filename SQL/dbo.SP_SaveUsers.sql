CREATE PROCEDURE [dbo].[SP_SaveUsers]
	@UserName varchar(50),
	@Password varchar(50),
	@Status int OUTPUT
AS
	IF(@UserName = NULL OR @Password = NULL)
	BEGIN
		SET @Status = 0
	END
	IF NOT EXISTS(SELECT 1 FROM 
				  Users WHERE UserName=@UserName)
		BEGIN
			INSERT INTO Users(UserName, Password) VALUES (@UserName, @Password)
		END
	ELSE
		BEGIN
			UPDATE Users
			   SET Password = @Password
			 WHERE UserName = @UserName
		END