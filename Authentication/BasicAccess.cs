using Basic_Authentication.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Basic_Authentication.Data_Access_Layer
{
    public class BasicAccess
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectBasic"].ConnectionString);

        public List<UserModel> GetData()
        {
            List<UserModel> data = new List<UserModel>();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("[dbo].[sp_getuser]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        data.Add(new UserModel()
                        {
                            Username = (string)dr["Username"],
                            Password = (string)dr["Password"],
                            Role = (string)dr["Role"]

                        });
                    }
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}