use MDBS

GO

CREATE PROCEDURE dbo.p_new_user
@fullName nvarchar(100),
@docNumber nvarchar(100),
@passwordHash nvarchar(200)

AS
BEGIN
	declare @user_id uniqueidentifier = newid()
	
	insert into dbo.[User]
		([ID],
		[FullName],
		[DocNumber],
		[PasswordHash],
		[DocStatus])
	values
		(@user_id,
		@fullName,
		@docNumber,
		@passwordHash,
		0)
END;