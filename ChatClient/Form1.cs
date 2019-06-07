using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

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
            messageBox.AppendText("Это гамно хотя бы запустилось");
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
                        byte[] data = Encoding.Unicode.GetBytes(inputField.Text);
                        stream.Write(data, 0, data.Length);
                        inputField.Text = "";
                    }
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    messageBox.AppendText("\n" + ex.Message);
                    messageBox.SelectionStart = messageBox.Text.Length - 1;
                    messageBox.ScrollToCaret();
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
                    messageBox.BeginInvoke(new Action(() => messageBox.AppendText("\nДобро пожаловать в чат, введите свой ник")));
                    messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length - 1));
                    messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
                    Task task = new Task(GetNewMessages);
                    task.Start();
                }
            }
            catch (Exception e)
            {
                messageBox.BeginInvoke(new Action<string>((s) => messageBox.AppendText(s)), "\n" + e.Message);
                messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length - 1));
                messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
            }
        }

        private void GetNewMessages()
        {
            StringBuilder builder = new StringBuilder();
            byte[] data = new byte[64];
            int bytes = 0;
            string message;
            while (true)
            {
                try
                {
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        if (bytes == 0)
                        {
                            Disconnect();
                            return;
                        }
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    builder.Clear();
                    messageBox.BeginInvoke(new Action<string>((s) => messageBox.AppendText(s)), "\n" + message);
                    messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length - 1));
                    messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
                }
                catch (Exception e)
                {
                    messageBox.BeginInvoke(new Action<string>((s) => messageBox.AppendText(s)), "\n" + e.Message);
                    messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length - 1));
                    messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
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
            Environment.Exit(0);
        }
    }
}
