using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    // Класс, представляющий собой единицу передачи данных между клиентом и сервером.
    public class Message
    {
        public Interactions.codes code { get; private set; }
        public string info { get; private set; }
        public List<string> list { get; private set; }
        public List<string> list2 { get; private set; }
        public Message(Interactions.codes code, string info = "", List<string> list = null, List<string> list2 = null)
        {
            this.code = code;
            this.info = info;
            this.list = list;
            this.list2 = list2;
        }
    }
}
