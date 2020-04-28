CREATE PROCEDURE [dbo].[spSchool_GetById]
@Id int
AS
Begin
	set Nocount on;

	Select Id, SchoolName, SchoolCode, Phone, [Location]
	From dbo.[School]
	Where Id = @Id;
End