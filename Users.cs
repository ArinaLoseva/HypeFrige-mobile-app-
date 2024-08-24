using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Users
    {
        [PrimaryKey]
        public string user_login { get; set; }
        public string user_password { get; set; }
    }
}
