using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using ChatClient.lib;
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
            menuStrip.BackColor = SystemColors.Control;
            Task.Run(() => Connect());
        }
        public void PrintToMessageBox(string message)
        {
            messageBox.BeginInvoke(new Action(() => messageBox.AppendText("\n" + message)));
            messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length - 1));
            messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
        }
        private void Connect()
        {
            try
            {
                client = new TcpClient(host, 1488);
                stream = client.GetStream();
                inputTextBox.BeginInvoke(new Action(() => { inputTextBox.Enabled = true; inputTextBox.Select(); }));
                PrintToMessageBox("Connected to server. Please enter your nickname.");
                SendToStream(new ChatLib.Message(codes.REQUESTING_ROOMLIST), ref client);
                chatroomList.BeginInvoke(new Action(() => selectedRoom = -1));
                Task.Run(() => GetNewMessages());
            }
            catch (Exception e)
            {
                PrintToMessageBox("In Connect(): " + e.Message);
            }
        }
        private void SendChatMessage(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && inputTextBox.Text.Trim(' ').Length > 0)
            {
                try
                {
                    if (client == null || stream == null || !client.Connected || !stream.CanWrite)
                        Task.Run(() => Connect());
                    else
                    {
                        string toSend = inputTextBox.Text.Trim(' ', '\r', '\n').Replace("\n", "").Replace("\r", "");
                        if (!loggedIn) {
                            if (toSend.Length < 51)
                            {
                                if (toSend.Length > 0)
                                {
                                    SendToStream(new ChatLib.Message(codes.SENDING_USERNAME, toSend), ref client);
                                    loggedIn = true;
                                }
                                else
                                    PrintToMessageBox("Nickname must be at least 1 character");
                            }
                            else
                                PrintToMessageBox("Nickname must be less than 51 characters.");
                        }
                        else {
                            if (toSend.Length < 948)
                                if (toSend.Length > 0)
                                    SendToStream(new ChatLib.Message(codes.SENDING_CHAT_MESSAGE, toSend), ref client);
                                else
                                    PrintToMessageBox("Your message is empty.");
                            else
                                PrintToMessageBox("Your message is way too long.");
                        }
                        inputTextBox.Text = "";
                    }
                    e.Handled = true;
                }
                catch (Exception ex)
                {
                    PrintToMessageBox("In SendMessage(): " + ex.Message);
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
                            chatroomList.BeginInvoke(new Action(() => chatroomList.SelectedIndex = selectedRoom));
                            break;
                        case codes.CONFIRMING_USERNAME:
                            BeginInvoke(new Action(() => Text = message.info + " - IRC"));
                            chatroomList.BeginInvoke(new Action(() => chatroomList.Enabled = true));
                            PrintToMessageBox("You logged in as " + message.info);
                            break;
                        case codes.REQUESTING_USERNAME:
                            PrintToMessageBox(message.info);
                            loggedIn = false;
                            break;
                        case codes.SENDING_USERLIST:
                            SetListboxItems(onlineUsersList, message.list);
                            break;
                        case codes.SENDING_CHAT_HIST:
                            messageBox.BeginInvoke(new Action(() => messageBox.Text = "Welcome."));
                            message.list.ForEach(mess => PrintToMessageBox(mess));
                            break;
                        case codes.SENDING_BROADCAST_MESSAGE:
                            PrintToMessageBox(message.info);
                            break;
                        case codes.EXISTING_ROOM_NAME:
                            AddRoom();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                PrintToMessageBox("In GetNewMessages(): " + e.Message);
                Disconnect();
                PrintToMessageBox("Disconnected from server");
                inputTextBox.Enabled = false;
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
                inputTextBox.Enabled = true;
                selectedRoom = chatroomList.SelectedIndex;
            }
        }
        private void ToolStripDisconnectClick(object sender, EventArgs e)
        {
            SendToStream(new ChatLib.Message(codes.SENDING_DISCONNECT_MESSAGE), ref client);
            Disconnect();
            loggedIn = false;
            chatroomList.Enabled = false;
            inputTextBox.Enabled = false;
            Text = "IRC";
        }
        private void ToolStripReconnectClick(object sender, EventArgs e)
        {
            ToolStripDisconnectClick(sender, e);
            messageBox.Clear();
            messageBox.AppendText("Ah shit, here we go again.");
            Connect();
        }
        private void ToolStripAddRoomClick(object sender, EventArgs e)
        {
            AddRoom();
        }
        private void AddRoom()
        {
            BeginInvoke(new Action(() => {
                NewRoomDialog meh = new NewRoomDialog(ref client);
                meh.Show();
            }));
        }
        private void ToolStripDeleteClick(object sender, EventArgs e)
        {
            if (selectedRoom != -1)
            {
                SendToStream(new ChatLib.Message(codes.REQUESTING_ROOM_DELETING), ref client);
                GoToOpenSpace();
            }
        }
        private void ToolStripLeaveClick(object sender, EventArgs e)
        {
            if (selectedRoom != -1)
            {
                SendToStream(new ChatLib.Message(codes.LEAVING_ROOM), ref client);
                GoToOpenSpace();
            }
        }
        private void GoToOpenSpace()
        {
            selectedRoom = -1;
            chatroomList.SelectedIndex = -1;
            onlineUsersList.Items.Clear();
            messageBox.Clear();
            messageBox.AppendText("Please select room.");
            inputTextBox.Enabled = false;
        }
        private void setLightTheme(object sender, EventArgs e)
        {
            menuStrip.BackColor = SystemColors.Control;
            menuStrip.ForeColor = SystemColors.WindowText;
            BackColor = SystemColors.Control;
            messageBox.BackColor = SystemColors.Window;
            messageBox.ForeColor = SystemColors.WindowText;
            onlineUsersList.BackColor = SystemColors.Window;
            onlineUsersList.ForeColor = SystemColors.WindowText;
            onlineUsersLabel.BackColor = SystemColors.Control;
            onlineUsersLabel.ForeColor = SystemColors.WindowText;
            chatroomList.BackColor = SystemColors.Window;
            chatroomList.ForeColor = SystemColors.WindowText;
            chatroomsLabel.BackColor = SystemColors.Control;
            chatroomsLabel.ForeColor = SystemColors.WindowText;
            inputTextBox.BackColor = SystemColors.Window;
            inputTextBox.ForeColor = SystemColors.WindowText;
        }
        private void setDarkTheme(object sender, EventArgs e)
        {
            Color controlBackground = Color.FromArgb(40, 40, 40);
            menuStrip.BackColor = SystemColors.ControlDarkDark;
            menuStrip.ForeColor = Color.White;
            BackColor = SystemColors.ControlDarkDark;
            messageBox.BackColor = controlBackground;
            messageBox.ForeColor = Color.White;
            onlineUsersList.BackColor = controlBackground;
            onlineUsersList.ForeColor = Color.White;
            onlineUsersLabel.BackColor = Color.DarkGray;
            onlineUsersLabel.ForeColor = Color.White;
            chatroomList.BackColor = controlBackground;
            chatroomList.ForeColor = Color.White;
            chatroomsLabel.BackColor = Color.DarkGray;
            chatroomsLabel.ForeColor = Color.White;
            inputTextBox.BackColor = controlBackground;
            inputTextBox.ForeColor = Color.White;
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
        private void inputTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                e.IsInputKey = true;
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ActiveControl = null;
        }
        private void OnRecize(object sender, EventArgs e)
        {
            messageBox.SelectionStart = messageBox.Text.Length - 1;
            messageBox.ScrollToCaret();
        }
        private void messageBox_MouseHover(object sender, EventArgs e)
        {
            messageBox.Select();
        }
        private void inputTextBox_MouseHover(object sender, EventArgs e)
        {
            inputTextBox.Select();
        }
        private void onlineUsersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            onlineUsersList.SelectedIndex = -1;
        }
    }
}
