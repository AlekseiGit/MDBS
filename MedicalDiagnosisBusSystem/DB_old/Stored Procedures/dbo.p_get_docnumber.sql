use MDBS

GO

CREATE PROCEDURE dbo.p_get_docnumber
@user_id uniqueidentifier,
@new_docnumber nvarchar(100) output

AS
BEGIN
	declare
		@code nvarchar(10),
		@num int
	
	select @code = left(DocNumber, 5) from dbo.[User] where ID = @user_id

	select top 1 @num = right(u.DocNumber, 2) + 1
	from dbo.[User] u
	where left(u.DocNumber, 5) = @code
	order by u.DocNumber desc

	select @new_docnumber = @code + right('0' + cast(@num as varchar), 2)
END;