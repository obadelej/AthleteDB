CREATE PROCEDURE [dbo].[spAthlete_Delete]
@Id int
As
Begin
	set nocount on;

	Delete 
	From dbo.[Athlete]
	Where Id = @Id;

end