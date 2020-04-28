CREATE TABLE [dbo].[School]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SchoolName] NVARCHAR(150) NULL, 
    [SchoolCode] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [Location] NVARCHAR(150) NULL
)
