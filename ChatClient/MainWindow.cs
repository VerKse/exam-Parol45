using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ChatClient.lib;
using static ChatClient.lib.Routines;

namespace ChatClient
{
    public partial class MainWindow : Form
    {
        private const string host = "46.173.214.207";
        static NetworkStream stream;
        static TcpClient client;

        public MainWindow()
        {
            InitializeComponent();
            messageBox.AppendText("Это гамно хотя бы запустилось.");
            Task task = new Task(Connect);
            task.Start();
        }

        private void SendMessage(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (client == null || stream == null)
                    {
                        Task task = new Task(Connect);
                        task.Start();
                    }
                    else
                    {
                        sendToStream(inputField.Text, ref stream);
                        inputField.Text = "";
                    }
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    printToMessageBox("In SendMessage(): " + ex.Message, messageBox);
                }
            }
        }

        private void Connect()
        {
            try
            {
                client = new TcpClient(host, 1488);
                stream = client.GetStream();
                if (client != null && stream != null)
                {
                    printToMessageBox("Добро пожаловать в чат, введите свой ник.", messageBox);
                    Task task = new Task(GetNewMessages);
                    task.Start();
                }
            }
            catch (Exception e)
            {
                printToMessageBox("In Connect(): " + e.Message, messageBox);
            }
        }

        private void GetNewMessages()
        {
            string message;
            while (true)
            {
                try
                {
                    message = getFromStream(ref stream);
                    if (message == null)
                    {
                        Disconnect();
                        return;
                    }
                    printToMessageBox(message, messageBox);
                }
                catch (Exception e)
                {
                    printToMessageBox("In GetNewMessages(): " + e.Message, messageBox);
                    Disconnect();
                }
            }

        }

        static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
