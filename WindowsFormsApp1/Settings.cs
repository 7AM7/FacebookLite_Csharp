using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
   public class Settings
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public string WorkAt { get; set; }
        public Image ProfileImage { get; set; }
        public Image CoverImage { get; set; }
    }
}
