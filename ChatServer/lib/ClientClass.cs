using System;
using System.Linq;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using ChatLib;
using static ChatLib.Interactions;

namespace ChatServer.lib
{
    class ClientClass
    {
        protected internal int id { get; private set; }
        protected internal string name { get; private set; }
        protected internal MySqlConnection connection { get; private set; }
        public TcpClient client;
        public Room room;
        public ClientClass(ref MySqlConnection connection, ref TcpClient client, string name, int id)
        {
            this.connection = connection;
            this.client = client;
            this.name = name;
            this.id = id;
        }
        // Обработка получаемых от клиента пакетов.
        public void Process()
        {
            try
            {
                Message message;
                while (true)
                {
                    message = GetFromStream(ref client);
                    switch (message.code)
                    {
                        case codes.REQUESTING_ROOMLIST:
                            SendToStream(new Message(codes.SENDING_ROOMLIST, list: DBmanager.GetRoomList(connection)), ref client);
                            break;
                        case codes.REQUESTING_CHAT_HIST:
                            SendToStream(new Message(codes.SENDING_CHAT_HIST, list: DBmanager.GetHistory(name, connection)), ref client);
                            break;
                        case codes.SENDING_CHAT_MESSAGE:
                            room.SendBroadcastMessage(name + ": " + message.info, connection);
                            Console.WriteLine(name + ": " + message.info);
                            break;
                        case codes.SENDING_SELECTED_ROOM:
                            ServerEngine.ChangeRoom(this, message.info);
                            break;
                        case codes.SENDING_DISCONNECT_MESSAGE:
                            Disconnect();
                            return;
                        default:
                            Console.WriteLine("Wrong message code with package body: " + message.info + ".");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In Process(): " + e.Message);
                Console.WriteLine(name + " left the room.");
                Disconnect();
            }
        }
        // Закрытие объектов, отвечающих за подключение.
        public void Disconnect()
        {
            if (client != null)
                client.Close();
            connection.Close();
            ServerEngine.existingNicknames.Remove(name);
            room.RemoveClient(id);
        }
    }
}
