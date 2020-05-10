CREATE PROCEDURE [dbo].[spAthlete_Insert]
@Id int=0 output,
@FirstName nvarchar(50),
@LastName NVARCHAR(50),
@BirthDate DATETIME2,
@IsMale bit,
@Phone NVARCHAR(50),
@Email NVARCHAR(150),
@AddressId INT,
@ParentId INT,
@SchoolId INT,
@CoachId int
AS
BEGIN
	set nocount on;

	Insert Into dbo.[Athlete](FirstName, LastName, BirthDate, IsMale, Phone, Email, AddressId, ParentId, SchoolId, CoachId)
	Values(@FirstName, @LastName, @BirthDate, @IsMale, @Phone, @Email, @AddressId, @ParentId, @SchoolId, @CoachId);

	select @Id = SCOPE_IDENTITY();

END