CREATE PROCEDURE [dbo].[spCoach_GetById]
	@Id int
	AS
	Begin
		set Nocount on;

		Select Id, FirstName, LastName, Phone, Email
		From dbo.[Coach]
		Where Id = @Id;
	End