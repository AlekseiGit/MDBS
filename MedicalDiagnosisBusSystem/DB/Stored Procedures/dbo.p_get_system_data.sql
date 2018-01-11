use MDBS

GO

CREATE PROCEDURE dbo.p_get_system_data
@user_id uniqueidentifier

AS
BEGIN
	declare
		@incoming_info nvarchar(10),
		@outgoing_info nvarchar(10),
		@need_answer_info nvarchar(10),
		@need_answer_date datetime,
		@need_answer_status int

	select @incoming_info = count(*)
	from dbo.message m (nolock) inner join dbo.Patient p (nolock) on m.PatientID = p.ID
	where m.[To] = @user_id and m.[Status] = 0 and m.[MessageDate] >= getdate() - 60

	set @incoming_info += '/'
	
	select @incoming_info += cast(count(*) as nvarchar)
	from dbo.message m (nolock) inner join dbo.Patient p (nolock) on m.PatientID = p.ID
	where m.[To] = @user_id and m.[MessageDate] >= getdate() - 60

	select @outgoing_info = count(*)
	from dbo.message m (nolock) inner join dbo.Patient p (nolock) on m.PatientID = p.ID
	where m.[From] = @user_id and m.[MessageDate] >= getdate() - 60
	
	select @need_answer_info = count(*)
	from dbo.message m (nolock) inner join dbo.Patient p (nolock) on m.PatientID = p.ID
	where m.[To] = @user_id and (select count(*) from dbo.message msg (nolock) where msg.[ParentMessageID] = m.[ID]) = 0

	select @need_answer_date = min(m.MessageDate)
	from dbo.message m (nolock) inner join dbo.Patient p (nolock) on m.PatientID = p.ID
	where m.[To] = @user_id and m.[Status] = 0
	
	if (select datediff(day, @need_answer_date, getdate())) < 2
	begin
		set @need_answer_status = 1
	end
	else if ((select datediff(day, @need_answer_date, getdate())) >= 2 and (select datediff(day, @need_answer_date, getdate())) < 3)
	begin
		set @need_answer_status = 2
	end
	else
	begin
		set @need_answer_status = 3
	end
	
	select
		@incoming_info as [incoming_info],
		@outgoing_info as [outgoing_info],
		@need_answer_info as [need_answer_info],
		@need_answer_status as [need_answer_status]
END;