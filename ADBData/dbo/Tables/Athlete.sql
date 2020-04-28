CREATE TABLE [dbo].[Athlete]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,	
    [FirstName] varchar(50),
    [LastName] varchar(50),
    [Birthdate] DATETIME2,
    [Gender] varchar(50),
    [Phone] VARCHAR(50),
    [Email] VARCHAR(50), 
	[AddressId] INT,
	[ParentId] INT,
	[CoachId] INT,
	[SchoolId] INT    
)
