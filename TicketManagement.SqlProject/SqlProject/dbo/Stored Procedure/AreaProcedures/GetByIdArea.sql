CREATE PROCEDURE [dbo].[GetByIdArea]
@Id int
AS
    SELECT * FROM Areas
    WHERE Id=@Id