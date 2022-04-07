using System;
using System.Data;

namespace TimeSheet.Models
{
    public class UserLogin
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public int RoleId { get; set; }


        #region Method
        public UserLogin()
            : base()
        {
        }


        public UserLogin(IDataReader reader)
        {
            ID = DBNull.Value != reader["ID"] ? (int)reader["ID"] : default;
            UserName = DBNull.Value != reader["UserName"] ? (string)reader["UserName"] : default;
            EmailID = DBNull.Value != reader["EmailID"] ? (string)reader["EmailID"] : default;
            RoleId = DBNull.Value != reader["RoleId"] ? (int)reader["RoleId"] : default;
        }
        #endregion
    }
}
