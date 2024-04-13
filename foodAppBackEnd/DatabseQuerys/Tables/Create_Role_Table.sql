
create table tbl_Role
(
	id uniqueidentifier not null primary key default(newId()),
	roleName nvarchar(100),
	isActive bit default(1)
)