CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[FirstName] VARCHAR(50),
	[LastName] VARCHAR(50),
	[BirthDate] DATETIME2,
	[Gender] VARCHAR(50), 
    [Status] INT NULL, 
    [AddressId] INT NULL, 
    [Phone] VARCHAR(50) NULL, 
    [ParentId] INT NULL, 
    [Email] VARBINARY(100) NULL 
    

)
