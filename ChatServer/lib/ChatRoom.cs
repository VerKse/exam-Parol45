using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ChatServer.lib
{
    class ChatRoom
    {
        List<ClientClass> clients = new List<ClientClass>();
        TcpListener listener;

        protected internal void AddNewClient(ClientClass client)
        {
            clients.Add(client);
            Task task = new Task(client.Process);
            task.Start();
        }

        protected internal void RemoveClient(string id)
        {
            ClientClass client = clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                clients.Remove(client);
        }
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
                    Console.WriteLine("Somebody connected");
                    AddNewClient(new ClientClass(client, this));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void UpdateAll(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                Console.Write(clients[i].Id + ", ");
                clients[i].Stream.Write(data, 0, data.Length);
            }
            Console.WriteLine();
        }
    }
}
