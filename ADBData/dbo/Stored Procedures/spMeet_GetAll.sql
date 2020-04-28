CREATE PROCEDURE [dbo].[spMeet_GetAll]
AS
Begin
	set nocount on
	Select Id, MeetName, StartDate, EndDate, [Location]
	From dbo.[Meet];
END