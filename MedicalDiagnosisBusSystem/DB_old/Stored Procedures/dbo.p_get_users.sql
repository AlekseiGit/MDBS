use MDBS

GO

CREATE PROCEDURE dbo.p_get_users
@user_id uniqueidentifier

AS
BEGIN
	if (select DocStatus from dbo.[User] where ID = @user_id) = 1
	begin
		select
			u.[ID],
			u.[FullName],
			u.[DocNumber]
		from dbo.[user] u (nolock)
		order by u.[DocNumber] asc
	end
	else
	begin
		declare @code nvarchar(10)

		select @code = left(DocNumber, 5) from dbo.[User] where ID = @user_id

		select
			u.[ID],
			u.[FullName],
			u.[DocNumber]
		from dbo.[user] u (nolock)
		where left(u.[DocNumber], 5) = @code
		order by u.[DocNumber] asc
	end
END;