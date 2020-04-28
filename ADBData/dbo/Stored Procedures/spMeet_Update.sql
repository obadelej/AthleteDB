CREATE PROCEDURE [dbo].[spMeet_Update]
@Id int,
@MeetName nvarchar(150),
@StartDate DateTime,
@EndDate DateTime,
@Location NVARCHAR(100)
AS
Begin
	set nocount on;
		
	Update dbo.[Meet]
	Set
	MeetName = @MeetName,
	StartDate = @StartDate,
	EndDate = @EndDate,
	[Location] = @Location
	Where Id = @Id;
end