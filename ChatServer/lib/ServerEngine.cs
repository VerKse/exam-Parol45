using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ChatLib;
using MySql.Data.MySqlClient;
using static ChatLib.Interactions;

namespace ChatServer.lib
{
    /// <summary>
    /// Класс, описывающий поведение сервера
    /// </summary>
    static class ServerEngine
    {
        static int idForNextUser = 0;
        static TcpListener listener;
        public static List<RoomClass> rooms = new List<RoomClass>();
        public static List<string> existingNicknames = new List<string>();
        public static List<ClientClass> unassignedUsers = new List<ClientClass>();
        /// <summary>
        /// Прослушивание порта на наличие новых подключений в бесконечном цикле
        /// </summary>
        public static void Listen()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 1488);
                listener.Start();
                while (true)
                {
                    Console.WriteLine("Waiting for connections...");
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Somebody connected.");
                    Task.Run(() => CreateUser(ref client));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In Listen(): " + e.Message);
            }
        }
        /// <summary>
        /// Создание задачи и объекта для пользователя после его подключения
        /// </summary>
        /// <param name="client"></param>
        private static void CreateUser(ref TcpClient client)
        {
            MySqlConnection connection = DBmanager.Connect();
            ClientClass clientObj = new ClientClass(ref connection, ref client, null, idForNextUser++);
            unassignedUsers.Add(clientObj);
            Task.Run(() => clientObj.Process());
        }
        /// <summary>
        /// Смена комнаты пользователя
        /// </summary>
        /// <param name="client"></param>
        /// <param name="newRoom"></param>
        public static void ChangeRoom(ClientClass client, string newRoom)
        {
            client.room.RemoveClient(client.id);
            rooms.FirstOrDefault(r => r.name == newRoom).AddClient(client);
        }
        /// <summary>
        /// Добавление пользователем комнаты
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="client"></param>
        /// <param name="roomName"></param>
        public static void AddRoom(ref MySqlConnection connection, ref TcpClient client, string roomName)
        {
            if (rooms.FirstOrDefault(r => 0 == string.Compare(r.name, roomName, true)) == null)
            {
                DBmanager.CreateNewRoom(ref connection, roomName);
                rooms.Add(new RoomClass(roomName));
                List<string> roomList = DBmanager.GetRoomList(connection);
                unassignedUsers.ForEach(user =>
                SendToStream(new MessageClass(codes.SENDING_ROOMLIST, list: roomList), ref user.client));
                rooms.ForEach(room => room.connectedUsers.ForEach(user =>
                SendToStream(new MessageClass(codes.SENDING_ROOMLIST, list: roomList), ref user.client)));
                Console.WriteLine("Room " + roomName + " is created.");
            }
            else
                SendToStream(new MessageClass(codes.EXISTING_ROOM_NAME), ref client);
        }
        /// <summary>
        /// Удаление комнаты из коллекции и закрытие подключения к БД
        /// </summary>
        /// <param name="room"></param>
        public static void RemoveRoom(RoomClass room)
        {
            rooms.Remove(room);
            DBmanager.RemoveRoom(ref room.connection, room.name);
            room.connection.Close();
        }
    }
}
