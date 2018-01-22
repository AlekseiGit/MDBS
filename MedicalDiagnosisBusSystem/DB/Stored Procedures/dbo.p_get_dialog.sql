use MDBS

GO

CREATE PROCEDURE dbo.p_get_dialog
@message_id uniqueidentifier

AS
BEGIN
	declare @patient_id uniqueidentifier
	select @patient_id = isnull([PatientID], '00000000-0000-0000-0000-000000000000') from dbo.message m (nolock) where [ID] = @message_id

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
		m.[PatientID] = @patient_id
	order by
		m.[MessageDate] desc
END;