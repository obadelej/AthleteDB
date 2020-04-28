CREATE PROCEDURE [dbo].[spEvent_Update]
	@Id int,
	@EventName nvarchar(50),
	@EventCode nvarchar(50)
	AS
	Begin
		set nocount on;
		
		Update dbo.[Event]
		Set
		EventName = @EventName,
		EventCode = @EventCode
		Where Id = @Id;
	end