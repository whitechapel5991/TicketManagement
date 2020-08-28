CREATE PROCEDURE [dbo].[GetByIdEventArea]
@Id int
AS
    SELECT * FROM EventAreas
    WHERE Id=@Id
