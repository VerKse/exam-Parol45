using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatServer.lib;
using MySql.Data.MySqlClient;

namespace ChatServer
{
    class Program
    {
        static ServerEngine server = new ServerEngine("Фаны Валакаса");

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("ver 0.0.6");
                // Подключаемся и создаём объекты в бд.
                DBmanager.Initialize();
                // Ждём пользователей.
                server.Listen();
                DBmanager.CloseConnection();
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
