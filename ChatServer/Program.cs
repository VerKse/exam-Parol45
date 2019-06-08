using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatServer.lib;
using MySql.Data.MySqlClient;

namespace ChatServer
{
    class Program
    {
        static ChatRoom server = new ChatRoom();
        static MySqlConnection mysql_connection;

        static void Main(string[] args)
        {
            try
            {
                DBmanager.Initialize(ref mysql_connection);
                mysql_connection.Close();
                server.Listen();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }
    }
}
