CREATE PROCEDURE [dbo].[spResult_Delete]
@Id int
AS
BEGIN
	set nocount on;

	Delete 
	From dbo.[Result]
	WHERE Id = @Id;
END