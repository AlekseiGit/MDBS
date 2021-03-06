USE [MDBS]
GO

/****** Object:  Table [dbo].[Message]    Script Date: 01.04.2017 18:57:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Message](
	[ID] [uniqueidentifier] DEFAULT NEWSEQUENTIALID(),
	[Info] [nvarchar](max) NULL,
	[Diagnosis] [nvarchar](max) NULL,
	[TherapyPlan] [nvarchar](max) NULL,
	[ParentMessageID] [uniqueidentifier] NULL,
	[PatientID] [uniqueidentifier] NULL,
	[From] [uniqueidentifier] NULL,
	[To] [uniqueidentifier] NULL,
	[FromName] [nvarchar](100) NULL,
	[MessageDate] [datetime] NULL,
	[Status] [int] NULL -- 0 - новый, 1 - прочитанный
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO