use MDBS

GO

CREATE PROCEDURE dbo.p_edit_patient
@patient_id uniqueidentifier,
@sex int,
@weight int,
@drugsCount nvarchar(10),
@birthDate datetime,
@medicalCardNumber nvarchar(100),
@currentTherapy nvarchar(max),
@illStart datetime,
@usedDrugs nvarchar(max),
@remissionPeriod nvarchar(max),
@lastExacerbation datetime,
@appliedTherapy nvarchar(max),
@surveyResults nvarchar(max),
@info nvarchar(max),
@note nvarchar(max)

AS
BEGIN
	update p
	set
		p.[Sex] = @sex,
		p.[Weight] = @weight,
		p.[DrugsCount] = @drugsCount,
		p.[BirthDate] = @birthDate,
		p.[MedicalCardNumber] = @medicalCardNumber,
		p.[CurrentTherapy] = @currentTherapy,
		p.[IllStart] = @illStart,
		p.[UsedDrugs] = @usedDrugs,
		p.[RemissionPeriod] = @remissionPeriod,
		p.[LastExacerbation] = @lastExacerbation,
		p.[AppliedTherapy] = @appliedTherapy,
		p.[SurveyResults] = @surveyResults,
		p.[Info] = @info,
		p.[Note] = @note
	from dbo.[Patient] p
	where
		p.[ID] = @patient_id
END;