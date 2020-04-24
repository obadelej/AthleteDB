CREATE TABLE [dbo].[Event]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [EventName] VARCHAR(50) NULL, 
    [EventCode] VARCHAR(50) NULL, 
    [Description] VARCHAR(200) NULL
)
