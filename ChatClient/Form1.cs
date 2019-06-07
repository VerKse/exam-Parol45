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
            Connect();
        }

        private void SendMessage(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    if (client == null || stream == null)
                        Connect();
                    byte[] data = Encoding.Unicode.GetBytes(inputField.Text);
                    stream.Write(data, 0, data.Length);
                    e.Handled = true;
                    inputField.Text = "";
                }
                catch (Exception ex)
                {
                    messageBox.AppendText(ex.Message + "\n");
                }
            }
        }

        private void Connect()
        {
            try
            {
                client = new TcpClient(host, 1488);
                stream = client.GetStream();
                Task task = new Task(GetNewMessages);
                task.Start();
            }
            catch (Exception e)
            {
                messageBox.AppendText(e.Message + "\n");
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
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    builder.Clear();
                    messageBox.BeginInvoke(new Action<string>((s) => messageBox.AppendText(s)), message + "\n");
                }
                catch (Exception e)
                {
                    messageBox.BeginInvoke(new Action<string>((s) => messageBox.AppendText(s)), e.Message + "\n");
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
