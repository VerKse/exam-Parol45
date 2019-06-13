using System;
using ChatServer.lib;

namespace ChatServer
{
    class Program
    {
        // TODO: ещё одна форма для входа.
        // TODO: шифрование.
        // TODO: регистрация пользователей в бд.
        // TODO: добавление/удаление комнат.
        // TODO: Заменить textBox на richTextBox.
        static void Main(string[] args)
        {
            Console.WriteLine("ver 0.3.1");
            // Создаём возможно отсутствующие объекты в бд.
            DBmanager.Initialize();
            // Ждём пользователей в бесконечном цикле.
            ServerEngine.Listen();
        }
    }
}
