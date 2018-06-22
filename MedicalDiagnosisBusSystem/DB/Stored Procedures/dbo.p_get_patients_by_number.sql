use MDBS

GO

CREATE PROCEDURE dbo.p_get_patients_by_field
@user_id uniqueidentifier,
@field nvarchar(200)

AS
BEGIN
	if (select DocStatus from dbo.[User] where ID = @user_id) = 1
	begin
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
			p.[Complaints],
			p.[Info]
		from dbo.patient p (nolock)
		where p.[FullName] = @field
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
			p.[BirthDate],
			p.[MedicalCardNumber],
			p.[VisitDate],
			p.[UsedDrugs],
			p.[RemissionPeriod],
			p.[LastExacerbation],
			p.[AppliedTherapy],
			p.[SurveyResults],
			p.[Complaints],
			p.[Info]
		from dbo.patient p (nolock)
		where p.[FullName] = @field
		order by p.[FullName] asc
	end
END;