use MDBS

GO

CREATE PROCEDURE dbo.p_send_message
@message_id uniqueidentifier,
@info nvarchar(max),
@diagnosis nvarchar(max),
@patient_number nvarchar(100),
@user_id uniqueidentifier,
@to_id uniqueidentifier,
@imgs_count int

AS
BEGIN
	insert into dbo.[Message]
		([ID],
		[Info],
		[Diagnosis],
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
	
	while (@imgs_count > 0)
	begin
		declare @attachment_id uniqueidentifier = newid()
	
		insert into dbo.[Attachments] ([ID], [MessageID], [Data], [Comment])
		values (@attachment_id, @message_id, null, '')
		
		insert into dbo.[AttachmentsQueue] ([MessageID], [AttachmentID], [Checksumm], [Status])
		values (@message_id, @attachment_id, null, 0)
		
		set @imgs_count = @imgs_count - 1
	end
END;