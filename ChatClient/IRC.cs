using System;
using System.Media;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using ChatLib;
using static ChatLib.Interactions;

namespace ChatClient
{
    public partial class IRC : Form
    {
        private const string host = "46.173.214.207";
        static TcpClient client;
        static NetworkStream stream;
        string room = null;
        bool loggedIn = false;
        bool inactive;
        public IRC()
        {
            InitializeComponent();
        }

        private void onIrcLoad(object sender, EventArgs e)
        {
            messageBox.AppendText("Ah shit, here we go again.");
            menuStrip.BackColor = SystemColors.Control;
            Task.Run(() => Connect());
            Activated += delegate(object se, EventArgs ev) { inactive = false; };
            Deactivate += delegate (object se, EventArgs ev) { inactive = true; };
        }
        /// <summary>
        /// Запись сообщения в messageBox из другого потока
        /// </summary>
        /// <param name="message"></param>
        public void PrintToMessageBox(string message)
        {
            messageBox.BeginInvoke(new Action(() => messageBox.AppendText("\n" + message)));
            messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length));
            messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
        }
        /// <summary>
        /// Подключение к серверу и дальнейший запуск ожидания входящих пакетов
        /// </summary>
        private void Connect()
        {
            try
            {
                client = new TcpClient(host, 1488);
                stream = client.GetStream();
                inputTextBox.BeginInvoke(new Action(() => { inputTextBox.Enabled = true; inputTextBox.Select(); }));
                PrintToMessageBox("Connected to server. Please enter your nickname.");
                SendToStream(new MessageClass(codes.REQUESTING_ROOMLIST), ref client);
                chatroomList.BeginInvoke(new Action(() => { chatroomList.SelectedIndex = -1; room = null; }));
                Task.Run(() => GetNewMessages());
            }
            catch (Exception e)
            {
                //PrintToMessageBox("In Connect(): " + e.Message);
                PrintToMessageBox("Unable to connect to the server;");
            }
        }
        /// <summary>
        /// Отправка на сервер содержимого inputBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                                    SendToStream(new MessageClass(codes.SENDING_USERNAME, toSend), ref client);
                                    loggedIn = true;
                                }
                                else
                                    PrintToMessageBox("Nickname must be at least 1 character long.");
                            }
                            else
                                PrintToMessageBox("Nickname must be less than 51 characters long.");
                        }
                        else {
                            if (toSend.Length < 948)
                                if (toSend.Length > 0)
                                    SendToStream(new MessageClass(codes.SENDING_CHAT_MESSAGE, toSend), ref client);
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
                    //PrintToMessageBox("In SendMessage(): " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Бесконечный цикл с получением пакета с сервера
        /// </summary>
        private void GetNewMessages()
        {
            MessageClass message;
            try
            {
                while (true)
                {
                    message = GetFromStream(ref client);
                    switch (message.code)
                    {
                        case codes.SENDING_ROOMLIST:
                            SetListboxItems(chatroomList, message.list);
                            UpdateListboxSelection();
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
                            BeginInvoke(new Action(() => { if (inactive) PlaySound(); }));
                            break;
                        case codes.EXISTING_ROOM_NAME:
                            AddRoom("Room already exists");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                //PrintToMessageBox("In GetNewMessages(): " + e.Message);
                Disconnect();
                PrintToMessageBox("Disconnected from server");
                inputTextBox.Enabled = false;
            }
        }
        /// <summary>
        /// Обновление всего содержимого в ListBox
        /// </summary>
        /// <param name="subj"></param>
        /// <param name="list"></param>
        private void SetListboxItems(ListBox subj, List<string> list)
        {
            subj.Invoke(new Action(() => subj.Items.Clear()));
            for (int i = 0; i < list.Count; i++)
                subj.Invoke(new Action(() => subj.Items.Add(list[i])));
        }
        /// <summary>
        /// Сохранение текущего выделения в chatroomList по названию комнаты
        /// </summary>
        private void UpdateListboxSelection()
        {
            for (int i = 0; i < chatroomList.Items.Count; i++)
                if (string.Compare(chatroomList.Items[i].ToString(), room, true) == 0)
                {
                    chatroomList.BeginInvoke(new Action(() => chatroomList.SelectedIndex = i));
                    break;
                }
        }
        private void ChooseRoom(object sender, EventArgs e)
        {
            if (chatroomList.SelectedIndex != -1 && chatroomList.SelectedItem.ToString() != room)
            {
                SendToStream(new MessageClass(codes.SENDING_SELECTED_ROOM, chatroomList.SelectedItem.ToString()), ref client);
                inputTextBox.Enabled = true;
                room = chatroomList.SelectedItem.ToString();
                UpdateListboxSelection();
            }
        }
        /// <summary>
        /// Воспроизведение звука уведомления.
        /// </summary>
        private void PlaySound()
        {
            try
            {
                SoundPlayer notification = new SoundPlayer("notification.wav");
                notification.Play();
            }
            catch (Exception e)
            {
                PrintToMessageBox(e.Message);
                PrintToMessageBox("Unable to find \"notification.wav\"");
            }
        }
        private void ToolStripDisconnectClick(object sender, EventArgs e)
        {
            SendToStream(new MessageClass(codes.SENDING_DISCONNECT_MESSAGE), ref client);
            Disconnect();
            loggedIn = false;
            chatroomList.Enabled = false;
            inputTextBox.Enabled = false;
            Text = "IRC";
        }
        /// <summary>
        /// Закрытие всех объектов, отвечающих за подключение 
        /// </summary>
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
        /// <summary>
        /// Открытие диалогового окна со вводом имени новой комнаты
        /// </summary>
        /// <param name="title"></param>
        private void AddRoom(string title = "Enter room name")
        {
            BeginInvoke(new Action(() => {
                NewRoomDialog meh = new NewRoomDialog(ref client, title);
                meh.Show();
            }));
        }
        private void ToolStripDeleteClick(object sender, EventArgs e)
        {
            if (room != null)
            {
                SendToStream(new MessageClass(codes.REQUESTING_ROOM_DELETING), ref client);
                GoToOpenSpace();
            }
        }
        private void ToolStripLeaveClick(object sender, EventArgs e)
        {
            if (room != null)
            {
                SendToStream(new MessageClass(codes.LEAVING_ROOM), ref client);
                GoToOpenSpace();
            }
        }
        /// <summary>
        /// Выход в "пространство" вне комнат
        /// </summary>
        private void GoToOpenSpace()
        {
            room = null;
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
            SendToStream(new MessageClass(codes.SENDING_DISCONNECT_MESSAGE), ref client);
            Disconnect();
        }
        // Чтобы убирать глупое выделение
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ActiveControl = null;
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            messageBox.SelectionStart = messageBox.Text.Length;
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
