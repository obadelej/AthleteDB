CREATE PROCEDURE [dbo].[spMeet_GetById]
@Id int
AS
Begin
	set Nocount on;

	Select Id, MeetName, StartDate, EndDate, [Location]
	From dbo.[Meet]
	Where Id = @Id;
End