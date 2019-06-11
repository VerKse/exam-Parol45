using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using static ChatClient.lib.Routines;
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
            Task.Run(() => Connect());
        }
        private void SendChatMessage(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && inputField.Text.Trim(' ').Length > 0)
            {
                try
                {
                    if (client == null || stream == null || !client.Connected || !stream.CanWrite)
                    {
                        Task task = new Task(Connect);
                        task.Start();
                    }
                    else
                    {
                        switch (lastServReqest)
                        {
                            case codes.REQUESTING_USERNAME:
                                sendToStream(new ChatLib.Message(codes.SENDING_USERNAME, inputField.Text.Trim(' ')), ref stream);
                                lastServReqest = codes.SENDING_USERNAME;
                                break;
                            default:
                                sendToStream(new ChatLib.Message(codes.SENDING_CHAT_MESSAGE, inputField.Text.Trim(' ')), ref stream);
                                break;
                        }
                        inputField.Text = "";
                    }
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    PrintToMessageBox("In SendMessage(): " + ex.Message, messageBox);
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
                    PrintToMessageBox("Connected to server. Please enter your nickname.", messageBox);
                    sendToStream(new ChatLib.Message(codes.REQUESTING_ROOMLIST), ref stream);
                    Task task = new Task(GetNewMessages);
                    task.Start();
                }
            }
            catch (Exception e)
            {
                PrintToMessageBox("In Connect(): " + e.Message, messageBox);
            }
        }

        private void GetNewMessages()
        {
            ChatLib.Message message;
            try
            {
                while (true)
                {
                    if (stream == null || !stream.CanRead)
                        break;
                    message = getFromStream(ref stream);
                    switch (message.code)
                    {
                        case codes.SENDING_BROADCAST_MESSAGE:
                            PrintToMessageBox(message.info, messageBox);
                            break;
                        case codes.CONFIRMING_USERNAME:
                            BeginInvoke(new Action(() => Text = message.info + " - " + Text));
                            chatroomList.BeginInvoke(new Action(() => chatroomList.Enabled = true));
                            onlineUsersList.BeginInvoke(new Action(() => onlineUsersList.Items.Add("Choose room.")));
                            break;
                        case codes.REQUESTING_USERNAME:
                            PrintToMessageBox(message.info, messageBox);
                            lastServReqest = codes.REQUESTING_USERNAME;
                            break;
                        case codes.SENDING_ROOMLIST:
                            IAsyncResult resultObj = chatroomList.BeginInvoke(new Action(() => chatroomList.Items.Clear()));
                            chatroomList.EndInvoke(resultObj);
                            for (int i = 0; i < message.list.Count; i++)
                            {
                                resultObj = chatroomList.BeginInvoke(new Action(() => chatroomList.Items.Add(message.list[i])));
                                chatroomList.EndInvoke(resultObj);
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                // PrintToMessageBox("In GetNewMessages(): " + e.Message, messageBox);
                PrintToMessageBox("Disconnected from server", messageBox);
                Disconnect();
            }
        }
        static void Disconnect()
        {
            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();
        }

        private void chatroomList_Click(object sender, EventArgs e)
        {
            if (chatroomList.SelectedItem != null)
            {
                messageBox.AppendText("\n" + chatroomList.SelectedItem.ToString());
            }
        }

        private void chatroomList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                chatroomList_Click(sender, e);
        }
        private void IRC_FormClosing(object sender, FormClosingEventArgs e)
        {
            sendToStream(new ChatLib.Message(codes.SENDING_DISCONNECT_MESSAGE, ""), ref stream);
            Disconnect();
        }
    }
}
