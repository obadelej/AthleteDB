CREATE PROCEDURE [dbo].[spParent_Delete]
@Id int
As
Begin
	set nocount on;

	Delete 
	From dbo.[Parent]
	Where Id = @Id;

end