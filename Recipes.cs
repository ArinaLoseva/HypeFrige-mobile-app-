using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Recipes
    {
        [PrimaryKey, AutoIncrement]
        public int recipe_id { get; set; }
        public string recipe_name { get; set; }
        public int recipe_time { get; set; }
        public string recipe_ingridients { get; set; }
        public string recipe_img { get; set; }
        public string recipe_text { get; set; }
    }
}
