using App1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public class DB
    {
        public readonly ISQLiteConnection conn;

        public DB(string path)
        {
            conn = new SQLiteConnection(path);
            conn.CreateTable<Users>();
            conn.CreateTable<Products>();
            conn.CreateTable<Recipes>();
            conn.CreateTable<Notes>();
        }

        public List<Users> GetUseres()
        {
            return conn.Table<Users>().ToList();
        }

        public int SaveUser(Users user)
        {
            return conn.Insert(user);
        }

        public List<Products> GetProducts()
        {
            return conn.Table<Products>().ToList();
        }

        public int SaveProduct(Products product)
        {
            return conn.Insert(product);
        }

        public List<Recipes> GetRecipes()
        {
            return conn.Table<Recipes>().ToList();
        }

        public int SaveRecipe(Recipes recipe)
        {
            return conn.Insert(recipe);
        }

        public List<Notes> GetNotes()
        {
            return conn.Table<Notes>().ToList();
        }

        public int SaveNote(Notes note)
        {
            return conn.Insert(note);
        }
    }
}
