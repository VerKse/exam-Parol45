using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ChatServer.lib
{
    /// <summary>
    /// Класс с методами взаимодействия с БД
    /// </summary>
    static class DBmanager
    {
        const string host = "localhost";
        const string user = "client";
        const string password = "12345a";
        public static string connectionString = "Datasource=" + host + ";User=" + user + ";Password=" + password + ";charset=utf8";
        /// <summary>
        /// Проверка при включении, что всё на месте
        /// </summary>
        public static void Initialize()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
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
                        "create table if not exists `" + chatRooms[i] + "_hist`(message nvarchar(1000) not null," +
                        " dt datetime(6) not null, id int not null auto_increment primary key);";
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
                initializator.CommandText = "create table if not exists `Фаны Валакаса_hist`(message nvarchar(1000) not null" +
                    ", dt datetime(6) not null, id int not null auto_increment primary key);";
                initializator.ExecuteNonQuery();
                Console.WriteLine("There are no rooms. Inserted default one and created hist table for it.");
            }
            connection.Close();
        }
        /// <summary>
        /// Создание подключения к БД
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "use Chat;";
            query.ExecuteNonQuery();
            return connection;
        }
        /// <summary>
        /// Выборка всех существующих комнат
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static List<string> GetRoomList(MySqlConnection connection)
        {
            List<string> result = new List<string>();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "select name from rooms order by name;";
            MySqlDataReader roomlist = query.ExecuteReader();
            while (roomlist.Read())
                result.Add(roomlist.GetString(0));
            roomlist.Close();
            return result;
        }
        /// <summary>
        /// Запись сообщения в историю сообщений комнаты
        /// </summary>
        /// <param name="message"></param>
        /// <param name="roomName"></param>
        /// <param name="connection"></param>
        public static void SaveMessage(string message, string roomName, ref MySqlConnection connection)
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "insert into `" + MySqlHelper.EscapeString(roomName) + "_hist`(message, dt) values('" 
                + MySqlHelper.EscapeString(message) + "', sysdate()); commit;";
            query.ExecuteNonQuery();
        }
        /// <summary>
        /// Выборка истории сообщений комнаты
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static List<string> GetHistory(string roomName, MySqlConnection connection)
        {
            List<string> hist = new List<string>();
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "select concat(date_format(dt, '%H:%i:%s'),'  ||  ', message) from `" 
                + MySqlHelper.EscapeString(roomName) + "_hist` order by id;";
            MySqlDataReader selection = query.ExecuteReader();
            while (selection.Read())
                hist.Add(selection.GetString(0));
            selection.Close();
            return hist;
        }
        /// <summary>
        /// Создание таблиц для комнаты
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="roomName"></param>
        public static void CreateNewRoom(ref MySqlConnection connection, string roomName)
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "insert into rooms(name) values ('" + MySqlHelper.EscapeString(roomName) + "');";
            query.ExecuteNonQuery();
            query.CommandText = "create table `" + MySqlHelper.EscapeString(roomName) + "_hist`(message nvarchar(1000) not null" +
                    ", dt datetime(6) not null, id int not null auto_increment primary key);";
            query.ExecuteNonQuery();
        }
        /// <summary>
        /// Удаление объектов комнаты
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="roomName"></param>
        public static void RemoveRoom(ref MySqlConnection connection, string roomName)
        {
            MySqlCommand query = connection.CreateCommand();
            query.CommandText = "delete from rooms where name = '" + MySqlHelper.EscapeString(roomName) + "';";
            query.ExecuteNonQuery();
            query.CommandText = "drop table `" + MySqlHelper.EscapeString(roomName) + "_hist`;";
            query.ExecuteNonQuery();
        }
    }
}
