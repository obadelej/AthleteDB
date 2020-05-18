CREATE TABLE [dbo].[Result]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AthleteId] INT NULL, 
    [MeetId] INT NULL, 
    [EventId] INT NULL, 
    [Mark] FLOAT NULL, 
	[Wind] NVARCHAR(50) NULL, 
    [PerfDate] DATETIME2 NULL,     
    CONSTRAINT [FK_Result_ToAthlete] FOREIGN KEY ([AthleteId]) REFERENCES [Athlete]([Id]), 
    CONSTRAINT [FK_Result_ToMeet] FOREIGN KEY ([MeetId]) REFERENCES [Meet]([Id]), 
    CONSTRAINT [FK_Result_ToEvent] FOREIGN KEY ([EventId]) REFERENCES [Event]([Id]) 
)
