use MDBS

GO

CREATE PROCEDURE dbo.p_answer_message
@therapy_plan nvarchar(max),
@parent_message_id uniqueidentifier,
@patient_id uniqueidentifier,
@user_id uniqueidentifier,
@to_id uniqueidentifier

AS
BEGIN
	declare @message_id uniqueidentifier = newid()
	
	insert into dbo.[Message]
		([ID],
		[TherapyPlan],
		[ParentMessageID],
		[PatientID],
		[From],
		[To],
		[FromName],
		[MessageDate],
		[Status])
	values
		(@message_id,
		@therapy_plan,
		@parent_message_id,
		@patient_id,
		@user_id,
		@to_id,
		(select [FullName] from dbo.[User] where [ID] = @user_id),
		getdate(),
		0)
END;