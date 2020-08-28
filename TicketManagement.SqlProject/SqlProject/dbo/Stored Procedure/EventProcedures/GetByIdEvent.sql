CREATE PROCEDURE [dbo].[GetByIdEvent]
@Id int
AS
    SELECT * FROM Events
    WHERE Id=@Id