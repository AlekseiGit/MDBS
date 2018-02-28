use MDBS

GO

CREATE PROCEDURE dbo.p_check_user
@login nvarchar(200),
@passwordHash nvarchar(200),
@docStatus int,
@user_id uniqueidentifier output,
@user_name nvarchar(200) output,
@user_num nvarchar(100) output

AS
BEGIN
	if (select count(*) from dbo.[user] (nolock) where DocNumber = @login and PasswordHash = @passwordHash and DocStatus = @docStatus) = 1
	begin
		select
			@user_id = u.ID,
			@user_name = u.FullName,
			@user_num = u.DocNumber
		from dbo.[user] u (nolock)
		where u.DocNumber = @login and u.PasswordHash = @passwordHash
	end
	else
	begin
		select @user_id = '00000000-0000-0000-0000-000000000000'
		select @user_name = ''
		select @user_num = ''
	end
END;