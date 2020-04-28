CREATE PROCEDURE [dbo].[spAddress_Delete]
@Id int
As
Begin
	set nocount on;

	Delete 
	From dbo.[Address]
	Where Id = @Id;

end