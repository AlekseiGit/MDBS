use MDBS

GO

CREATE PROCEDURE dbo.p_attachments

AS
BEGIN
	update aq
	set aq.[Status] = 1
	from dbo.[Message] m (nolock)
		inner join dbo.[Attachments] a (nolock) on a.MessageID = m.ID
		inner join dbo.[AttachmentsQueue] aq (nolock) on aq.[AttachmentID] = a.[ID]
	where
		aq.[Status] = 0
		and aq.[Checksumm] = (select CAST(a.Data as varbinary(max)) FOR XML PATH(''), BINARY BASE64)

	update msg
	set msg.[Status] = 0
	from dbo.[Message] msg (nolock)
	where
		msg.[Status] = -1
		and (select count(aq.[ID])
			from dbo.[Message] m (nolock)
				inner join dbo.[Attachments] a (nolock) on a.MessageID = m.ID
				inner join dbo.[AttachmentsQueue] aq (nolock) on aq.[AttachmentID] = a.[ID]
			where
				m.[ID] = msg.[ID]
				and aq.[Status] = 0) = 0
END;