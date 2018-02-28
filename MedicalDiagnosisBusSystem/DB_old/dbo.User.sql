USE [MDBS]
GO

/****** Object:  Table [dbo].[User]    Script Date: 01.04.2017 14:34:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[ID] [uniqueidentifier] DEFAULT NEWSEQUENTIALID(),
	[FullName] [nvarchar](200) NULL,
	[DocNumber] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[PasswordHash] [nvarchar](200) NULL,
	[DocStatus] [int] NULL -- 1 - может давать консультации, 0 - не может давать консультации
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Index [NonClusteredIndex-DocNumber]    Script Date: 24.01.2018 18:12:01 ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-DocNumber] ON [dbo].[User]
(
	[DocNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO