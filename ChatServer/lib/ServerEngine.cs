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
            string name = null;
            bool served = false;
            try
            {
                MySqlConnection connection = DBmanager.Connect();
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
                                name = message.info;
                                existingNicknames.Add(name);
                                SendToStream(new Message(codes.CONFIRMING_USERNAME, name), ref client);
                                Console.WriteLine("User " + name + " logged in.");
                            }
                            else
                            {
                                SendToStream(new Message(codes.REQUESTING_USERNAME,
                                    "There is user witn nickname \"" + message.info + "\" already"), ref client);
                            }
                            break;
                        case codes.SENDING_SELECTED_ROOM:
                            if (name != null)
                            {
                                ClientClass clientObj = new ClientClass(ref connection, ref client, name, idForNextUser++);
                                rooms.FirstOrDefault(r => r.name == message.info).AddClient(clientObj);
                                Task.Run(() => clientObj.Process());
                                served = true;
                            }
                            else
                            {
                                Console.WriteLine("Bad room name.");
                            }
                            break;
                        case codes.SENDING_DISCONNECT_MESSAGE:
                            throw new Exception("Disconnected");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In LogIn(): " + e.Message);
                if (client != null)
                {
                    client.GetStream().Close();
                    client.Close();
                }
                Console.WriteLine((name == null ? "Anon" : name) + " disconnected");
                if (name != null)
                    existingNicknames.Remove(name);
            }
        }
        public static void ChangeRoom(ClientClass client, string newRoom)
        {
            client.room.RemoveClient(client.id);
            rooms.FirstOrDefault(r => r.name == newRoom).AddClient(client);
        }
        // TODO: добавление/удаление комнат.
    }
}
