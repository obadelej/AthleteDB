CREATE PROCEDURE [dbo].[spAddress_Insert]
@Id int=0 output,
@Street1 NVARCHAR(150),
@Street2 NVARCHAR(150),
@Town NVARCHAR(50),
@Parish NVARCHAR(50),
@Country NVARCHAR(50)
AS
BEGIN
	set nocount on;

	Insert Into dbo.[Address](Street1, Street2, Town, Parish, Country)
	Values(@Street1, @Street2, @Town, @Parish, @Country);

	select @Id = SCOPE_IDENTITY();

END