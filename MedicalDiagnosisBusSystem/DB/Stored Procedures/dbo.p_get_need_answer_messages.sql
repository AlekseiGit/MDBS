use MDBS

GO

CREATE PROCEDURE dbo.p_get_need_answer_messages
@user_id uniqueidentifier

AS
BEGIN
	select
		m.[ID],
		m.[Info],
		m.[Diagnosis],
		m.[TherapyPlan],
		isnull(m.[ParentMessageID], '00000000-0000-0000-0000-000000000000') as [ParentMessageID],
		isnull(m.[PatientID], '00000000-0000-0000-0000-000000000000') as [PatientID],
		isnull(p.[MedicalCardNumber], '---') as [PatientName],
		isnull(m.[From], '00000000-0000-0000-0000-000000000000') as [From],
		isnull(m.[To], '00000000-0000-0000-0000-000000000000') as [To],
		m.[FromName],
		m.[MessageDate],
		m.[Status]
	from dbo.message m (nolock)
		inner join dbo.Patient p (nolock)
			on m.PatientID = p.ID
	where
		m.[To] = @user_id
		and (select count(*) from dbo.message msg (nolock) where msg.[ParentMessageID] = m.[ID]) = 0
	order by
		m.[MessageDate] asc
END;