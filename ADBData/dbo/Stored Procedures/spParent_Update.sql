CREATE PROCEDURE [dbo].[spParent_Update]
@Id int,
@FirstName nvarchar(50),
@LastName NVARCHAR(50),
@Phone NVARCHAR(50),
@Email NVARCHAR(50)
AS
Begin
	set nocount on;
		
	Update dbo.[Parent]
	Set
	FirstName = @FirstName,
	LastName = @LastName,
	Phone = @Phone,
	Email = @Email
	Where Id = @Id;
end