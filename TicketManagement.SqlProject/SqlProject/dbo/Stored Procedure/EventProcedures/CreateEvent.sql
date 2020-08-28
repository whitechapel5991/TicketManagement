CREATE PROCEDURE [dbo].[CreateEvent]
	@Name nvarchar(50),
    @Description nvarchar(50),
    @LayoutId int
AS
    INSERT INTO Events (Name, Description, LayoutId)
    VALUES (@Name, @Description, @LayoutId)
    SELECT SCOPE_IDENTITY()
