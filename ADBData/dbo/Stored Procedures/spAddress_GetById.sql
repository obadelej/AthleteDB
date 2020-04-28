CREATE PROCEDURE [dbo].[spAddress_GetById]
@Id int
AS
Begin
	set Nocount on;

	Select Id, Street1, Street2, Town, Parish, Country
	From dbo.[Address]
	Where Id = @Id;
End