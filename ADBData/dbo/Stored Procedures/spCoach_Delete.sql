CREATE PROCEDURE [dbo].[spCoach_Delete]
@Id int
As
Begin
	set nocount on;

	Delete 
	From dbo.[Coach]
	Where Id = @Id;

end