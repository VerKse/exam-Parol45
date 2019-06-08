using System;
using System.Windows.Forms;
using System.Text;
using System.Net.Sockets;
using ChatClient.lib;

namespace ChatClient.lib
{
    static class Routines
    {
        public static void printToMessageBox(string message, RichTextBoxEx messageBox)
        {
            messageBox.BeginInvoke(new Action(() => messageBox.AppendText("\n" + message)));
            messageBox.BeginInvoke(new Action(() => messageBox.SelectionStart = messageBox.Text.Length - 1));
            messageBox.BeginInvoke(new Action(() => messageBox.ScrollToCaret()));
        }
        public static void sendToStream(string message, ref NetworkStream stream)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
        public static string getFromStream(ref NetworkStream stream)
        {
            StringBuilder builder = new StringBuilder();
            byte[] data = new byte[64];
            int bytes = 0;
            string message = "";
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));

                message += builder.ToString();
                builder.Clear();
            }
            while (stream.DataAvailable);
            return message;
        }
    }
}
