CREATE TABLE [dbo].[Athlete]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,	
    [FirstName] NVARCHAR(50),
    [LastName] NVARCHAR(50),
    [BirthDate] DATETIME2,
    [IsMale] BIT,
    [Phone] NVARCHAR(50),
    [Email] NVARCHAR(150), 
	[AddressId] INT,
	[ParentId] INT,
	[SchoolId] INT,
	[CoachId] INT, 
    CONSTRAINT [FK_Athlete_ToAddress] FOREIGN KEY ([AddressId]) REFERENCES [Address]([Id]), 
    CONSTRAINT [FK_Athlete_ToParent] FOREIGN KEY ([ParentId]) REFERENCES [Parent]([Id]), 
    CONSTRAINT [FK_Athlete_ToSchool] FOREIGN KEY ([SchoolId]) REFERENCES [School]([Id]), 
    CONSTRAINT [FK_Athlete_ToCoach] FOREIGN KEY ([CoachId]) REFERENCES [Coach]([Id])
	    
)
