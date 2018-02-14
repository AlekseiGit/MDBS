USE [MDBS]
GO

/****** Object:  Table [dbo].[MessageQueue]    Script Date: 01.04.2017 18:57:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MessageQueue](
	[ID] [uniqueidentifier] DEFAULT NEWSEQUENTIALID(),
	[MessageID] [uniqueidentifier] NULL,
	[Status] [int] NULL -- 0 - новый, 1 - прочитанный
 CONSTRAINT [PK_MessageQueue] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO