using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatLib;
using static ChatLib.Interactions;
namespace ChatServer.lib
{
    class Room
    {
        public string name;
        public List<ClientClass> connectedUsers = new List<ClientClass>();
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
            SendToStream(new Message(codes.SENDING_USERLIST, list: connectedUsers.Select(u => u.name).Distinct().ToList()),
                ref client.stream);
            Task.Run(() => SendBroadcastMessage(client.name + " joined the room."));
        }
        protected internal void RemoveClient(int id)
        {
            ClientClass client = connectedUsers.FirstOrDefault(i => i.id == id);
            if (client != null)
                connectedUsers.Remove(client);
            Task.Run(() => SendBroadcastMessage(client.name + " left the room."));
            Console.WriteLine(client.name + " left the room. There are " + connectedUsers.Count + " connected users.");
        }
        public void SendBroadcastMessage(string message)
        {
            Console.Write("Broadcasting for: ");
            for (int i = 0; i < connectedUsers.Count; i++)
            {
                Console.Write(connectedUsers[i].name + (i + 1 == connectedUsers.Count ? "." : ", "));
                SendToStream(new Message(codes.SENDING_BROADCAST_MESSAGE, message), ref connectedUsers[i].stream);
            }
            Console.WriteLine();
            // TODO: сохранение в историю сообщений.
        }
    }
}
