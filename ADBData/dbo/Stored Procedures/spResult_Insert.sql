CREATE PROCEDURE [dbo].[spResult_Insert]
@Id INT=0 OUTPUT,
@AthleteId INT,
@MeetId INT,
@EventId INT,
@Mark FLOAT,
@Wind nvarchar(50),
@PerfDate DATETIME2
AS
BEGIN
	SET nocount on;
	INSERT INTO dbo.[Result](AthleteId, MeetId, EventId, Mark, Wind, PerfDate)
	VALUES(@AthleteId, @MeetId, @EventId, @Mark, @Wind, @PerfDate);

	SELECT @Id=SCOPE_IDENTITY();
END