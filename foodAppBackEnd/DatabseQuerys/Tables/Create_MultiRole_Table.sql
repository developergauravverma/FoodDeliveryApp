
create table tbl_UserWithRole
(
	id uniqueidentifier not null primary key default(newId()),
	userId uniqueidentifier not null,
	RoleId uniqueidentifier not null
)