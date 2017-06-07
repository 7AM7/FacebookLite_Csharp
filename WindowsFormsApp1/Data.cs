using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
   public class Data
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Info { get; set; }
        public DateTime Datetime { get; set; }
        public byte []image { get; set; }
        public override string ToString()
        {
            return string.Format("name:{0}, pass:{1}, info: {2} date:{3}",
                Username, Password, Info,Datetime);
        }
    }
}
