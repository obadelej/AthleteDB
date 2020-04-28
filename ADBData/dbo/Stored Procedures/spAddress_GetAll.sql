CREATE PROCEDURE [dbo].[spAddress_GetAll]
AS
Begin
	set nocount on
	Select Id, Street1, Street2, Town, Parish, Country
	From dbo.[Address];
END