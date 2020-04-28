CREATE PROCEDURE [dbo].[spParent_GetById]
@Id int
AS
Begin
	set Nocount on;

	Select Id, FirstName, LastName, Phone, Email
	From dbo.[Parent]
	Where Id = @Id;
End