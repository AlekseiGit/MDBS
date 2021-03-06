USE [MDBS]
GO

/****** Object:  Table [dbo].[Attachments]    Script Date: 01.04.2017 14:34:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Attachments](
	[ID] [uniqueidentifier] DEFAULT NEWSEQUENTIALID(),
	[MessageID] [uniqueidentifier] NULL,
	[Data] [varbinary](max) NULL,
	[Comment] [nvarchar](max) NULL
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO