CREATE PROCEDURE [dbo].[spCoach_Update]
@Id int,
@FirstName nvarchar(50),
@LastName NVARCHAR(50),
@Phone NVARCHAR(50),
@Email NVARCHAR(50)
AS
Begin
	set nocount on;
		
	Update dbo.[Coach]
	Set
	FirstName = @FirstName,
	LastName = @LastName,
	Phone = @Phone,
	Email = @Email
	Where Id = @Id;
end