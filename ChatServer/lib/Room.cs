using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ChatLib;
using static ChatLib.Interactions;
namespace ChatServer.lib
{
    class Room
    {
        public string name;
        public List<ClientClass> connectedUsers = new List<ClientClass>();
        protected internal MySqlConnection connection = DBmanager.Connect();
        public Room(string name)
        {
            this.name = name;
        }
        protected internal void AddClient(ClientClass client)
        {
            client.room = this;
            connectedUsers.Add(client);
            Console.WriteLine("Successfully added client " + client.name + " to " + this.name +
                " room. There are " + connectedUsers.Count + " connected users.");
            connectedUsers.ForEach(user => SendToStream(new Message(codes.SENDING_CHAT_INFO,
                list: connectedUsers.Select(u => u.name).ToList(), list2: DBmanager.GetHistory(name, user.connection)), ref user.client));
            Task.Run(() => SendBroadcastMessage(client.name + " joined the room.", connection));
        }
        protected internal void RemoveClient(int id)
        {
            ClientClass client = connectedUsers.FirstOrDefault(i => i.id == id);
            if (client != null)
                connectedUsers.Remove(client);
            Task.Run(() => SendBroadcastMessage(client.name + " left the room.", connection));
            connectedUsers.ForEach(user => SendToStream(new Message(codes.SENDING_CHAT_INFO,
                list: connectedUsers.Select(u => u.name).ToList(), list2: DBmanager.GetHistory(name, user.connection)), ref user.client));
            Console.WriteLine(client.name + " left the room. There are " + connectedUsers.Count + " connected users.");
        }
        public void SendBroadcastMessage(string message, MySqlConnection connection)
        {
            DBmanager.SaveMessage(message, name, connection);
            Console.Write("Broadcasting for: ");
            for (int i = 0; i < connectedUsers.Count; i++)
            {
                Console.Write(connectedUsers[i].name + (i + 1 == connectedUsers.Count ? "" : ", "));
                SendToStream(new Message(codes.SENDING_BROADCAST_MESSAGE, message), ref connectedUsers[i].client);
            }
            Console.WriteLine(".");
        }
    }
}
