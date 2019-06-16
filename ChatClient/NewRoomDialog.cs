using System;
using System.Net.Sockets;
using System.Windows.Forms;
using static ChatLib.Interactions;

namespace ChatClient
{
    public partial class NewRoomDialog : Form
    {
        TcpClient client;
        public NewRoomDialog(ref TcpClient client, string title)
        {
            InitializeComponent();
            this.client = client;
            Text = title;
        }

        private void TryToCreateRoom(object sender, EventArgs e)
        {
            string toSend = roomName.Text.Trim(' ');
            if (toSend.Length > 0)
            {
                SendToStream(new ChatLib.Message(codes.REQUESTING_NEW_ROOM, toSend), ref client);
                Close();
            }
        }

        private void roomName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TryToCreateRoom(sender, e);
                e.Handled = true;
            }
        }
    }
}
