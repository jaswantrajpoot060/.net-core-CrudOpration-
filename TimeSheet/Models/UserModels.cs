using System;
using System.Data;

namespace TimeSheet.Models
{
    public class UserModels
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string error { get; set; }

    }
}
