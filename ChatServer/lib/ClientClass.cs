using System;
using System.Linq;
using System.Net.Sockets;
using ChatLib;
using static ChatLib.Interactions;

namespace ChatServer.lib
{
    class ClientClass
    {
        protected internal int id { get; private set; }
        protected internal string name { get; private set; }
        TcpClient client;
        public NetworkStream stream;
        public Room room;
        public ClientClass(ref TcpClient client, string name, int id)
        {
            this.client = client;
            this.name = name;
            this.id = id;
            stream = client.GetStream();
        }
        // Обработка получаемых от клиента пакетов.
        public void Process()
        {
            try
            {
                Message message;
                while (true)
                {
                    message = GetFromStream(ref stream);
                    switch (message.code)
                    {
                        case codes.REQUESTING_ROOMLIST:
                            SendToStream(new Message(codes.SENDING_ROOMLIST, list: DBmanager.GetRoomList()), ref stream);
                            break;
                        case codes.SENDING_CHAT_MESSAGE:
                            room.SendBroadcastMessage(name + ": " + message.info);
                            Console.WriteLine(name + ": " + message.info);
                            break;
                        case codes.SENDING_SELECTED_ROOM:
                            ServerEngine.ChangeRoom(this, message.info);
                            break;
                        case codes.SENDING_DISCONNECT_MESSAGE:
                            Disconnect();
                            ServerEngine.existingNicknames.Remove(name);
                            room.RemoveClient(id);
                            return;
                        default:
                            Console.WriteLine("Wrong message code with package body: " + message.info + ".");
                            break;
                        // TODO: добавить обработку остальных кодов.
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In Process(): " + e.Message);
                Disconnect();
                room.RemoveClient(id);
            }
        }
        // Закрытие объектов, отвечающих за подключение.
        public void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
