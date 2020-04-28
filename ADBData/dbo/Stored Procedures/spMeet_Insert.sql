CREATE PROCEDURE [dbo].[spMeet_Insert]
@Id int=0 output,
@MeetName nvarchar(150),
@StartDate DateTime,
@EndDate DateTime,
@Location nvarchar(100)
AS
BEGIN
	set nocount on;

	Insert Into dbo.[Meet](MeetName, StartDate, EndDate, [Location])
	Values(@MeetName, @StartDate, @EndDate, @Location);

	select @Id = SCOPE_IDENTITY();

END