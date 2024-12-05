CREATE PROCEDURE [dbo].[CreateEventArea]
	@Description nvarchar(50),
    @CoordX int,
    @CoordY int,
	@Price decimal,
    @EventId int,
    @EventAreaId int Output
AS
    INSERT INTO EventAreas (Description, CoordX, CoordY, Price, EventId)
    VALUES (@Description, @CoordX, @CoordY, @Price, @EventId)
    Set @EventAreaId = SCOPE_IDENTITY()
