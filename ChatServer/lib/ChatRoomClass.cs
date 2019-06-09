using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ChatLib;
using static ChatLib.Interactions;

namespace ChatServer.lib
{
    class ChatRoomClass
    {
        int idForNextUser = 0;
        string name;
        List<ClientClass> connectedUsers = new List<ClientClass>();
        TcpListener listener;

        public ChatRoomClass(string name = "")
        {
            this.name = name;
        }
        // Добавление в коллекцию нового клиента и создание выделенного потока (task ~= thread) для него.
        protected internal void AddNewClient(ClientClass client)
        {
            connectedUsers.Add(client);
            Console.WriteLine("Successfully added client to collection. There are " + connectedUsers.Count + " connected users.");
            Task task = new Task(client.Process);
            task.Start();
        }
        // Удаление из коллекции пользователя с заданным id (Да, из-за постоянных переподключений он за границы выйти может).
        protected internal void RemoveClient(int id)
        {
            ClientClass client = connectedUsers.FirstOrDefault(c => c.id== id);
            if (client != null)
            {
                connectedUsers.Remove(client);
                UpdateAll(client.name + " has disconnected.");
                Console.WriteLine(client.name + " has disconnected.");
            }
        }
        // Ожидание новых подключений с любого адреса.
        public void Listen()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 1488);
                listener.Start();
                while (true)
                {
                    Console.WriteLine("Waiting for connections... ");
                    TcpClient client = listener.AcceptTcpClient();
                    Console.WriteLine("Somebody has connected.");
                    AddNewClient(new ClientClass(client, this, idForNextUser++));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("In Listen(): " + e.Message);
            }
        }
        // Широковещательная рассылка сообщения или подкл/откл пользователя.
        public void UpdateAll(string message)
        {
            Console.Write("Broadcasting for: ");
            for (int i = 0; i < connectedUsers.Count; i++)
            {
                Console.Write(connectedUsers[i].name + (i + 1 == connectedUsers.Count ? "." : ", "));
                sendToStream(new Message(codes.SENDING_BROADCAST_MESSAGE, message), ref connectedUsers[i].Stream);
            }
            Console.WriteLine();
        }
    }
}
