create or alter procedure sp_UserLogin
	@email nvarchar(1000),
	@password nvarchar(1000)
as
begin
	select top 1 *, (select STRING_AGG(roleName,',') from tbl_Role r 
	inner join tbl_UserWithRole uwr on uwr.RoleId = r.id where uwr.userId = u.id) as [Role] 
	from tbl_User u where u.emailId = @email and u.[password] = @password
end