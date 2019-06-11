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
        TcpClient Client;
        ServerEngine Server;
        public NetworkStream Stream;
        public ClientClass(TcpClient client, ServerEngine server, int id)
        {
            Client = client;
            Server = server;
            this.id = id;
        }
        // Обработка получаемых от клиента пакетов.
        public void Process()
        {
            try
            {
                Stream = Client.GetStream();
                Message message;
                while (true)
                {
                    message = getFromStream(ref Stream);
                    switch (message.code)
                    {
                        case codes.SENDING_USERNAME:
                            if (Server.connectedUsers.FirstOrDefault(c => c.name == message.info) == null)
                            {
                                name = message.info;
                                sendToStream(new Message(codes.CONFIRMING_USERNAME, name), ref Stream);
                                Server.UpdateAll(name + " has connected.");
                                Console.WriteLine(name + " has connected.");
                            }
                            else
                            {
                                sendToStream(new Message(codes.REQUESTING_USERNAME, 
                                    "There is user witn nickname \"" + message.info + "\" in this room already"), ref Stream);
                                Console.WriteLine("There is user witn nickname " + name + " already");
                            }
                            break;
                        case codes.REQUESTING_ROOMLIST:
                            sendToStream(new Message(codes.SENDING_ROOMLIST, list: DBmanager.GetRoomList()), ref Stream);
                            break;
                        case codes.SENDING_CHAT_MESSAGE:
                            Server.UpdateAll(name + ": " + message.info);
                            Console.WriteLine(name + ": " + message.info);
                            break;
                        case codes.SENDING_DISCONNECT_MESSAGE:
                            Disconnect();
                            Server.RemoveClient(id);
                            return;
                        default:
                            Console.WriteLine("Wrong message code. With package body: " + message.info + ".");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In Process(): " + e.Message);
                Disconnect();
                Server.RemoveClient(id);
            }
        }
        // Закрытие объектов, отвечающих за подключение.
        public void Disconnect()
        {
            if (Stream != null)
                Stream.Close();
            if (Client != null)
                Client.Close();
        }
    }
}
