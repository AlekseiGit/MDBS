use MDBS

GO

CREATE PROCEDURE dbo.p_send_message
@info nvarchar(max),
@diagnosis nvarchar(max),
--@therapy_plan nvarchar(max),
@patient_number nvarchar(100),
@user_id uniqueidentifier,
@to_id uniqueidentifier,
@img_0_send int = 0,
@img_1_send int = 0,
@img_2_send int = 0,
@img_3_send int = 0,
@img_4_send int = 0,
@img_5_send int = 0,
@img_6_send int = 0,
@img_7_send int = 0,
@img_8_send int = 0,
@img_9_send int = 0,
@img_0 [varbinary](max) = null,
@img_1 [varbinary](max) = null,
@img_2 [varbinary](max) = null,
@img_3 [varbinary](max) = null,
@img_4 [varbinary](max) = null,
@img_5 [varbinary](max) = null,
@img_6 [varbinary](max) = null,
@img_7 [varbinary](max) = null,
@img_8 [varbinary](max) = null,
@img_9 [varbinary](max) = null

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
	
	if (@img_0_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_0, '')
	end
	
	if (@img_1_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_1, '')
	end
	
	if (@img_2_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_2, '')
	end
	
	if (@img_3_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_3, '')
	end
	
	if (@img_4_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_4, '')
	end
	
	if (@img_5_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_5, '')
	end
	
	if (@img_6_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_6, '')
	end
	
	if (@img_7_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_7, '')
	end
	
	if (@img_8_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_8, '')
	end
	
	if (@img_9_send = 1)
	begin
		insert into dbo.[Attachments] ([MessageID], [Data], [Comment])
		values (@message_id, @img_9, '')
	end
END;