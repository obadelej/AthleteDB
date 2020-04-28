CREATE PROCEDURE [dbo].[spCoach_Insert]
@Id int=0 output,
@FirstName nvarchar(50),
@LastName nvarchar(50),
@Phone nvarchar(50),
@Email nvarchar(50)
AS
BEGIN
	set nocount on;

	Insert Into dbo.[Coach](FirstName, LastName, Phone, Email)
	Values(@FirstName, @LastName, @Phone, @Email);

	select @Id = SCOPE_IDENTITY();

END