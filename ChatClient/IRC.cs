using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using static ChatClient.lib.Routines;
using ChatLib;
using static ChatLib.Interactions;

namespace ChatClient
{
    public partial class IRC : Form
    {
        private const string host = "46.173.214.207";
        static NetworkStream stream;
        static TcpClient client;
        public string username;
        codes lastServReqest = codes.REQUESTING_USERNAME;
        public IRC()
        {
            InitializeComponent();
        }

        private void onIrcLoad(object sender, EventArgs e)
        {
            messageBox.AppendText("Ah shit, here we go again.");
            Task task = new Task(Connect);
            task.Start();
        }
        private void SendChatMessage(object sender, KeyPressEventArgs e)
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
                        switch (lastServReqest)
                        {
                            case codes.EXISTING_USERNAME:
                            case codes.REQUESTING_USERNAME:
                                sendToStream(new ChatLib.Message(codes.SENDING_USERNAME, inputField.Text), ref stream);
                                lastServReqest = codes.SENDING_USERNAME;
                                break;
                            default:
                                sendToStream(new ChatLib.Message(codes.SENDING_CHAT_MESSAGE, inputField.Text), ref stream);
                                break;
                        }
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
                    printToMessageBox("Connected to server. Please enter your nickname.", messageBox);
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
            ChatLib.Message message;
            while (true)
            {
                try
                {
                    message = getFromStream(ref stream);
                    printToMessageBox(message.info, messageBox);
                }
                catch (Exception e)
                {
                    printToMessageBox("In GetNewMessages(): " + e.Message, messageBox);
                }
            }

        }
        private void IRC_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendToStream(new ChatLib.Message(codes.SENDING_DISCONNECT_MESSAGE, ""), ref stream);
            Disconnect();
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
