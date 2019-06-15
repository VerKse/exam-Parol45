using System;
using ChatServer.lib;

namespace ChatServer
{
    class Program
    {
        // TODO: шифрование.
        // TODO: регистрация пользователей в бд.
        // TODO: переписать под ридеры.
        // TODO: ещё одна форма для входа.
        // TODO: звук прихода сообщения.
        // TODO: добавление комнат из комнаты.
        static void Main(string[] args)
        {
            Console.WriteLine("ver 0.3.4");
            // Создаём возможно отсутствующие объекты в бд.
            DBmanager.Initialize();
            // Ждём пользователей в бесконечном цикле.
            ServerEngine.Listen();
        }
    }
}
