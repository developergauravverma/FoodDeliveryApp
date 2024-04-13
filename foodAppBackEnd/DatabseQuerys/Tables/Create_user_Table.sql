
create table tbl_User
(
	id uniqueidentifier not null primary key default(newId()),
	userName nvarchar(100),
	firstName nvarchar(100),
	lastName nvarchar(100),
	emailId nvarchar(1000),
	[password] nvarchar(1000),
	[address] nvarchar(2000),
	contactNo nvarchar(50),
	IsActive bit default(1)
)