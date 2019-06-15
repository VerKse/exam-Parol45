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
    static class ServerEngine
    {
        static int idForNextUser = 0;
        static TcpListener listener;
        public static List<Room> rooms = new List<Room>();
        public static List<ClientClass> unassignedUsers = new List<ClientClass>();
        public static List<string> existingNicknames = new List<string>();
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
                    Task.Run(() => LogIn(ref client));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In Listen(): " + e.Message);
            }
        }
        private static void LogIn(ref TcpClient client)
        {
            Message message;
            bool served = false;
            MySqlConnection connection = DBmanager.Connect();
            ClientClass clientObj = new ClientClass(ref connection, ref client, null, idForNextUser++);
            unassignedUsers.Add(clientObj);
            try
            {
                while (!served)
                {
                    message = GetFromStream(ref client);
                    switch (message.code)
                    {
                        case codes.REQUESTING_ROOMLIST:
                            SendToStream(new Message(codes.SENDING_ROOMLIST, list: DBmanager.GetRoomList(connection))
                                , ref client);
                            break;
                        case codes.SENDING_USERNAME:
                            if (existingNicknames.FirstOrDefault(n => n == message.info) == null)
                            {
                                clientObj.name = message.info;
                                existingNicknames.Add(clientObj.name);
                                SendToStream(new Message(codes.CONFIRMING_USERNAME, clientObj.name), ref client);
                                Console.WriteLine("User " + clientObj.name + " logged in.");
                            }
                            else
                            {
                                SendToStream(new Message(codes.REQUESTING_USERNAME,
                                    "There is user witn nickname \"" + message.info + "\" already"), ref client);
                            }
                            break;
                        case codes.SENDING_SELECTED_ROOM:
                            unassignedUsers.Remove(clientObj);
                            rooms.FirstOrDefault(r => r.name == message.info).AddClient(clientObj);
                            Task.Run(() => clientObj.Process());
                            served = true;
                            break;
                        case codes.REQUESTING_NEW_ROOM:
                            AddRoom(ref connection, ref client, message.info);
                            break;
                        case codes.SENDING_DISCONNECT_MESSAGE:
                            throw new Exception("Disconnected");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In LogIn(): " + e.Message);
                unassignedUsers.Remove(clientObj);
                if (clientObj.client != null)
                {
                    clientObj.client.GetStream().Close();
                    clientObj.client.Close();
                }
                Console.WriteLine((clientObj.name == null ? "Anon" : clientObj.name) + " disconnected");
                if (clientObj.name != null)
                    existingNicknames.Remove(clientObj.name);
            }
        }
        public static void ChangeRoom(ClientClass client, string newRoom)
        {
            client.room.RemoveClient(client.id);
            rooms.FirstOrDefault(r => r.name == newRoom).AddClient(client);
        }
        public static void AddRoom(ref MySqlConnection connection, ref TcpClient client, string roomName)
        {
            if (rooms.FirstOrDefault(r => 0 == string.Compare(r.name, roomName, true)) == null)
            {
                DBmanager.CreateNewRoom(ref connection, roomName);
                rooms.Add(new Room(roomName));
                List<string> roomList = DBmanager.GetRoomList(connection);
                unassignedUsers.ForEach(user =>
                SendToStream(new Message(codes.SENDING_ROOMLIST, list: roomList), ref user.client));
                rooms.ForEach(room => room.connectedUsers.ForEach(user =>
                SendToStream(new Message(codes.SENDING_ROOMLIST, list: roomList), ref user.client)));
                Console.WriteLine("Room " + roomName + " is created.");
            }
            else
                SendToStream(new Message(codes.EXISTING_ROOM_NAME), ref client);
        }
        public static void RemoveRoom(Room room)
        {
            rooms.Remove(room);
            DBmanager.RemoveRoom(ref room.connection, room.name);
            room.connection.Close();
        }
    }
}
