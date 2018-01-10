use MDBS

GO

CREATE PROCEDURE dbo.p_new_patient
@fullName nvarchar(200),
@sex int,
@weight int,
@birthDate datetime,
@medicalCardNumber nvarchar(100),
@currentTherapy nvarchar(max),
@info nvarchar(max)

AS
BEGIN
	declare @patient_id uniqueidentifier = newid()
	
	insert into dbo.[Patient]
		([ID],
		[FullName],
		[Sex],
		[Weight],
		[BirthDate],
		[MedicalCardNumber],
		[CurrentTherapy],
		[Info],
		[Note])
	values
		(@patient_id,
		@fullName,
		@sex,
		@weight,
		@birthDate,
		@medicalCardNumber,
		@currentTherapy,
		@info,
		'')
END;