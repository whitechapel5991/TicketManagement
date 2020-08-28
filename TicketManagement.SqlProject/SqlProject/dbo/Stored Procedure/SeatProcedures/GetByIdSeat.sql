CREATE PROCEDURE [dbo].[GetByIdSeat]
    @Id int
AS
    SELECT * FROM Seats
    WHERE Id=@Id
