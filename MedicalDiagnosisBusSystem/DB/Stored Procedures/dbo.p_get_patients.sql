use MDBS

GO

CREATE PROCEDURE dbo.p_get_patients
@user_id uniqueidentifier

AS
BEGIN
	if (select DocStatus from dbo.[User] where ID = @user_id) = 1
	begin
		select
			p.[ID],
			p.[FullName],
			p.[Sex],
			p.[Weight],
			p.[DrugsCount],
			p.[BirthDate],
			p.[MedicalCardNumber],
			p.[CurrentTherapy],
			p.[Info],
			p.[Note]
		from dbo.patient p (nolock)
		order by p.[FullName] asc
	end
	else
	begin
		declare @code nvarchar(10)

		select @code = left(DocNumber, 5) from dbo.[User] where ID = @user_id

		select
			p.[ID],
			p.[FullName],
			p.[Sex],
			p.[Weight],
			p.[DrugsCount],
			p.[BirthDate],
			p.[MedicalCardNumber],
			p.[CurrentTherapy],
			p.[Info],
			p.[Note]
		from dbo.patient p (nolock)
		where left(p.[MedicalCardNumber], 5) = @code
		order by p.[FullName] asc
	end
END;