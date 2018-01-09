use MDBS

GO

CREATE PROCEDURE dbo.p_get_patients

AS
BEGIN
	select
		p.[ID],
		p.[FullName],
		p.[Sex],
		p.[Weight],
		p.[BirthDate],
		p.[MedicalCardNumber],
		p.[CurrentTherapy],
		p.[Info],
		p.[Note]
	from dbo.patient p (nolock)
	order by p.[FullName] asc
END;