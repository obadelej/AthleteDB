CREATE PROCEDURE [dbo].[spAthlete_GetById]
@Id int
AS
Begin
	set Nocount on;

	Select Id, FirstName, LastName, BirthDate, IsMale, Phone, Email, AddressId, ParentId, SchoolId, CoachId
	From dbo.[Athlete]
	Where Id = @Id;
End