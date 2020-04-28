CREATE PROCEDURE [dbo].[spEvent_GetById]
	@Id int
	AS
	Begin
		set Nocount on;

		Select Id, EventName, EventCode
		From dbo.[Event]
		Where Id = @Id;
	End