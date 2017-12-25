use MDBS

delete from dbo.[Attachments]
delete from dbo.[Message]
delete from dbo.[Patient]
delete from dbo.[Payments]
delete from dbo.[User]

declare
@patient1 uniqueidentifier = newid(),
@patient2 uniqueidentifier = newid(),
@patient3 uniqueidentifier = newid(),
@patient4 uniqueidentifier = newid(),
@patient5 uniqueidentifier = newid(),
@patient6 uniqueidentifier = newid(),
@patient7 uniqueidentifier = newid(),
@patient8 uniqueidentifier = newid(),
@patient9 uniqueidentifier = newid(),
@patient10 uniqueidentifier = newid(),
@patient11 uniqueidentifier = newid(),
@patient12 uniqueidentifier = newid(),
@patient13 uniqueidentifier = newid(),
@patient14 uniqueidentifier = newid(),
@patient15 uniqueidentifier = newid(),
@patient16 uniqueidentifier = newid()

insert into [User] (ID, FullName, DocNumber, Password, PasswordHash, DocStatus) values ('5A239C9B-E404-4AF3-A7BD-8D1C4925781D', 'Доктор1', '7701', 'qwerty', '12345678', '1')
insert into [User] (ID, FullName, DocNumber, Password, PasswordHash, DocStatus) values ('4A239C9B-E404-4AF3-A7BD-8D1C4925781D', 'Доктор2', '7702', 'qwerty', '12345678', '1')
insert into [User] (ID, FullName, DocNumber, Password, PasswordHash, DocStatus) values ('3A239C9B-E404-4AF3-A7BD-8D1C4925781D', 'Доктор3', '7703', 'qwerty', '12345678', '0')
insert into [User] (ID, FullName, DocNumber, Password, PasswordHash, DocStatus) values ('2A239C9B-E404-4AF3-A7BD-8D1C4925781D', 'Доктор4', '7704', 'qwerty', '12345678', '0')
insert into [User] (ID, FullName, DocNumber, Password, PasswordHash, DocStatus) values ('1A239C9B-E404-4AF3-A7BD-8D1C4925781D', 'Доктор5', '7705', 'qwerty', '12345678', '1')

insert into [Patient] (ID, FullName, Sex, BirthDate, MedicalCardNumber, Info, Note)
	values (@patient1, 'Пациент 1', 1, '19850319', '7700275', 'Некоторая информация об этом пациенте', 'Небольшая заметка')
insert into [Patient] (ID, FullName, Sex, BirthDate, MedicalCardNumber, Info, Note)
	values (@patient2, 'Пациент 2', 1, '19770102', '7700984', 'Некоторая информация об этом пациенте', 'Небольшая заметка')
insert into [Patient] (ID, FullName, Sex, BirthDate, MedicalCardNumber, Info, Note)
	values (@patient3, 'Пациент 3', 2, '19810808', '7700031', 'Некоторая информация об этом пациенте', 'Небольшая заметка')
insert into [Patient] (ID, FullName, Sex, BirthDate, MedicalCardNumber, Info, Note)
	values (@patient4, 'Пациент 4', 2, '19921206', '7705681', 'Некоторая информация об этом пациенте', 'Небольшая заметка')
insert into [Patient] (ID, FullName, Sex, BirthDate, MedicalCardNumber, Info, Note)
	values (@patient5, 'Пациент 5', 1, '19880721', '7700221', 'Некоторая информация об этом пациенте', 'Небольшая заметка')

/*
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient1, Null, @user1, 'Qwerty', getdate(), 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient2, Null, @user1, 'Qwerty', getdate() - 2, 1)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient3, Null, @user1, 'Qwerty', getdate() - 8, 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient4, Null, @user1, 'Qwerty', getdate() - 98, 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient5, Null, @user1, 'Qwerty', getdate() - 275, 1)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient6, Null, @user1, 'Qwerty', getdate() - 14, 2)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient7, Null, @user1, 'Qwerty', getdate() - 112, 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient8, Null, @user1, 'Qwerty', getdate() - 99, 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient9, Null, @user1, 'Qwerty', getdate() - 75, 2)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient10, Null, @user1, 'Qwerty', getdate() - 88, 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient11, Null, @user1, 'Qwerty', getdate() - 188, 1)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient12, Null, @user1, 'Qwerty', getdate() - 157, 1)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient13, Null, @user1, 'Qwerty', getdate() - 22, 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient14, Null, @user1, 'Qwerty', getdate() - 201, 2)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient15, Null, @user1, 'Qwerty', getdate() - 65, 0)
insert into [Message] (Info, Diagnosis, TherapyPlan, ParentMessageID, PatientID, [From], [To], FromName, MessageDate, Status)
	values ('Some Info', 'Some Diagnosis', 'Some Therapy Plan', Null, @patient16, Null, @user1, 'Qwerty', getdate() - 122, 0)
*/

select * from [User]
select * from [Patient]
select * from [Message]
--select * from [Attachments]
--select * from [Payments]