use MDBS

GO

CREATE PROCEDURE dbo.p_check_user
@login nvarchar(200),
@passwordHash nvarchar(200),
@user_id uniqueidentifier output

AS
BEGIN
	if (select count(*) from dbo.[user] (nolock) where DocNumber = @login and PasswordHash = @passwordHash) = 1
	begin
		select @user_id = u.ID
		from dbo.[user] u (nolock)
		where u.DocNumber = @login and u.PasswordHash = @passwordHash
	end
	else
	begin
		select @user_id = '00000000-0000-0000-0000-000000000000'
	end
END;