CREATE PROCEDURE [dbo].[CreateEventSeat]
    @Row int,
    @Number int,
    @State int,
    @EventAreaId int,
    @EventSeatId int Output
AS
    INSERT INTO EventSeats (Row, Number, State, EventAreaId)
    VALUES (@Row, @Number, @State, @EventAreaId)
    set @EventSeatId = SCOPE_IDENTITY()
