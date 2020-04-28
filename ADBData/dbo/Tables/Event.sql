CREATE TABLE [dbo].[Event] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [EventName]   VARCHAR (50)  NULL,
    [EventCode]   VARCHAR (50)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

