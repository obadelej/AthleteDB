CREATE PROCEDURE [dbo].[spAthlete_GetAll]
	AS
Begin
	set nocount on
	Select Id, FirstName, LastName, BirthDate, IsMale, Phone, Email, AddressId, ParentId, SchoolId, CoachId
	From dbo.[Athlete];
END