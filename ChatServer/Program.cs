using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatServer.lib;
using MySql.Data.MySqlClient;

namespace ChatServer
{
    class Program
    {
        static ChatRoomClass server = new ChatRoomClass("Фаны Валакаса");
        static MySqlConnection mysql_connection;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("ver 0.0.5");
                // Подключаемся и создаём объекты в бд.
                DBmanager.Initialize(ref mysql_connection);
                // Ждём пользователей.
                server.Listen();
                mysql_connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
