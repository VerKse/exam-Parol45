using System;
using System.Collections.Generic;
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
        static TcpClient client;
        static NetworkStream stream;
        int selectedRoom;
        bool loggedIn = false;
        public IRC()
        {
            InitializeComponent();
        }

        private void onIrcLoad(object sender, EventArgs e)
        {
            messageBox.AppendText("Ah shit, here we go again.");
            Task.Run(() => Connect());
        }
        private void Connect()
        {
            try
            {
                client = new TcpClient(host, 1488);
                stream = client.GetStream();
                PrintToMessageBox("Connected to server. Please enter your nickname.", messageBox);
                SendToStream(new ChatLib.Message(codes.REQUESTING_ROOMLIST), ref client);
                chatroomList.BeginInvoke(new Action(() => selectedRoom = chatroomList.SelectedIndex));
                Task.Run(() => GetNewMessages());
            }
            catch (Exception e)
            {
                PrintToMessageBox("In Connect(): " + e.Message, messageBox);
            }
        }
        private void SendChatMessage(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && inputField.Text.Trim(' ').Length > 0)
            {
                try
                {
                    if (client == null || stream == null || !client.Connected || !stream.CanWrite)
                        Task.Run(() => Connect());
                    else
                    {
                        if (!loggedIn) {
                            if (inputField.Text.Trim(' ').Length < 51)
                            {
                                SendToStream(new ChatLib.Message(codes.SENDING_USERNAME, inputField.Text.Trim(' ')), ref client);
                                loggedIn = true;
                            }
                            else
                                PrintToMessageBox("Nickname must be less than 51 characters.", messageBox);
                        }
                        else {
                            if (inputField.Text.Trim(' ').Length < 948)
                                SendToStream(new ChatLib.Message(codes.SENDING_CHAT_MESSAGE, inputField.Text.Trim(' ')), ref client);
                            else
                                PrintToMessageBox("Your message is way too long.", messageBox);
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
        private void GetNewMessages()
        {
            ChatLib.Message message;
            try
            {
                while (true)
                {
                    message = GetFromStream(ref client);
                    switch (message.code)
                    {
                        case codes.SENDING_ROOMLIST:
                            SetListboxItems(chatroomList, message.list);
                            break;
                        case codes.CONFIRMING_USERNAME:
                            BeginInvoke(new Action(() => Text = message.info + " - " + Text));
                            chatroomList.BeginInvoke(new Action(() => chatroomList.Enabled = true));
                            PrintToMessageBox("You logged in as " + message.info, messageBox);
                            break;
                        case codes.REQUESTING_USERNAME:
                            PrintToMessageBox(message.info, messageBox);
                            loggedIn = false;
                            break;
                        case codes.SENDING_USERLIST:
                            SetListboxItems(onlineUsersList, message.list);
                            break;
                        case codes.SENDING_CHAT_HIST:
                            messageBox.BeginInvoke(new Action(() => messageBox.Text = "Welcome."));
                            message.list.ForEach(mess => PrintToMessageBox(mess, messageBox));
                            break;
                        case codes.SENDING_BROADCAST_MESSAGE:
                            PrintToMessageBox(message.info, messageBox);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                PrintToMessageBox("In GetNewMessages(): " + e.Message, messageBox);
                PrintToMessageBox("Disconnected from server", messageBox);
                Disconnect();
            }
        }
        private void SetListboxItems(ListBox subj, List<string> list)
        {
            IAsyncResult resultUserlist = subj.BeginInvoke(new Action(() => subj.Items.Clear()));
            onlineUsersList.EndInvoke(resultUserlist);
            for (int i = 0; i < list.Count; i++)
            {
                resultUserlist = subj.BeginInvoke(new Action(() => subj.Items.Add(list[i])));
                subj.EndInvoke(resultUserlist);
            }
        }
        private void ChooseRoom(object sender, EventArgs e)
        {
            if (chatroomList.SelectedIndex != selectedRoom)
            {
                SendToStream(new ChatLib.Message(codes.SENDING_SELECTED_ROOM, chatroomList.SelectedItem.ToString()), ref client);
                selectedRoom = chatroomList.SelectedIndex;
            }
        }
        private void IRC_FormClosing(object sender, FormClosingEventArgs e)
        {
            SendToStream(new ChatLib.Message(codes.SENDING_DISCONNECT_MESSAGE), ref client);
            Disconnect();
        }
        static void Disconnect()
        {
            try
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
            catch { }
        }
    }
}
