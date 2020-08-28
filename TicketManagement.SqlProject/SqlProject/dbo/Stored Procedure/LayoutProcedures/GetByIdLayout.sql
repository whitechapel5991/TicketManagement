CREATE PROCEDURE [dbo].[GetByIdLayout]
@Id int
AS
    SELECT * FROM Layouts
    WHERE Id=@Id
