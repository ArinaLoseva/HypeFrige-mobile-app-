using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int product_id { get; set; }
        public string user_login { get; set; }
        public string product_name { get; set; }
        public string product_type { get; set; }
        public string product_place { get; set; }
        public string product_date { get; set; }
    }
}
