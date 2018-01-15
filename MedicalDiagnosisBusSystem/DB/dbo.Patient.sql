USE [MDBS]
GO

/****** Object:  Table [dbo].[Patient]    Script Date: 01.04.2017 14:34:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Patient](
	[ID] [uniqueidentifier] DEFAULT NEWSEQUENTIALID(),
	[FullName] [nvarchar](200) NULL,
	[Sex] [int] NULL, -- 0 - unknown; 1 - male; 2 - female
	[Weight] [int] NULL, -- вес пациента в килограммах
	[DrugsCount] [nvarchar](100) NULL,
	[BirthDate] [datetime] NULL,
	[MedicalCardNumber] [nvarchar](100) NULL,
	[CurrentTherapy] [nvarchar](max) NULL, -- текущее лечение
	[Info] [nvarchar](max) NULL, -- заполняется по шаблону
	[Note] [nvarchar](200) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO