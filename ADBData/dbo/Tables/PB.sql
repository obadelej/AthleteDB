CREATE TABLE [dbo].[PB]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NULL, 
    [EventId] INT NULL, 
    [PBDate] DATETIME2 NULL, 
    [Mark] INT NULL, 
    [Location] VARCHAR(50) NULL
)
