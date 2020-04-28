CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[FirstName] VARCHAR(50),
	[LastName] VARCHAR(50),
	[BirthDate] DATETIME2,
	[Gender] VARCHAR(50), 
	[Phone] VARCHAR(50) NULL, 
	[Email] VARBINARY(100) NULL, 
    [AddressId] INT NULL,    
    [ParentId] INT NULL, 
    [CoachId] INT NULL, 
    [SchoolId] INT NULL, 
   
    

)
