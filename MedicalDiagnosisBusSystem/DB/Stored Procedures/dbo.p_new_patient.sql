use MDBS

GO

CREATE PROCEDURE dbo.p_new_patient
@sex int,
@weight int,
@birthDate datetime,
@medicalCardNumber nvarchar(100),
@visitDate datetime,
@usedDrugs nvarchar(max),
@remissionPeriod nvarchar(max),
@lastExacerbation datetime,
@appliedTherapy nvarchar(max),
@surveyResults nvarchar(max),
@info nvarchar(max),
@fio nvarchar(200)

AS
BEGIN
	declare @patient_id uniqueidentifier = newid()
	
	insert into dbo.[Patient]
		([ID],
		[FullName],
		[Sex],
		[Weight],
		[BirthDate],
		[MedicalCardNumber],
		[VisitDate],
		[UsedDrugs],
		[RemissionPeriod],
		[LastExacerbation],
		[AppliedTherapy],
		[SurveyResults],
		[Info])
	values
		(@patient_id,
		@fio,
		@sex,
		@weight,
		@birthDate,
		@medicalCardNumber,
		@visitDate,
		@usedDrugs,
		@remissionPeriod,
		@lastExacerbation,
		@appliedTherapy,
		@surveyResults,
		@info)
END;