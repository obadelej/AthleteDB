CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Street1] VARCHAR(50) NULL, 
    [Street2] VARCHAR(50) NULL, 
    [Town] VARCHAR(50) NULL, 
    [City] VARCHAR(50) NULL, 
    [Country] VARCHAR(50) NULL
)
