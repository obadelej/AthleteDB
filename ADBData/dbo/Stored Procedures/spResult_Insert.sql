CREATE PROCEDURE [dbo].[spResult_Insert]
@Id INT=0 OUTPUT,
@AthleteId INT,
@MeetId INT,
@EventId INT,
@Mark INT,
@PerfDate DATETIME2
AS
BEGIN
	SET nocount on;
	INSERT INTO dbo.[Result](AthleteId, MeetId, EventId, Mark, PerfDate)
	VALUES(@AthleteId, @MeetId, @EventId, @Mark, @PerfDate);

	SELECT @Id=SCOPE_IDENTITY();
END