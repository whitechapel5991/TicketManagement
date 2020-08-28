CREATE PROCEDURE [dbo].[CreateVenue]
	@Description nvarchar(50),
    @Address nvarchar(50),
    @Phone nvarchar(50)
AS
    INSERT INTO Venues (Description, Address, Phone)
    VALUES (@Description, @Address, @Phone)
    SELECT SCOPE_IDENTITY()
