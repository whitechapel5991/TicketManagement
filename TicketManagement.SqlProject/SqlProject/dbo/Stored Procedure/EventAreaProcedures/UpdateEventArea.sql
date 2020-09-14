CREATE PROCEDURE [dbo].[UpdateEventArea]
	@Id int,
	@Description nvarchar(50),
    @CoordX int,
    @CoordY int,
	@Price decimal,
    @EventId int
AS
    UPDATE EventAreas set Price=@Price
	where Id = @Id;
