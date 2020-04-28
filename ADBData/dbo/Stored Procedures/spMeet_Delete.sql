CREATE PROCEDURE [dbo].[spMeet_Delete]
@Id int
As
Begin
	set nocount on;

	Delete 
	From dbo.[Meet]
	Where Id = @Id;

end