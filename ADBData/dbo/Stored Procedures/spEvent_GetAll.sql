CREATE PROCEDURE [dbo].[spEvent_GetAll]
	
AS
Begin
	set nocount on
	Select Id, EventName, EventCode
	From dbo.[Event];
	
End