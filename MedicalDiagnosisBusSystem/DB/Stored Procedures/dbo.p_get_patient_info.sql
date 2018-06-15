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
		p.[BirthDate],
		p.[MedicalCardNumber],
		p.[VisitDate],
		p.[UsedDrugs],
		p.[RemissionPeriod],
		p.[LastExacerbation],
		p.[AppliedTherapy],
		p.[SurveyResults],
		p.[Info]
	from dbo.patient p (nolock)
	where
		p.[ID] = @patient_id
END;