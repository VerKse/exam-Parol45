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
        static MySqlConnection connection;
        const string host = "46.173.214.207"; // поменять на localhost
        const string user = "client"; // поменять на root 
        const string password = "12345a";
        public static void Initialize()
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
            initializator.CommandText = "select name from rooms;";
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
                        "create table if not exists `" + chatRooms[i] + "_hist`(message nvarchar(1000) not null, dt datetime(6) not null primary key);";
                    initializator.ExecuteNonQuery();
                    Console.Write(chatRooms[i] + (i == chatRooms.Count - 1 ? "" : ", "));
                    ServerEngine.rooms.Add(new Room(chatRooms[i]));
                }
                Console.WriteLine(".");
            }
            else
            {
                selection.Close();
                initializator.CommandText = "insert into rooms(name) values('Фаны Валакаса');";
                initializator.ExecuteNonQuery();
                initializator.CommandText = "create table if not exists `Фаны Валакаса_hist`(message nvarchar(1000) not null, dt datetime(6) not null primary key);";
                initializator.ExecuteNonQuery();
                Console.WriteLine("There are no rooms. Inserted default one and created hist table for it.");
            }
        }
        public static List<string> GetRoomList()
        {
            List<string> result = new List<string>();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "select name from rooms;";
            MySqlDataReader roomlist = query.ExecuteReader();
            while (roomlist.Read())
                result.Add(roomlist.GetString(0));
            roomlist.Close();
            return result;
        }
        // Изменить сигнатуру для собственного соединения у клиента
        public static void SaveMessage(string message, string roomName)
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "insert into `" + roomName + "_hist`(message, dt) values('" + message.Replace("'", "\'") + "', sysdate());";
            query.ExecuteNonQuery();
        }
        public static List<string> GetHistory(string roomName)
        {
            List<string> hist = new List<string>();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "select message from `" + roomName + "_hist` order by dt;";
            MySqlDataReader selection = query.ExecuteReader();
            while (selection.Read())
                hist.Add(selection.GetString(0));
            selection.Close();
            return hist;
        }
        public static void CloseConnection()
        {
            connection.Close();
        }
    }
}
