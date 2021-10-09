using System.Collections.Generic;

namespace vtb_backend.Models
{
    public class Header
    {
        public string dayName { get; set; }
        public int imageId { get; set; }
        public string text { get; set; }
        public List<Button> buttons { get; set; }
        public List<Message> messages { get; set; }
    }


    public class Message
    {
        public int type { get; set; }
        public int imageId { get; set; }
        public Item[] textList { get; set; }
        public string text { get; set; }
        public List<Button> buttons { get; set; }
    }

    public class Button
    {
        public int answer_id { get; set; }
        public bool is_correct { get; set; }
        public string text { get; set; }
    }

    public class Item
    {
        public string text { get; set; }
        public int imageId { get; set; }
    }
}