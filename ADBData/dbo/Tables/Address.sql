CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Street1] NVARCHAR(150) NULL, 
    [Street2] NVARCHAR(150) NULL, 
    [Town] NVARCHAR(50) NULL, 
    [Parish] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL
)
