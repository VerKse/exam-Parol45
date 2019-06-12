using System;
using ChatServer.lib;

namespace ChatServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("ver 0.1.1");
            // Подключаемся и создаём объекты в бд.
            DBmanager.Initialize();
            // Ждём пользователей в бесконечном цикле.
            ServerEngine.Listen();
            // Отключаемся от бд.
            DBmanager.CloseConnection();
        }
    }
}
