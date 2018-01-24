use MDBS

GO

CREATE PROCEDURE dbo.p_send_message
@info nvarchar(max),
@diagnosis nvarchar(max),
--@therapy_plan nvarchar(max),
@patient_number nvarchar(100),
@user_id uniqueidentifier,
@to_id uniqueidentifier,
@img_one_send int = 0,
@img_two_send int = 0,
@img_three_send int = 0,
@img_four_send int = 0,
@img_five_send int = 0,
@img_one [varbinary](max) = null,
@img_two [varbinary](max) = null,
@img_three [varbinary](max) = null,
@img_four [varbinary](max) = null,
@img_five [varbinary](max) = null

AS
BEGIN
	declare @message_id uniqueidentifier = newid()
	
	insert into dbo.[Message]
		([ID],
		[Info],
		[Diagnosis],
		--[TherapyPlan],
		[ParentMessageID],
		[PatientID],
		[From],
		[To],
		[FromName],
		[MessageDate],
		[Status])
	values
		(@message_id,
		@info,
		@diagnosis,
		null,
		(select [ID] from dbo.[Patient] where [MedicalCardNumber] = @patient_number),
		@user_id,
		@to_id,
		(select [FullName] + ' (' + substring(DocNumber, 3, 2) + ')' from dbo.[User] where [ID] = @user_id),
		getdate(),
		0)
	
	if (@img_one_send = 1)
	begin
		insert into dbo.[Attachments]
			([MessageID],
			[Data],
			[Comment])
		values
			(@message_id,
			@img_one,
			'')
	end
	
	if (@img_two_send = 1)
	begin
		insert into dbo.[Attachments]
			([MessageID],
			[Data],
			[Comment])
		values
			(@message_id,
			@img_two,
			'')
	end
	
	if (@img_three_send = 1)
	begin
		insert into dbo.[Attachments]
			([MessageID],
			[Data],
			[Comment])
		values
			(@message_id,
			@img_three,
			'')
	end
	
	if (@img_four_send = 1)
	begin
		insert into dbo.[Attachments]
			([MessageID],
			[Data],
			[Comment])
		values
			(@message_id,
			@img_four,
			'')
	end
	
	if (@img_five_send = 1)
	begin
		insert into dbo.[Attachments]
			([MessageID],
			[Data],
			[Comment])
		values
			(@message_id,
			@img_five,
			'')
	end
END;