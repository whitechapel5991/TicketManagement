CREATE PROCEDURE [dbo].[GetByIdEventSeat]
@Id int
AS
    SELECT * FROM EventSeats
    WHERE Id=@Id
