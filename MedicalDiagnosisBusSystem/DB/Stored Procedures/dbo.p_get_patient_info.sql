use MDBS

GO

CREATE PROCEDURE dbo.p_get_patient_info
@patient_id uniqueidentifier

AS
BEGIN
	select
		p.[ID],
		p.[FullName],
		p.[Sex],
		p.[Weight],
		p.[DrugsCount],
		p.[BirthDate],
		p.[MedicalCardNumber],
		p.[CurrentTherapy],
		p.[IllStart],
		p.[UsedDrugs],
		p.[RemissionPeriod],
		p.[LastExacerbation],
		p.[AppliedTherapy],
		p.[SurveyResults],
		p.[Info],
		p.[Note]
	from dbo.patient p (nolock)
	where
		p.[ID] = @patient_id
END;