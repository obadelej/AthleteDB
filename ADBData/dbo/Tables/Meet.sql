CREATE TABLE [dbo].[Meet]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MeetName] VARCHAR(150) NULL, 
    [StartDate] DATETIME2 NULL, 
    [EndDate] DATETIME2 NULL, 
    [Location] VARCHAR(100) NULL
)
