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
		p.[BirthDate],
		p.[MedicalCardNumber],
		p.[Info],
		p.[Note]
	from dbo.patient p (nolock)
	where
		p.[ID] = @patient_id
END;