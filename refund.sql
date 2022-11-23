use GameStore
select tmp.UserID,tmp.ProductID,Price,lastestDate
from OrderDetail,[Order],
(select UserID,ProductID,max(DatePurchase) as lastestDate
from [Order],OrderDetail
where [Order].Id=OrderDetail.Id
group by UserID,ProductID)as tmp
where OrderDetail.Id=[Order].Id and [Order].DatePurchase=tmp.lastestDate
and tmp.UserID=[Order].UserID and tmp.ProductID=OrderDetail.ProductID and DATEDIFF(DAY,DatePurchase,GETDATE())<7

select * 
from [Order],OrderDetail
where [Order].Id=OrderDetail.Id

select UserID,ProductID,max(DatePurchase) as lastestDate
from [Order],OrderDetail
where [Order].Id=OrderDetail.Id
group by UserID,ProductID
create proc listgameRefund @userID int
as
begin
	select UserID,ProductID,Name,max(DatePurchase) as DatePurchase
	from [Order],OrderDetail,Product
	where [Order].Id=OrderDetail.Id and Product.Id=OrderDetail.ProductID and UserID=@userID
	group by UserID,ProductID,Name
	having DATEDIFF(day,max(DatePurchase),GETDATE())<=7 
end
exec listgameRefund 2
set dateformat dmy
