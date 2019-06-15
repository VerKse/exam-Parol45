using System;
using System.Text;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace ChatLib
{
    public static class Interactions
    {
        // Унифицированный формат кодов для "общения" клиента и сервера.
        public enum codes
        {
            SENDING_USERNAME,
            SENDING_SELECTED_ROOM,
            SENDING_CHAT_MESSAGE,
            SENDING_DISCONNECT_MESSAGE,
            SENDING_ROOMLIST,
            SENDING_USERLIST,
            SENDING_CHAT_HIST,
            SENDING_BROADCAST_MESSAGE,
            REQUESTING_ROOMLIST,
            REQUESTING_USERLIST,
            REQUESTING_CHAT_HIST,
            REQUESTING_NEW_ROOM,
            REQUESTING_ROOM_DELETING,
            REQUESTING_USERNAME,
            CONFIRMING_USERNAME,
            LEAVING_ROOM,
            EXISTING_ROOM_NAME
        }
        // Отправка в поток преобразованного в json объекта класса Message.
        public static void SendToStream(Message message, ref TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                string temp = JsonConvert.SerializeObject(message);
                byte[] data = Encoding.Unicode.GetBytes((temp.Length * 2).ToString() + temp);
                if (stream != null && stream.CanWrite)
                    stream.Write(data, 0, data.Length);
            }
            catch
            {
                if (client != null)
                    client.Close();
            }
        }
        // Получение сообщения из потока и его расшифровка из jsona в объект класса Message.
        public static Message GetFromStream(ref TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StringBuilder builder = new StringBuilder();
            byte[] data = new byte[64];
            int toGet = 0, got = 0, from = 0, bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, (toGet != 0 && toGet - got < data.Length ? toGet - got : data.Length));
                got += bytes;
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                if (toGet == 0 && builder.Length > 0)
                {
                    string temp = builder.ToString();
                    toGet = int.Parse(temp.Substring(0, temp.IndexOf("{")));
                    from = (int)Math.Log10(toGet) + 1;
                    got -= from * 2;
                }
            }
            while (toGet == 0 || toGet > got);
            return JsonConvert.DeserializeObject<Message>(builder.ToString().Substring(from));
        }
    }
}
