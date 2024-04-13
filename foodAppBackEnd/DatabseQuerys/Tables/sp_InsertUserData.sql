create or alter procedure sp_InsertUserData
	@userName nvarchar(100),
	@firstName nvarchar(100),
	@lastName nvarchar(100),
	@emailId nvarchar(1000),
	@password nvarchar(1000),
	@address nvarchar(2000),
	@contactNo nvarchar(50),
	@userRole nvarchar(1000)
as
begin
declare @insertTable table(Id uniqueidentifier)
declare @userId uniqueidentifier = null;
	insert into tbl_User (userName,firstName,lastName,emailId,[password],[address], contactNo)
	output inserted.id into @insertTable
	values (@userName,@firstName,@lastName,@emailId,@password,@address,@contactNo)

	select top 1 @userId = Id from @insertTable

	insert into tbl_UserWithRole (userId, RoleId)
	select @userId,id from tbl_Role where id in (select * from string_split(@userRole,','))

	select *, (select STRING_AGG(r.roleName,',') from tbl_Role r 
	inner join tbl_UserWithRole uwr on 
	uwr.RoleId = r.id where uwr.userId=@userId) 
	as [role] from tbl_User u where u.id = @userId and u.IsActive=1

end