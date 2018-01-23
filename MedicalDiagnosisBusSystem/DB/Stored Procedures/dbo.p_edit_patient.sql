use MDBS

GO

CREATE PROCEDURE dbo.p_edit_patient
@patient_id uniqueidentifier,
@fullName nvarchar(200),
@sex int,
@weight int,
@drugsCount nvarchar(10),
@birthDate datetime,
@medicalCardNumber nvarchar(100),
@currentTherapy nvarchar(max),
@info nvarchar(max),
@note nvarchar(max)

AS
BEGIN
	update p
	set
		p.[FullName] = @fullName,
		p.[Sex] = @sex,
		p.[Weight] = @weight,
		p.[DrugsCount] = @drugsCount,
		p.[BirthDate] = @birthDate,
		p.[MedicalCardNumber] = @medicalCardNumber,
		p.[CurrentTherapy] = @currentTherapy,
		p.[Info] = @info,
		p.[Note] = @note
	from dbo.[Patient] p
	where
		p.[ID] = @patient_id
END;