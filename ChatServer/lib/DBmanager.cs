using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ChatServer.lib
{
    static class DBmanager
    {
        const string host = "46.173.214.207"; // поменять на localhost
        const string user = "client"; // поменять на root 
        const string password = "12345a";
        public static void Initialize(ref MySqlConnection connection)
        {
            string Connect = "Datasource=" + host + ";User=" + user + ";Password=" + password + ";charset=utf8";
            connection = new MySqlConnection(Connect);
            connection.Open();
            MySqlCommand initializator = connection.CreateCommand();
            initializator.CommandText = "create database if not exists Chat; use Chat;";
            initializator.ExecuteNonQuery();
            Console.WriteLine("Database is created.");
            initializator.CommandText = "create table if not exists rooms(name nvarchar(50) not null primary key);";
            initializator.ExecuteNonQuery();
            Console.WriteLine("Table rooms is created.");
            initializator.CommandText = "select * from rooms;";
            MySqlDataReader selection = initializator.ExecuteReader();
            if (selection.HasRows)
            {
                List<string> chatRooms = new List<string>();
                while (selection.Read())
                {
                    chatRooms.Add(selection.GetString(0).Replace("`", "'"));
                }
                selection.Close();
                Console.Write("Existing rooms: "); 
                for (int i = 0; i < chatRooms.Count; i++)
                {
                    initializator.CommandText =
                        "create table if not exists `" + chatRooms[i] + "_hist`(message nvarchar(1000), id int not null primary key auto_increment);";
                    initializator.ExecuteNonQuery();
                    Console.Write(chatRooms[i] + (i == chatRooms.Count ? ", " : ""));
                }
                Console.WriteLine(".");
            }
            else
            {
                selection.Close();
                initializator.CommandText = "insert into rooms(name) values('Фаны Валакаса');";
                initializator.ExecuteNonQuery();
                initializator.CommandText = "create table if not exists `Фаны Валакаса_hist`(message nvarchar(1000), id int not null primary key auto_increment);";
                initializator.ExecuteNonQuery();
                Console.WriteLine("There are no rooms. Inserted default one and created hist table for it.");
            }
        }
    }
}
