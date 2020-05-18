CREATE PROCEDURE [dbo].[spResult_Update]
@Id INT,
@AthleteId INT,
@MeetId INT,
@EventId INT,
@Mark FLOAT,
@Wind NVARCHAR(50),
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
	Wind = @Wind,
	PerfDate = @PerfDate
	WHERE Id = @Id;
END