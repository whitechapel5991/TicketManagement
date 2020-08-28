CREATE PROCEDURE [dbo].[UpdateEvent]
	@Id int,
	@Name nvarchar(50),
    @Description nvarchar(50),
    @LayoutId int
AS
    UPDATE Events set Name=@Name, Description=@Description, LayoutId=@LayoutId
	where Id = @Id
