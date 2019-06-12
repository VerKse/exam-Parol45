using System;
using System.Text;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace ChatLib
{
    public static class Interactions
    {
        // Унифицированный формат кодов для "общения" клиента и сервера.
        public enum codes {
            SENDING_USERNAME,
            SENDING_ROOMLIST,
            SENDING_ROOM_NAME,
            SENDING_CHAT_INFO,
            SENDING_BROADCAST_MESSAGE,
            SENDING_CHAT_MESSAGE,
            SENDING_SELECTED_ROOM,
            SENDING_DISCONNECT_MESSAGE,
            REQUESTING_ROOMLIST,
            REQUESTING_CHAT_INFO,
            REQUESTING_USERNAME,
            CONFIRMING_USERNAME
        }
        // Отправка в поток преобразованного в json объекта класса Message.
        public static void SendToStream(Message message, ref TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
            byte[] data = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(message));
            if (stream != null && stream.CanWrite)
                stream.Write(data, 0, data.Length);
            }
            catch
            {
                if (client != null)
                    client.Close();
            }
        }
        // Получение сообщения из потока и его расшифровка из jsona в объект класса Message (Стрим должен быть живой).
        public static Message GetFromStream(ref TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StringBuilder builder = new StringBuilder();
            byte[] data = new byte[64];
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return JsonConvert.DeserializeObject<Message>(builder.ToString());
        }
    }
}
