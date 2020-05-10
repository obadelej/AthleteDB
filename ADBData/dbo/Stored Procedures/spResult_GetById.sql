CREATE PROCEDURE [dbo].[spResult_GetById]
@id int
AS
BEGIN
	Set Nocount on;

	Select *
	From dbo.[Result]
	WHERE Id = @id;

END