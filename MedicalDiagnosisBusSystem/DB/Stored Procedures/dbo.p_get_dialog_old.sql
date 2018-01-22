use MDBS

GO

CREATE PROCEDURE dbo.p_get_dialog
@message_id uniqueidentifier

AS
BEGIN
	create table #msg
	(
		[ID] [uniqueidentifier],
		[Info] [nvarchar](max),
		[Diagnosis] [nvarchar](max),
		[TherapyPlan] [nvarchar](max),
		[ParentMessageID] [uniqueidentifier],
		[PatientID] [uniqueidentifier],
		[PatientName] [nvarchar](200),
		[From] [uniqueidentifier],
		[To] [uniqueidentifier],
		[FromName] [nvarchar](100),
		[MessageDate] [datetime],
		[Status] [int]
	)
	
	declare @msg_id uniqueidentifier
	declare @parent_msg_id uniqueidentifier
	declare @child_msg_id uniqueidentifier
	
	set @msg_id = @message_id
	
	while (1 = 1)
	begin
		select @parent_msg_id = isnull([ParentMessageID], '00000000-0000-0000-0000-000000000000') from dbo.message m (nolock) where [ID] = @msg_id
		select @child_msg_id = isnull([ID], '00000000-0000-0000-0000-000000000000') from dbo.message m (nolock) where [ParentMessageID] = @msg_id
		
		if (@child_msg_id != '00000000-0000-0000-0000-000000000000')
		begin
			insert into #msg
			select
				m.[ID],
				m.[Info],
				m.[Diagnosis],
				m.[TherapyPlan],
				isnull(m.[ParentMessageID], '00000000-0000-0000-0000-000000000000') as [ParentMessageID],
				isnull(m.[PatientID], '00000000-0000-0000-0000-000000000000') as [PatientID],
				isnull(p.[FullName], '---') as [PatientName],
				isnull(m.[From], '00000000-0000-0000-0000-000000000000') as [From],
				isnull(m.[To], '00000000-0000-0000-0000-000000000000') as [To],
				m.[FromName],
				m.[MessageDate],
				m.[Status]
			from dbo.message m (nolock)
				inner join dbo.Patient p (nolock)
					on m.PatientID = p.ID
			where
				m.[ID] = @msg_id
			order by
				m.[MessageDate] desc
				set @msg_id = @child_msg_id
		end
		
		insert into #msg
		select
			m.[ID],
			m.[Info],
			m.[Diagnosis],
			m.[TherapyPlan],
			isnull(m.[ParentMessageID], '00000000-0000-0000-0000-000000000000') as [ParentMessageID],
			isnull(m.[PatientID], '00000000-0000-0000-0000-000000000000') as [PatientID],
			isnull(p.[FullName], '---') as [PatientName],
			isnull(m.[From], '00000000-0000-0000-0000-000000000000') as [From],
			isnull(m.[To], '00000000-0000-0000-0000-000000000000') as [To],
			m.[FromName],
			m.[MessageDate],
			m.[Status]
		from dbo.message m (nolock)
			inner join dbo.Patient p (nolock)
				on m.PatientID = p.ID
		where
			m.[ID] = @msg_id
		order by
			m.[MessageDate] desc
				
		if (@parent_msg_id != '00000000-0000-0000-0000-000000000000')
		begin
			set @msg_id = @parent_msg_id
			--set @parent_msg_id = '00000000-0000-0000-0000-000000000000'
		end
		else
		begin
			break;
		end
	end
	
	select distinct * from #msg order by [MessageDate] desc
END;