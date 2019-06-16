using System;
using ChatServer.lib;

namespace ChatServer
{
    class Program
    {
        // TODO: шифрование.
        // TODO: регистрация пользователей в бд.
        // TODO: ещё одна форма для входа.
        // TODO: переписать под эвенты.
        static void Main(string[] args)
        {
            Console.WriteLine("ver 0.4.0");
            // Создаём возможно отсутствующие объекты в бд.
            DBmanager.Initialize();
            // Ждём пользователей в бесконечном цикле.
            ServerEngine.Listen();
        }
    }
}
