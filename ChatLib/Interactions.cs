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
            SENDING_USERLIST,
            SENDING_MESSAGE_HISTORY,
            SENDING_BROADCAST_MESSAGE,
            SENDING_CHAT_MESSAGE,
            SENDING_SELECTED_ROOM,
            SENDING_DISCONNECT_MESSAGE,
            REQUESTING_ROOMLIST,
            REQUESTING_USERLIST,
            REQUESTNG_MESSAGE_HISTORY,
            REQUESTING_USERNAME,
            CONFIRMING_USERNAME
        }
        // Отправка в поток преобразованного в json объекта класса Message.
        public static void sendToStream(Message message, ref NetworkStream stream)
        {
            byte[] data = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(message));
            if (stream != null && stream.CanWrite)
                stream.Write(data, 0, data.Length);
        }
        // Получение сообщения из потока и его расшифровка из jsona в объект класса Message.
        public static Message getFromStream(ref NetworkStream stream)
        {
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
