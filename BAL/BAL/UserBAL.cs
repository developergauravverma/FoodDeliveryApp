
using BAL.IBAL;
using DAL.DataProvider;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.BAL
{
    public class UserBAL : IUserBAL
    {
        IDataProvider _provider;
        public async Task<User> UserRegister(User user)
        {
            DataTable dt = new DataTable();
            _provider = new DataProvider();
            Collection<SqlParameter> parameters = new Collection<SqlParameter>()
            {
                new SqlParameter("@userName", user.userName),
                new SqlParameter("@firstName", user.firstName),
                new SqlParameter("@lastName", user.lastName),
                new SqlParameter("@emailId", user.emailId),
                new SqlParameter("@password", user.password),
                new SqlParameter("@address", user.address),
                new SqlParameter("@contactNo", user.contactNo),
                new SqlParameter("@userRole", user.roleId)
            };
            dt = await _provider.ConnectDataBaseWithParam(parameters, "sp_InsertUserData");
            User u = new User();

            if(dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                u.roleIdLst = (Convert.IsDBNull(dr["role"]) ? default : dr["role"]?.ToString()?.Split(',').ToList()) ?? new List<string>();

                u.id = Convert.IsDBNull(dr["id"]) ? default : Guid.Parse(dr["id"].ToString() ?? string.Empty);
                u.userName = (Convert.IsDBNull(dr["userName"]) ? default : Convert.ToString(dr["userName"])) ?? string.Empty;
                u.firstName = (Convert.IsDBNull(dr["firstName"]) ? default : Convert.ToString(dr["firstName"])) ?? string.Empty;
                u.lastName = (Convert.IsDBNull(dr["lastName"]) ? default : Convert.ToString(dr["lastName"])) ?? string.Empty;
                u.emailId = (Convert.IsDBNull(dr["emailId"]) ? default : Convert.ToString(dr["emailId"])) ?? string.Empty;
                u.password = (Convert.IsDBNull(dr["password"]) ? default : Convert.ToString(dr["password"])) ?? string.Empty;
                u.address = (Convert.IsDBNull(dr["address"]) ? default : Convert.ToString(dr["address"])) ?? string.Empty;
                u.contactNo = (Convert.IsDBNull(dr["contactNo"]) ? default : Convert.ToString(dr["contactNo"])) ?? string.Empty;
                u.roleId = (Convert.IsDBNull(dr["role"]) ? default : Convert.ToString(dr["role"])) ?? string.Empty;
            }

            return u;
        }
    }
}
