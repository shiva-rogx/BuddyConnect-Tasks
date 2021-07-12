create database Products
use Products

create table Product(
ProductID int identity(1,1),ProductName varchar(20) not null,
Description varchar(50) not null, Price money not null,
DiscountPercentage float not null,constraint pk_PID primary key (ProductID)
);
create table Customer(
CustomerID int identity(10,1),CustomerName varchar(20) not null,
Address varchar(50) not null, ContactNumber varchar(10) not null,
constraint pk_CID primary key (CustomerID)
);

create table PurchaseOrder(
POID int identity(100,1), CustomerID int not null,
ProductID int not null, DateOfPurchase Date not null,
Amount money not null, constraint pk_POID primary key (POID)
);

create table PurchaseOrderDetail( PODID int identity(1000,1),
POID int not null, ProductID int not null,
Price money not null, Quantity int not null,
constraint pk_PODID primary key (PODID)
);

select * from Product p join PurchaseOrderDetail pod on p.ProductID=pod.ProductID
join PurchaseOrder po on po.POID=pod.POID
join Customer c on c.CustomerID=po.CustomerID

select MONTH ( DateOfPurchase) Month,CustomerName,Amount
from PurchaseOrder po 
join Customer c on c.CustomerID=po.CustomerID

select  MONTH ( DateOfPurchase) Month,ProductName,Quantity
from Product p join PurchaseOrder po on p.ProductID=po.ProductID
join PurchaseOrderDetail pod on po.POID=pod.POID
order by ProductName

select  MONTH ( DateOfPurchase) Month,MAX(Price) MaxPriceMonthWise,po.POID
from PurchaseOrder po 
join PurchaseOrderDetail pod on po.POID=pod.POID
where po.POID in (select POID from PurchaseOrderDetail pod1 
				  where Price in
						(select MAX(Price) from PurchaseOrderDetail pod2 
						join PurchaseOrder po1 on po1.POID=pod2.POID 
						group by MONTH ( po1.DateOfPurchase)))
group by MONTH ( DateOfPurchase),po.POID

select  MONTH ( DateOfPurchase) Month,MAX(Price) MaxPriceMonthWise,po.POID
from PurchaseOrder po 
join PurchaseOrderDetail pod on po.POID=pod.POID
group by po.POID,MONTH ( DateOfPurchase)
