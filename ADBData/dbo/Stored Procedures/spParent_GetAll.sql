CREATE PROCEDURE [dbo].[spParent_GetAll]
AS
Begin
	set nocount on
	Select Id, FirstName, LastName, Phone, Email
	From dbo.[Parent];
END