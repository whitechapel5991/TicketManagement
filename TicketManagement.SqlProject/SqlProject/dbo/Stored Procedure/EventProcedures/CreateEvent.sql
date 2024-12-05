CREATE PROCEDURE [dbo].[CreateEvent]
	@Name nvarchar(50),
    @Description nvarchar(50),
    @LayoutId int,
    @BeginDate datetime,
    @EndDate datetime
AS
    declare @AreaTemp table (
    Id int, 
    LayoutId int, 
    Description nvarchar(200), 
    CoordX int, 
    CoordY int,
    RowId int not null identity(1,1));

    declare @SeatTemp table (
    Id int, 
    AreaId int, 
    Row int, 
    Number int,
    RowId int not null identity(1,1));

    DECLARE @AreaRows       int
    DECLARE @SeatRows       int
    DECLARE @AreaCurrentRow  int
    DECLARE @SeatCurrentRow  int
    DECLARE @SelectCol1     int

    BEGIN TRAN
    Begin try
        Insert into @AreaTemp (Id, LayoutId, Description, CoordX, CoordY)
        select Id, LayoutId, Description, CoordX, CoordY
        from dbo.Areas
        where LayoutId = @LayoutId;
        set @AreaRows=@@ROWCOUNT;

        Insert into @SeatTemp (Id, AreaId, Row, Number)
        select dbo.Seats.Id, AreaId, Row, Number
        from @AreaTemp
        join dbo.Seats on [@AreaTemp].Id = dbo.Seats.AreaId
        set @SeatRows=@@ROWCOUNT;

        declare @eventId int;
        INSERT INTO Events (Name, Description, LayoutId, BeginDate, EndDate)
        VALUES (@Name, @Description, @LayoutId, @BeginDate, @EndDate)
        set @eventId = SCOPE_IDENTITY();

        set @AreaCurrentRow=0
        while @AreaCurrentRow < @AreaRows
        begin
            set @AreaCurrentRow=@AreaCurrentRow+1
            declare @descriptionParam nvarchar(50), @coordXParam int, @coordYParam int, @priceParam decimal(18,0), @areaIdParam int, @eventAreaIdParam int;
            select top 1 @descriptionParam=Description, @coordXParam=CoordX, @coordYParam=CoordY, @areaIdParam=Id
            from @AreaTemp
            where RowId = @AreaCurrentRow;
            set @priceParam = 0;
            exec dbo.CreateEventArea @descriptionParam, @coordXParam, @coordYParam, @priceParam, @eventId, @eventAreaIdParam OUTPUT;



            declare @SeatIdParam int, @RowParam int, @numberParam int, @eventSeatIdOutput int;
            declare Seat_Cursor cursor for
            (select Id, Row, Number
                from @SeatTemp
                where AreaId = @areaIdParam);

            open Seat_Cursor
            FETCH NEXT FROM Seat_Cursor INTO
    @SeatIdParam, @RowParam, @numberParam

            while @@FETCH_STATUS = 0
            begin
                exec dbo.CreateEventSeat @RowParam, @numberParam, 0, @eventAreaIdParam, @eventSeatIdOutput output;
               
               FETCH NEXT FROM Seat_Cursor INTO
                        @SeatIdParam, @RowParam, @numberParam
            end
            CLOSE Seat_Cursor
            DEALLOCATE Seat_Cursor
        end

        commit tran
        return @eventId
    end try
    begin catch
        return -1
        rollback tran
    end catch
