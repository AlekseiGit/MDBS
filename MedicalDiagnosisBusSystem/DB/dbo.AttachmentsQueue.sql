USE [MDBS]
GO

/****** Object:  Table [dbo].[AttachmentsQueue]    Script Date: 01.04.2017 14:34:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AttachmentsQueue](
	[ID] [uniqueidentifier] DEFAULT NEWSEQUENTIALID(),
	[MessageID] [uniqueidentifier] NULL,
	[AttachmentID] [uniqueidentifier] NULL,
	[Checksumm] [nvarchar](max) NULL,
	[Status] [int] NULL -- 0 - новый, 1 - прочитанный
 CONSTRAINT [PK_AttachmentsQueue] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO