using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Model
{
    public class Chat
    {
        public Chat()
        {
            UniCO = DateTime.UtcNow.Ticks;
            ProfilePic = "ms-appx:///Assets/male-01.png";
            CreatedTime = DateTime.UtcNow;
            TimeZoneInfo localZoneId = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
            CreatedTime= TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localZoneId);
            CreatedTimeString = CreatedTime.ToString("hh:mm tt");
        }
        [PrimaryKey,AutoIncrement]
        public int Index { get; set; }
        public long UniCO { get; set; }
        public long SenderId { get; set; }
        public long RecieverId { get; set; }
        public string SenderName { get; set; }
        public string RecieverName { get; set; }
        public string Msg { get; set; }
        public string ProfilePic { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedTimeString { get; set; }


    }
}
