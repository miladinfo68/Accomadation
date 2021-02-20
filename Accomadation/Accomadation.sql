use master
go
--drop database Accomadation
create database Accomadation
go

use Accomadation
go

--drop table [User]

create table [User](
ID int not null identity(1,1) primary key,
FullName nvarchar(100),
Email nvarchar(100) not null ,
[Password] nvarchar(100) not null ,
Gender bit not null default(0)  -- 0=man , 1=woman
)

go
create table Room(
ID int not null identity(1,1) primary key,
Places tinyint 
)
go
create table Booking(
ID int not null identity(1,1) primary key,
RoomId int not null,
UserId int not null ,
StartDate date not null ,
EndDate date not null ,
)

go

insert into Room(Places) values(1)
insert into Room(Places) values(2)
insert into Room(Places) values(3)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(5)
insert into Room(Places) values(4)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(6)
insert into Room(Places) values(1)
insert into Room(Places) values(2)
insert into Room(Places) values(3)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(5)
insert into Room(Places) values(4)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(6)
insert into Room(Places) values(1)
insert into Room(Places) values(2)
insert into Room(Places) values(3)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(5)
insert into Room(Places) values(4)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(6)
insert into Room(Places) values(1)
insert into Room(Places) values(2)
insert into Room(Places) values(3)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(5)
insert into Room(Places) values(4)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(6)
insert into Room(Places) values(1)
insert into Room(Places) values(2)
insert into Room(Places) values(3)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(5)
insert into Room(Places) values(4)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(6)
insert into Room(Places) values(1)
insert into Room(Places) values(2)
insert into Room(Places) values(3)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(5)
insert into Room(Places) values(4)
insert into Room(Places) values(3)
insert into Room(Places) values(2)
insert into Room(Places) values(6)

go

insert into [User](FullName ,Email, [Password],Gender)values(N'Oliver	Mock','mail@dd.com','123',0)
insert into [User](FullName ,Email, [Password],Gender)values(N'John Benson','mail@ee.com','123',0)
insert into [User](FullName ,Email, [Password],Gender)values(N'Amir Tataloo','mail@ff.com','123',0)
insert into [User](FullName ,Email, [Password],Gender)values(N'Milad Jalali','mail@gg.com','123',0)
insert into [User](FullName ,Email, [Password],Gender)values(N'Emma Dash','mail@aa.com','123',1)
insert into [User](FullName ,Email, [Password],Gender)values(N'Sophia Moriss','mail@bb.com','123',1)
insert into [User](FullName ,Email, [Password],Gender)values(N'Charlotte Claim','mail@cc.com','123',1)
insert into [User](FullName ,Email, [Password],Gender)values(N'Leila Arami','mail@hh.com','123',1)
insert into [User](FullName ,Email, [Password],Gender)values(N'Maedeh Zibaroo','mail@ii.com','123',1)

go

create procedure sp_AddNewBook
@userId int ,
@gender bit ,
@roomId int ,
@startDate date ,
@endDate date 
as
begin
	declare @roomCapcity tinyint=(select Places from Room where ID=@roomId)
	--we assume 
	declare @filled tinyint=(select count(1) from Booking where RoomId=@roomId and EndDate between @startDate and @endDate)	
	declare @userSex bit =(select Gender from [User] where ID=@userId)

	if(not exists(select top 1 b.ID from Booking b where b.UserId=@userId ))
	begin
		if(@roomCapcity <= @filled) select 1			--There_Is_No_Empty_Capacity
		else if(@startDate ='' or @endDate='') select 2 --StarDate_EndDate_Empty
		else if(@startDate>@endDate) select 3			--StartDate_GreaterThan_EndDate	 
		else
		begin
			insert into Booking(RoomId ,UserId ,StartDate ,EndDate)
			values(@roomId,@userId,@startDate,@endDate)
			select 4 --success
		end
	end
	else
	begin
		if(exists(select top 1 b.ID from Booking b where b.UserId=@userId and b.RoomId=@roomId and EndDate between @startDate and @endDate))
		begin
			select 5 --Current_Room_Before_Reserved
		end 
		else
		begin
			if(exists(select top 1 b.ID from Booking b where b.UserId=@userId and b.EndDate>= @startDate order by EndDate desc))
			begin
				select 6 --overlap range date (between current range date and existed in past)
			end
			else
			begin
				if(@gender = @userSex)
				begin
					insert into Booking(RoomId ,UserId ,StartDate ,EndDate)
					values(@roomId,@userId,@startDate,@endDate)
					select 4 --success
				end
				else
				begin
					select 7 --InvalidSexuality
				end
			end
		end
	end
end


go



create procedure sp_freeRooms
@date   date =null
as
begin
	--declare @date   date =''
	select t.RoomId,t.Capacity,t.Filled ,t.Capacity - t.Filled as Remain ,t.StartDate,t.EndDate
	from (
		select distinct r.ID as RoomId , r.Places as Capacity ,count(b.RoomId) as Filled,b.StartDate ,b.EndDate
		from Room r
		join Booking b on b.RoomId=r.ID
		where ((@date is null) or (@date is not null and (@date between b.StartDate and b.EndDate)))  
		group by r.ID,r.Places,b.StartDate ,b.EndDate
	) t
	where t.Capacity - t.Filled > 0

	union

	select distinct r.ID as RoomId , r.Places as Capacity ,0 as Filled ,r.Places as Remain,b.StartDate ,b.EndDate
	from Room r
	left join Booking b on b.RoomId=r.ID
	where  b.RoomId is null
	
	order by EndDate desc

end




