CREATE PROCEDURE [dbo].[CreateLayout]
	@Description nvarchar(50),
    @VenueId int
AS
    INSERT INTO Layouts (Description, VenueId)
    VALUES (@Description, @VenueId)
    SELECT SCOPE_IDENTITY()
