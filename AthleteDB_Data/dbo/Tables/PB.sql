CREATE TABLE [dbo].[PB]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NULL, 
    [EventId] INT NULL, 
    [PBDate] DATETIME2 NULL, 
    [Mark] VARCHAR(50) NULL, 
    [Location] VARCHAR(50) NULL
)
