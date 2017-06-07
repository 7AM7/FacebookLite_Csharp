using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
   public class Friends
    {
        public string Username { get; set; }
        public string Friendname { get; set; }
        public string SenderName { get; set; }
        public string ReciveName { get; set; }
        public int isAccepted { get; set; }
        public Image friendimage { get; set; }
        public override string ToString()
        {
            return string.Format("{0}\n{1}\n{2}\n{3}\n",
                Username, Friendname, SenderName, ReciveName);
        }
    }

}
