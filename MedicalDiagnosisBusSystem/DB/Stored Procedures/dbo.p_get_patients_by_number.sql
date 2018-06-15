use MDBS

GO

CREATE PROCEDURE dbo.p_get_patients_by_number
@user_id uniqueidentifier,
@num nvarchar(100)

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
			p.[Info]
		from dbo.patient p (nolock)
		where p.[MedicalCardNumber] like '%' + @num + '%'
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
			p.[Info]
		from dbo.patient p (nolock)
		where left(p.[MedicalCardNumber], 5) = @code and p.[MedicalCardNumber] like '%' + @num + '%'
		order by p.[FullName] asc
	end
END;