use EmployeeDB


create table EmployeeInfoStore(
	EmpId int primary key identity,
	Name varchar(200),
	username nvarchar(50),
	password nvarchar(50),
	Address nvarchar(500),
	Phone nvarchar(50),
	Email nvarchar(50),
	TotalSalary decimal,
	empCode AS 'EMP' + RIGHT('000' + CAST(EmpId AS VARCHAR(10)), 3) PERSISTED UNIQUE
)

drop table EmployeeInfoStore





create table Signup (
	Id int primary key identity,
	username varchar(100),
	password varchar(100),
	email varchar(100)
)

