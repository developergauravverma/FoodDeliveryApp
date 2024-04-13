using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DAL.DataProvider
{
    public class DataProvider : IDataProvider
    {
        private connection _con;
        private SqlConnection _connection;
        private DataTable dt;
        private SqlCommand cmd;

        public DataProvider()
        {
            _con = new connection();
            _connection = new SqlConnection(_con.GetConnectionString());
        }

        private Task<SqlCommand> StartUp(string proc)
        {
            var cmd = new SqlCommand
            {
                Connection = _connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = proc
            };
            return Task.FromResult(cmd);
        }
        public async Task<DataTable> ConnectDataBaseWithParam(Collection<SqlParameter> param, string Procuder)
        {
            dt = new DataTable();
            var command = await StartUp(Procuder);
            if(param != null)
            {
                foreach(SqlParameter p in param)
                {
                    if(p != null)
                    {
                        if(p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dt.Load(reader);
                _connection.Close();
            }
            return await Task.FromResult(dt);
        }
        public async Task<DataTable> ConnectDataBase(string Procuder)
        {
            dt = new DataTable();
            var command = await StartUp(Procuder);
            _connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            _connection.Close();
            dt.Load(reader);
            return await Task.FromResult(dt);
        }
        public async Task<bool> SaveDataEntity(Collection<SqlParameter> param, string Procuder)
        {
            bool result = false;
            var command = await StartUp(Procuder);
            if (param != null)
            {
                foreach (SqlParameter p in param)
                {
                    if (p != null)
                    {
                        if (p.Value == null)
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
                _connection.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
                _connection.Close();
            }
            return await Task.FromResult(result);
        }
    }
}
