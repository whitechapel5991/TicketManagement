CREATE PROCEDURE [dbo].[UpdateEventSeat]
	@Id int,
	@Row int,
    @Number int,
    @State int,
    @EventAreaId int
AS
    UPDATE EventSeats set State=@State
	where Id = @Id;
