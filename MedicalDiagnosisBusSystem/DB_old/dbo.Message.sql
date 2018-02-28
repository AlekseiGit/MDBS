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

ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_From] FOREIGN KEY([From])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_From]
GO

ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_To] FOREIGN KEY([To])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_To]
GO

ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Patient] FOREIGN KEY([PatientID])
REFERENCES [dbo].[Patient] ([ID])
GO

ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Patient]
GO

ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_Message] FOREIGN KEY([ParentMessageID])
REFERENCES [dbo].[Message] ([ID])
GO

ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_Message]
GO