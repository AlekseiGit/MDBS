use MDBS

GO

CREATE PROCEDURE dbo.p_get_outgoing_messages
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
		1 as [Status] --m.[Status]
	from dbo.message m (nolock)
		inner join dbo.Patient p (nolock)
			on m.PatientID = p.ID
	where
		m.[From] = @user_id
		and m.[MessageDate] >= getdate() - 60
	order by
		m.[MessageDate] desc
END;