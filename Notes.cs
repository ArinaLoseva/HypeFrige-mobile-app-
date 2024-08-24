using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Notes
    {
        [PrimaryKey, AutoIncrement]
        public int note_id { get; set; }
        public string user_login { get; set; }
        public string note_name { get; set; }
        public string note_text { get; set; }
    }
}
