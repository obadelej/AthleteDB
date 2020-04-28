CREATE PROCEDURE [dbo].[spSchool_Delete]
@Id int
As
Begin
	set nocount on;

	Delete 
	From dbo.[School]
	Where Id = @Id;

end