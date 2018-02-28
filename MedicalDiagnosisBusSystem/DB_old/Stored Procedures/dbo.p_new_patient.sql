use MDBS

GO

CREATE PROCEDURE dbo.p_new_patient
@sex int,
@weight int,
@drugsCount nvarchar(10),
@birthDate datetime,
@medicalCardNumber nvarchar(100),
@currentTherapy nvarchar(max),
@info nvarchar(max),
@note nvarchar(200)

AS
BEGIN
	declare @patient_id uniqueidentifier = newid()
	
	insert into dbo.[Patient]
		([ID],
		[Sex],
		[Weight],
		[DrugsCount],
		[BirthDate],
		[MedicalCardNumber],
		[CurrentTherapy],
		[Info],
		[Note])
	values
		(@patient_id,
		@sex,
		@weight,
		@drugsCount,
		@birthDate,
		@medicalCardNumber,
		@currentTherapy,
		@info,
		@note)
END;