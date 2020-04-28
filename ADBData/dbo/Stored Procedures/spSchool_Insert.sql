CREATE PROCEDURE [dbo].[spSchool_Insert]
@Id int=0 output,
@SchoolName nvarchar(150),
@SchoolCode nvarchar(50),
@Phone NVARCHAR(50),
@Location nvarchar(150)
AS
BEGIN
	set nocount on;

	Insert Into dbo.[School](SchoolName, SchoolCode, Phone, [Location])
	Values(@SchoolName, @SchoolCode, @Phone, @Location);

	select @Id = SCOPE_IDENTITY();

END