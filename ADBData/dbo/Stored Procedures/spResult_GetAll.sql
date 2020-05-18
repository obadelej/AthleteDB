CREATE PROCEDURE [dbo].[spResult_GetAll]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT r.Id AS Id, r.AthleteId AS AthleteId, r.MeetId AS MeetId, r.EventId AS EventId, r.Mark AS Mark, r.Wind As Wind,
	r.PerfDate AS PerfDate, m.MeetName AS MeetName, m.[Location] AS [Location], m.StartDate AS StartDate, 
	m.EndDate AS EndDate, e.EventName AS EventName, a.FirstName AS FirstName, a.LastName AS LastName
	FROM dbo.[Result] r
	INNER JOIN dbo.Athlete a ON a.Id = r.AthleteId
	INNER JOIN dbo.Meet AS m ON m.Id = r.MeetId
	INNER JOIN dbo.[Event] AS e ON e.Id = r.EventId
	ORDER BY e.EventCode, Mark;
END