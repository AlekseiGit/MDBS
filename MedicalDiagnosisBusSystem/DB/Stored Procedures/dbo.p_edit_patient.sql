use MDBS

GO

CREATE PROCEDURE dbo.p_edit_patient
@patient_id uniqueidentifier,
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
@complaints nvarchar(max),
@info nvarchar(max)

AS
BEGIN
	update p
	set
		p.[Sex] = @sex,
		p.[Weight] = @weight,
		p.[BirthDate] = @birthDate,
		p.[MedicalCardNumber] = @medicalCardNumber,
		p.[VisitDate] = @visitDate,
		p.[UsedDrugs] = @usedDrugs,
		p.[RemissionPeriod] = @remissionPeriod,
		p.[LastExacerbation] = @lastExacerbation,
		p.[AppliedTherapy] = @appliedTherapy,
		p.[SurveyResults] = @surveyResults,
		p.[Complaints] = @complaints,
		p.[Info] = @info
	from dbo.[Patient] p
	where
		p.[ID] = @patient_id
END;