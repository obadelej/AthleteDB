CREATE PROCEDURE [dbo].[spEvent_Insert]
@Id int=0 output,
@EventName nvarchar(50),
@EventCode nvarchar(50)
AS
BEGIN
	set nocount on;

	Insert Into dbo.[Event](EventName, EventCode)
	Values(@EventName, @EventCode);

	select @Id = SCOPE_IDENTITY();

END