CREATE PROCEDURE [dbo].[spAthlete_Update]
@Id INT=0 OUTPUT,
@FirstName NVARCHAR(50),
@LastName NVARCHAR(50),
@BirthDate DATETIME2,
@IsMale bit,
@Phone NVARCHAR(50),
@Email NVARCHAR(150),
@AddressId INT,
@ParentId INT,
@SchoolId INT,
@CoachId INT
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.[Athlete]
	SET
	FirstName = @FirstName,
	LastName = @LastName,
	BirthDate = @BirthDate,
	IsMale =  @IsMale,
	Phone = @Phone,
	Email =  @Email,
	AddressId = @AddressId,
	ParentId =  @ParentId,
	SchoolId =@SchoolId,
	CoachId = @CoachId
	where Id = @Id;
END