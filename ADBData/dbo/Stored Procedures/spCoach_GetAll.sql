CREATE PROCEDURE [dbo].[spCoach_GetAll]
	AS
Begin
	set nocount on
	Select Id, FirstName, LastName, Phone, Email
	From dbo.[Coach];
END