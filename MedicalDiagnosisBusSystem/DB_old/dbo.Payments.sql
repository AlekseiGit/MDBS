USE [MDBS]
GO

/****** Object:  Table [dbo].[Payments]    Script Date: 01.04.2017 14:34:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payments](
	[ID] [uniqueidentifier] DEFAULT NEWSEQUENTIALID(),
	[MessageID] [uniqueidentifier] NULL,
	[Data] [varbinary](max) NULL,
	[Summ] decimal(18,2) NULL,
	[PayByCard] [int] NULL,
	[Comment] [nvarchar](max) NULL
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Message] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Message] ([ID])
GO

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Message]
GO