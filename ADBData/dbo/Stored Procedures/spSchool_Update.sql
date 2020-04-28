CREATE PROCEDURE [dbo].[spSchool_Update]
@Id int,
@SchoolName nvarchar(150),
@SchoolCode NVARCHAR(50),
@Phone NVARCHAR(50),
@Location NVARCHAR(150)
AS
Begin
	set nocount on;
		
	Update dbo.[School]
	Set
	SchoolName = @SchoolName,
	SchoolCode = @SchoolCode,
	Phone = @Phone,
	[Location] = @Location
	Where Id = @Id;
end