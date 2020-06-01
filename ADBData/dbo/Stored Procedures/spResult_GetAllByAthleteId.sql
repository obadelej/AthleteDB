CREATE PROCEDURE [dbo].[spResult_GetAllByAthleteId]
	@AthleteId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT a.Id as AthleteId, a.FirstName AS FirstName, a.LastName AS LastName, a.BirthDate AS BirthDate, a.IsMale AS Male,a.Phone AS Phone, a.Email AS Email,
	p.FirstName AS ParentName, p.LastName AS ParentSurname, p.Phone AS ParentPhone, p.Email AS ParentEmail,
	ad.Street1 AS Street1, ad.Street2 AS Street2, ad.Town AS Town, ad.Parish AS Parish, ad.Country AS Country,
	s.SchoolName AS School, s.Phone AS SchoolPhone, s.[Location] AS SchoolLocation,
	c.FirstName AS CoachName, c.LastName AS CoachSurname, c.Phone AS CoachPhone, c.Email AS CoachEmail,
	m.MeetName AS MeetName, m.StartDate AS StartDate, m.EndDate AS EndDate, m.[Location] AS MeetLocation,
	e.EventName AS EventName,
	r.Mark AS EventMark, r.Wind AS Wind, r.PerfDate AS PerfDate	
	FROM dbo.Result r
	inner join dbo.[Event] e on e.Id=r.EventId
	inner join dbo.Meet m on m.Id=r.MeetId
	inner join dbo.Athlete a 
		Join dbo.Parent p ON p.Id=a.ParentId
		JOIN dbo.[Address] ad ON ad.Id=a.AddressId
		JOIN dbo.Coach c ON c.Id=a.CoachId
		JOIN dbo.School s ON s.Id=a.SchoolId
		ON a.Id=r.AthleteId
	WHERE r.AthleteId=@AthleteId;
END