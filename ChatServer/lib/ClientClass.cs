using System;
using System.Linq;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using ChatLib;
using static ChatLib.Interactions;

namespace ChatServer.lib
{
    /// <summary>
    /// Класс, описывающий взаимодействия пользователя с сервером
    /// </summary>
    class ClientClass
    {
        protected internal int id { get; private set; }
        protected internal string name { get; set; }
        protected internal MySqlConnection connection;
        public TcpClient client;
        public RoomClass room;
        public ClientClass(ref MySqlConnection connection, ref TcpClient client, string name, int id)
        {
            this.connection = connection;
            this.client = client;
            this.name = name;
            this.id = id;
        }
        /// <summary>
        /// Обработка получаемых от клиента пакетов
        /// </summary>
        public void Process()
        {
            try
            {
                MessageClass message;
                while (true)
                {
                    message = GetFromStream(ref client);
                    switch (message.code)
                    {
                        case codes.REQUESTING_ROOMLIST:
                            SendToStream(new MessageClass(codes.SENDING_ROOMLIST, list: DBmanager.GetRoomList(connection)), ref client);
                            break;
                        case codes.SENDING_USERNAME:
                            if (ServerEngine.existingNicknames.FirstOrDefault(n => n == message.info) == null)
                            {
                                name = message.info;
                                ServerEngine.existingNicknames.Add(name);
                                SendToStream(new MessageClass(codes.CONFIRMING_USERNAME, name), ref client);
                                Console.WriteLine("User " + name + " logged in.");
                            }
                            else
                            {
                                SendToStream(new MessageClass(codes.REQUESTING_USERNAME,
                                    "There is user witn nickname \"" + message.info + "\" already"), ref client);
                            }
                            break;
                        case codes.REQUESTING_CHAT_HIST:
                            SendToStream(new MessageClass(codes.SENDING_CHAT_HIST, list: DBmanager.GetHistory(name, connection)), ref client);
                            break;
                        case codes.SENDING_CHAT_MESSAGE:
                            if (room != null)
                            {
                                room.SendBroadcastMessage(name + ": " + message.info, connection);
                                Console.WriteLine(name + ": " + message.info);
                            }
                            break;
                        case codes.SENDING_SELECTED_ROOM:
                            if (room != null)
                                ServerEngine.ChangeRoom(this, message.info);
                            else
                            {
                                ServerEngine.unassignedUsers.Remove(this);
                                ServerEngine.rooms.FirstOrDefault(r => r.name == message.info).AddClient(this);
                            }
                            break;
                        case codes.REQUESTING_NEW_ROOM:
                            ServerEngine.AddRoom(ref connection, ref client, message.info);
                            Console.WriteLine("Room " + message.info + " was added.");
                            break;
                        case codes.REQUESTING_ROOM_DELETING:
                            if (room.connectedUsers.Count == 1 && room != null)
                            {
                                room.RemoveClient(id);
                                ServerEngine.unassignedUsers.Add(this);
                                ServerEngine.RemoveRoom(room);
                                Console.WriteLine("Room " + room.name + " was deleted.");
                                SendToStream(new MessageClass(codes.SENDING_ROOMLIST, list: DBmanager.GetRoomList(connection)), ref client);
                                room = null;
                            }
                            break;
                        case codes.LEAVING_ROOM:
                            room.RemoveClient(id);
                            room = null;
                            ServerEngine.unassignedUsers.Add(this);
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
                Disconnect();
            }
        }
        /// <summary>
        /// Закрытие объектов, отвечающих за подключение
        /// </summary>
        public void Disconnect()
        {
            if (client != null)
                client.Close();
            connection.Close();
            if (name != null)
                ServerEngine.existingNicknames.Remove(name);
            if (room != null)
                room.RemoveClient(id);
            else
                Console.WriteLine(name + " disconnected.");
        }
    }
}
