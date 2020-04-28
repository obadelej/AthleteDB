CREATE PROCEDURE [dbo].[spSchool_GetAll]
AS
Begin
	set nocount on
	Select Id, SchoolName, SchoolCode, Phone, [Location]
	From dbo.[School];
END