CREATE PROCEDURE [dbo].[DeleteEvent]
		@Id int
AS
	BEGIN TRAN
    Begin try
		delete from Events where Id=@Id;
 commit tran
    end try
    begin catch
        rollback tran
    end catch