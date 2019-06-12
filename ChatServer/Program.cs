using System;
using ChatServer.lib;

namespace ChatServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("ver 0.1.2");
            // Создаём возможно отсутствующие объекты в бд.
            DBmanager.Initialize();
            // Ждём пользователей в бесконечном цикле.
            ServerEngine.Listen();
        }
    }
}
