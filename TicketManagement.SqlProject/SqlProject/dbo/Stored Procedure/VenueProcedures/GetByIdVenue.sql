CREATE PROCEDURE [dbo].[GetByIdVenue]
    @Id int
AS
    SELECT * FROM Venues
    WHERE Id=@Id
