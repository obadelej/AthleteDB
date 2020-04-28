CREATE PROCEDURE [dbo].[spEvent_Delete]
@Id int
As
Begin
	set nocount on;

	Delete 
	From dbo.[Event]
	Where Id = @Id;

end