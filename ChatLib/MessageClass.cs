using System.Collections.Generic;

namespace ChatLib
{
    /// <summary>
    /// Класс, представляющий собой единицу передачи данных между клиентом и сервером
    /// </summary>
    public class MessageClass
    {
        public Interactions.codes code { get; private set; }
        public string info { get; private set; }
        public List<string> list { get; private set; }
        public MessageClass(Interactions.codes code, string info = "", List<string> list = null)
        {
            this.code = code;
            this.info = info;
            this.list = list;
        }
    }
}
