CREATE PROCEDURE [dbo].[spAddress_Update]
@Id int,
@Street1 NVARCHAR(150),
@Street2 NVARCHAR(150),
@Town NVARCHAR(50),
@Parish NVARCHAR(50),
@Country NVARCHAR(50)
AS
Begin
	set nocount on;
		
	Update dbo.[Address]
	Set
	Street1 = @Street1,
	Street2 = @Street2,
	Town = @Town,
	Parish = @Parish,
	Country = @Country
	Where Id = @Id;
end