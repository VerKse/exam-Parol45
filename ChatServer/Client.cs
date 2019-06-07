using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace ChatServer
{
    class ClientClass
    {
        protected internal string Id { get; private set; }
        TcpClient Client;
        ServerEngine Server;
        public NetworkStream Stream;
        public ClientClass(TcpClient client, ServerEngine server)
        {
            this.Client = client;
            this.Server = server;
        }
        public void Process()
        {
            try
            {
                Stream = Client.GetStream();
                string message = GetMessage();
                Id = message;
                Server.UpdateAll(message + " вошёл в чат");
                Console.WriteLine(message + " вошёл в чат");

                while (true)
                {
                    message = GetMessage();
                    Server.UpdateAll(Id + ": " + message);
                    Console.WriteLine(Id + ": " + message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Id + " " + e.Message);
                Disconnect();
                Server.RemoveClient(Id);
            }
        }

        private string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder.ToString();
        }

        public void Disconnect()
        {
            if (Stream != null)
                Stream.Close();
            if (Client != null)
                Client.Close();
        }
    }
}
