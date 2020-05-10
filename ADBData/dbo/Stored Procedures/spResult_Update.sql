CREATE PROCEDURE [dbo].[spResult_Update]
@Id INT,
@AthleteId INT,
@MeetId INT,
@EventId INT,
@Mark INT,
@PerfDate DATETIME2
AS
BEGIN
	SET nocount on;

	UPDATE dbo.[Result]
	SET
	AthleteId = @AthleteId,
	MeetId = @MeetId,
	EventId = @EventId,
	Mark = @Mark,
	PerfDate = @PerfDate
	WHERE Id = @Id;
END