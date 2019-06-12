using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ChatLib;
using static ChatLib.Interactions;

namespace ChatServer.lib
{
    static class ServerEngine
    {
        static int idForNextUser = 0;
        static TcpListener listener;
        public static List<Room> rooms = new List<Room>();
        public static List<string> existingNicknames = new List<string>();
        private static void LogIn(TcpClient client)
        {
            Message message;
            string name = null;
            bool served = false;
            NetworkStream stream = client.GetStream();
            while (!served)
                try
                {
                    if (stream != null && stream.CanRead)
                    {
                        message = GetFromStream(ref stream);
                        switch (message.code)
                        {
                            case codes.REQUESTING_ROOMLIST:
                                SendToStream(new Message(codes.SENDING_ROOMLIST, list: DBmanager.GetRoomList()), ref stream);
                                break;
                            case codes.SENDING_USERNAME:
                                if (existingNicknames.FirstOrDefault(n => n == message.info) == null)
                                {
                                    name = message.info;
                                    existingNicknames.Add(name);
                                    SendToStream(new Message(codes.CONFIRMING_USERNAME, name), ref stream);
                                    Console.WriteLine("User " + name + " logged in.");
                                }
                                else
                                {
                                    SendToStream(new Message(codes.REQUESTING_USERNAME,
                                        "There is user witn nickname \"" + message.info + "\" already"), ref stream);
                                    Console.WriteLine("There is user witn nickname " + name + " already");
                                }
                                break;
                            case codes.SENDING_SELECTED_ROOM:
                                if (name != null && rooms.FirstOrDefault(r => r.name == message.info) != null)
                                {
                                    ClientClass clientObj = new ClientClass(ref client, name, idForNextUser++);
                                    rooms.FirstOrDefault(r => r.name == message.info).AddClient(clientObj);
                                    Task.Run(() => clientObj.Process());
                                    served = true;
                                }
                                else
                                {
                                    Console.WriteLine("Bad room name.");
                                }
                                break;
                                // TODO: обработка запроса онлайн-листа и остальных кодов.
                        }
                    }
                    else
                    {
                        served = true;
                        if (name != null)
                            existingNicknames.Remove(name);
                    }
                }
                catch (Exception e)
                {
                    // Console.WriteLine("In LogIn(): " + e.Message);
                    if (stream != null)
                        stream.Close();
                    if (client != null)
                        client.Close();
                    Console.WriteLine("Anon disconnected");
                }
        }
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
                    Task.Run(() => LogIn(client));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In Listen(): " + e.Message);
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
