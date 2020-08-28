CREATE PROCEDURE [dbo].[UpdateLayout]
	@Id int,
	@Description nvarchar(50),
    @VenueId int
AS
    UPDATE Layouts set Description=@Description, VenueId=@VenueId
	where Id = @Id
