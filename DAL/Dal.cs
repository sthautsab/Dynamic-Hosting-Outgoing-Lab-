using OutGoingLab.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OutGoingLab.DAL
{
    public class Dal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectLab"].ConnectionString);

        public List<Outgoinglab> GetData()
        {
            List<Outgoinglab> data = new List<Outgoinglab>();
            using (con)
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_OutGoingLabRead", con);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        object detailValue = row["Details"];
                        string details = detailValue == DBNull.Value ? null : (string)detailValue;
                        object emailValue = row["Email"];
                        string email = emailValue == DBNull.Value ? null : (string)emailValue;
                        object contactpersonValue = row["ContactPerson"];
                        string contactperson = contactpersonValue == DBNull.Value ? null : (string)contactpersonValue;

                        data.Add(new Outgoinglab()
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            Email = email,
                            MobileNo = (string)row["MobileNo"],
                            Name = (string)row["Name"],
                            Location = (string)row["Location"],
                            ContactPerson = contactperson,
                            CreatedBy = (int)row["CreatedBy"],
                            CreatedDate = (DateTime)row["CreatedDate"],
                            Details = details,
                            IsSync_Bit = (bool)row["IsSync-Bit"],
                            IsActive_Bit = (bool)row["IsActive-Bit"]

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

        public int CreateData(Outgoinglab re)
        {
            if (re == null)
            {
                throw new InvalidExpressionException("Empty Object");
            }
            using (con)
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_OutGoingLabCreate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MobileNo", re.MobileNo);
                cmd.Parameters.AddWithValue("@Location", re.Location);
                cmd.Parameters.AddWithValue("@Name", re.Name);
                cmd.Parameters.AddWithValue("@CreatedDate", re.CreatedDate);
                cmd.Parameters.AddWithValue("@CreatedBy", re.CreatedBy);
                cmd.Parameters.AddWithValue("@IsSync_Bit", re.IsSync_Bit);
                cmd.Parameters.AddWithValue("@IsActive_Bit", re.IsActive_Bit);
                object parameterValueDetails = re.Details;
                if (string.IsNullOrEmpty(re.Details))
                    parameterValueDetails = DBNull.Value;
                cmd.Parameters.AddWithValue("@Details", parameterValueDetails);

                object parameterValueEmail = re.Email;
                if (string.IsNullOrEmpty(re.Email))
                    parameterValueEmail = DBNull.Value;
                cmd.Parameters.AddWithValue("@Email", parameterValueEmail);

                object parameterValueContactPerson = re.ContactPerson;
                if (string.IsNullOrEmpty(re.ContactPerson))
                    parameterValueContactPerson = DBNull.Value;
                cmd.Parameters.AddWithValue("@ContactPerson", parameterValueContactPerson);

                SqlParameter returnParameter = new SqlParameter();
                returnParameter.ParameterName = "@id";
                returnParameter.SqlDbType = SqlDbType.Int;
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnParameter);
                con.Open();
                int x = cmd.ExecuteNonQuery();
                con.Close();
                if (x > 0)
                {
                    return (int)returnParameter.Value;
                }
                else
                {
                    return 0;
                }
            }
        }


        public int Edit(int id, Outgoinglab re)
        {
            if (re == null)
            {
                throw new InvalidExpressionException("Empty Object");
            }
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectLab"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.sp_OutGoingLabUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@MobileNo", re.MobileNo);
                cmd.Parameters.AddWithValue("@Location", re.Location);
                cmd.Parameters.AddWithValue("@Name", re.Name);
                cmd.Parameters.AddWithValue("@CreatedDate", re.CreatedDate);
                cmd.Parameters.AddWithValue("@CreatedBy", re.CreatedBy);
                cmd.Parameters.AddWithValue("@IsSync_Bit", re.IsSync_Bit);
                cmd.Parameters.AddWithValue("@IsActive_Bit", re.IsActive_Bit);

                object parameterValueDetails = re.Details;
                if (string.IsNullOrEmpty(re.Details))
                    parameterValueDetails = DBNull.Value;
                cmd.Parameters.AddWithValue("@Details", parameterValueDetails);

                object parameterValueEmail = re.Email;
                if (string.IsNullOrEmpty(re.Email))
                    parameterValueEmail = DBNull.Value;
                cmd.Parameters.AddWithValue("@Email", parameterValueEmail);

                object parameterValueContactPerson = re.ContactPerson;
                if (string.IsNullOrEmpty(re.ContactPerson))
                    parameterValueContactPerson = DBNull.Value;
                cmd.Parameters.AddWithValue("@ContactPerson", parameterValueContactPerson);

                SqlParameter returnParameter = new SqlParameter();
                returnParameter.ParameterName = "@idd";
                returnParameter.SqlDbType = SqlDbType.Int;
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnParameter);
                con.Open();
                int x = cmd.ExecuteNonQuery();
                con.Close();
                if (x > 0)
                {
                    return (int)returnParameter.Value;
                }
                else
                {
                    return 0;
                }
            }

        }


    }
}