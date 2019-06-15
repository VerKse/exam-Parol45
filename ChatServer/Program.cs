using System;
using ChatServer.lib;

namespace ChatServer
{
    class Program
    {
        // TODO: ещё одна форма для входа.
        // TODO: шифрование.
        // TODO: регистрация пользователей в бд.
        // TODO: удаление комнат.
        // TODO: звук прихода сообщения.
        // TODO: переписать под ридеры.
        // TODO: добавление комнат из комнаты + меньше вызовов в загрузке истории.
        static void Main(string[] args)
        {
            Console.WriteLine("ver 0.3.3");
            // Создаём возможно отсутствующие объекты в бд.
            DBmanager.Initialize();
            // Ждём пользователей в бесконечном цикле.
            ServerEngine.Listen();
        }
    }
}
