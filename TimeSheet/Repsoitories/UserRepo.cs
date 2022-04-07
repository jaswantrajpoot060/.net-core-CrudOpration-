using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TimeSheet.Models;

namespace TimeSheet.Repsoitories
{
    public class UserRepo
    {
        private readonly string _connectionString;
        public UserRepo(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConncetion");
        }

        public async Task<List<UserModels>> GetAll()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("getAllUsers", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<UserModels>();
                    await con.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }
                    return response;
                }
            }
        }



        public async Task<UserModels> GetById(int Id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UserByID", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", Id));

                    UserModels response = null;
                    await con.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task Insert(UserModels usermodels)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("AddUser", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UserName", usermodels.UserName));
                    cmd.Parameters.Add(new SqlParameter("@Password", usermodels.Password));
                    cmd.Parameters.Add(new SqlParameter("@EmailID", usermodels.EmailID));
                    cmd.Parameters.Add(new SqlParameter("@RoleId", usermodels.RoleId));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", usermodels.CreatedDate));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedDate", usermodels.UpdatedDate));
                    cmd.Parameters.Add(new SqlParameter("@IsActive", usermodels.IsActive));
                    
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Update(UserModels usermodels)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateUsers", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", usermodels.ID));
                    cmd.Parameters.Add(new SqlParameter("@UserName", usermodels.UserName));
                    cmd.Parameters.Add(new SqlParameter("@Password", usermodels.Password));
                    cmd.Parameters.Add(new SqlParameter("@EmailID", usermodels.EmailID));
                    cmd.Parameters.Add(new SqlParameter("@RoleId", usermodels.RoleId));
                    cmd.Parameters.Add(new SqlParameter("@CreatedDate", usermodels.CreatedDate));
                    cmd.Parameters.Add(new SqlParameter("@UpdatedDate", usermodels.UpdatedDate));
                    cmd.Parameters.Add(new SqlParameter("@IsActive", usermodels.IsActive));

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        public async Task DeleteById(int Id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", Id));

                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
        private UserModels MapToValue(SqlDataReader reader)
        {
            return new UserModels()
            {
                ID = (int)reader["ID"],
                UserName = (string)reader["UserName"],
                Password = (string)reader["Password"],
                EmailID = (string)reader["EmailID"],
                RoleId = (int)reader["RoleId"],
                CreatedDate = (DateTime)reader["CreatedDate"],
                UpdatedDate = (DateTime)reader["UpdatedDate"],
                IsActive = (bool)reader["IsActive"],
            };
        }
    }
}
