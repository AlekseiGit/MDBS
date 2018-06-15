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
	[BirthDate] [datetime] NULL,
	[MedicalCardNumber] [nvarchar](100) NOT NULL,
	[VisitDate] [datetime] NULL, -- дата обращения
	[UsedDrugs] [nvarchar](max) NULL, -- Чем лечился, проводилась ли пелотерапия
	[RemissionPeriod] [nvarchar](max) NULL,
	[LastExacerbation] [datetime] NULL,
	[AppliedTherapy] [nvarchar](max) NULL,
	[SurveyResults] [nvarchar](max) NULL,
	[Info] [nvarchar](max) NULL, -- доп инфо по пациенту
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Index [NonClusteredIndex-PatientCardNumber]    Script Date: 24.01.2018 17:17:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-PatientCardNumber] ON [dbo].[Patient]
(
	[MedicalCardNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO