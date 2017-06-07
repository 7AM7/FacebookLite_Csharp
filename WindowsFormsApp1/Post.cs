using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Post : UserControl
    {
        public Post()
        {
            InitializeComponent();
        }
        public Post(Image Pic,string Name, string postText)
        {
            InitializeComponent();
            PostPic.Image = Pic;
            lblPostTeXT.Text = postText;
            LblName.Text = Name;
            Setheight();
        }
        private void Setheight()
        {
            var maxSize = new Size(495, int.MaxValue);
            var g = CreateGraphics();
            var size = g.MeasureString(lblPostTeXT.Text, lblPostTeXT.Font, lblPostTeXT.Width);

            lblPostTeXT.Height = int.Parse(Math.Round(size.Height + 2, 0).ToString());
            LblName.Top = lblPostTeXT.Top + 10;
            Height = LikeBtn.Bottom + 10 ;
        }

        private void Post_Resize(object sender, EventArgs e)
        {
            Setheight();
        }
    }
}
