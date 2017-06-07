using MetroFramework.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    public partial class Main : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        SqlConnection conn = new SqlConnection(
        "Data Source=DESKTOP-AJ8DB4G;" +
        "Initial Catalog=Users;" +
        "Integrated Security=SSPI;");
        SqlCommand cmd;
        int id = 0;
        Settings settings = new Settings();
        Friends frs = new Friends();
        int x = 0;
        int i =1;
        
        Image imging; Image imging3;
        Image imging4;
        Image imging1;
        string sendername;
        string friendname;
        int friendcount = 0;
        byte[] sender_photo;
        byte[] reciv_photo;
        int y = 10;
        byte[] photo_aray;
        byte[] photo_arayCover;
        string BirthDay;
        string gender;
        string FriendUsrName = "";
        List<Data> dataa = new List<Data>();
        List<string> WaitList = new List<string>();
        List<string> FinishList = new List<string>();
        List<Friends> AddList = new List<Friends>();
        List<Friends> Listiiing = new List<Friends>();
        bool isMyFriendShow = false;
        string  requstname = "";
        int z = 1;

        private bubble bbl_old = new bubble();
        private Post post_old = new Post();
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        int sum = 0;
        Socket sck1, sck2;
        EndPoint eplocal, epremote;
        byte[] buffer;
        Random random = new Random();
        int[] number = new int[2];
        string localip, remoteip;
        int localport, remoteport;
        string Name1, Name2;
        Hashtable emotions;
        
        public int _idd
        {
            set { id = value; }
        }
        public string FrooName { get; set; }
        public Main()
        {
           
            InitializeComponent();
            bbl_old.Top = 0 - bbl_old.Height + 10;
            post_old.Top = 0 - post_old.Height + 10;
            var pos = this.PointToScreen(MyFSname.Location);
            pos = CoverPic.PointToClient(pos);
            MyFSname.Parent = CoverPic;
            // MyFSname.Location = pos;
            MyFSname.BackColor = Color.Transparent;

            FriendFSname.Parent = FrCoverPic;
            // MyFSname.Location = pos;
            FriendFSname.BackColor = Color.Transparent;
            DisplayData();
        }
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tab_Click(object sender, EventArgs e)
        {
            bar.Top = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Top;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
        }




        public int Idd
        {
            get { return id; }
            set { id = value; }
        }


        private void Profile_tab_Click(object sender, EventArgs e)
        {

            try
            {

                //FrList.ClearSelected();
                // FrList.SelectionMode = SelectionMode.None;

                FrList.ClearSelected();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex.Message);
            }
            ProfilePanel.Show();
            MessangerPanel.Hide();
            FrProfilePanel.Hide();
            ChatName.Visible = false;
            ChatPic.Visible = false;
            ChatStatus.Visible = false;
            BackBtn.Visible = false;
            tab_Click(sender, e);

        }

        private void home_tab_Click(object sender, EventArgs e)
        {
            ProfilePanel.Hide();
            MessangerPanel.Hide();
            ChatName.Visible = false;
            ChatPic.Visible = false;
            ChatStatus.Visible = false;
            BackBtn.Visible = false;
            //clearWindow1.Show();
            // myProfile1.Hide();

            //clearWindow1.BringToFront();
            //homeTab1.BringToFront();
            // new HomeTab().BringToFront();
            tab_Click(sender, e);
        }

        private void Main_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        void DisplayData()
        {

            try
            {
                string query = "SELECT * FROM UserData WHERE Usr_id = '" + id + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count == 1)
                {
                    conn.Open();
                    cmd = new SqlCommand("Select * from UserData where  Usr_id = '" + id + "'", conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            settings.Email = reader.GetString(3).ToString();
                            settings.FirstName = reader.GetString(4).ToString();
                            settings.SecondName = reader.GetString(5).ToString();
                            settings.Status = reader.GetString(12).ToString();
                            settings.WorkAt = reader.GetString(13).ToString();
                            settings.Mobile = reader.GetString(14).ToString();
                            settings.BirthDay = reader.GetDateTime(6);
                            settings.Country = reader.GetString(7).ToString();
                            settings.City = reader.GetString(8).ToString();
                            gender = reader.GetString(9).ToString();
                            settings.UserName = reader.GetString(1).ToString(); ;
                            if (gender == "Male")
                                AboutGender.selectedIndex = 0;
                            else if (gender == "Female")
                                AboutGender.selectedIndex = 1;

                            long len1 = reader.GetBytes(11, 0, null, 0, 0);
                            Byte[] buffer1 = new Byte[len1];
                            reader.GetBytes(11, 0, buffer1, 0, (int)len1);
                            settings.CoverImage = byteArrayToImage(buffer1);

                            long len = reader.GetBytes(10, 0, null, 0, 0);
                            Byte[] buffer = new Byte[len];
                            reader.GetBytes(10, 0, buffer, 0, (int)len);
                            settings.ProfileImage = byteArrayToImage(buffer);

                        }
                        CoverPic.Image = settings.CoverImage;
                        Status_txt.Text = settings.Status;
                        NameLabel.Text = FirstCharToUpper(settings.FirstName) + " " + FirstCharToUpper(settings.SecondName);
                        UserPic.Image = settings.ProfileImage;
                        aboutEmail.Text = settings.Email;
                        aboutFirstName.Text = FirstCharToUpper(settings.FirstName);
                        aboutScondName.Text = FirstCharToUpper(settings.SecondName);
                        ProfilePic.Image = settings.ProfileImage;
                        aboutCity.Text = settings.City;

                        aboutWork.Text = settings.WorkAt;
                        aboutCountry.Text = settings.Country;
                        AboutMobile.Text = settings.Mobile;
                        postPic.Image = settings.ProfileImage;
                        CityLabel.Text = settings.City;
                        CounrtyLabel.Text = settings.City + " City," + settings.Country;
                        WorkAtLabel.Text = settings.WorkAt;
                        BirthDay = settings.BirthDay.ToString();
                        string[] filterWord = BirthDay.Split(' ');
                        BirthDayLabel.Text = filterWord[0];
                        aboutBithDay.Value = settings.BirthDay;
                        MyFSname.Text = FirstCharToUpper(settings.FirstName) + " " + FirstCharToUpper(settings.SecondName);
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:" + ex.Message);
                Console.WriteLine("" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        private void Main_Load(object sender, EventArgs e)
        {
            //aboutFirstName.ForeColor = System.Drawing.SystemColors.Window; ;

            postTxt.GotFocus += PostTxt_GotFocus;
            postTxt.LostFocus += PostTxt_LostFocus;
            DisplayData();
            ReadFriend();
            FrProfilePanel.Hide();
            DisplayDataFr();
            DisplayDataFinish();
            

            FrindLabel.Text = "Friends("+FrFriendList.Items.Count+")";
            MessangerPanel.Hide();



            //DisplayButtonAdd();
            // DisplayButtonAdd();
        }
        private void PostTxt_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "What's on your min....?")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void PostTxt_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "What's on your min....?";
                tb.ForeColor = Color.LightGray;
            }
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public byte[] ImageToByte2(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        private void UserPic_Click(object sender, EventArgs e)
        {


        }

        private void ProfilePic_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                ProfilePic.Image = Image.FromFile(openFileDialog1.FileName);
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE UserData SET ProfileImage=@ProfileImage WHERE usr_id=@usr_id", conn);
                    cmd.Parameters.AddWithValue("@usr_id", id);
                    photo_aray = ImageToByte2(ProfilePic.Image);
                    cmd.Parameters.AddWithValue("@ProfileImage", photo_aray);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Data is updated");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error:" + ex.Message);
                    Console.WriteLine("" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void tabb_Click(object sender, EventArgs e)
        {
            profileBar.Left = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Left;
        }
        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            about_panl.Hide();
            MyFirendPanel.Hide();
            timeline_panel.Show();
            timer1.Start();
            isMyFriendShow = false;
            tabb_Click(sender, e);
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            about_panl.Show();
            MyFirendPanel.Hide();
            timeline_panel.Hide();
            isMyFriendShow = false;
            timer1.Start();
            tabb_Click(sender, e);
        }

        private void post_btn_Click(object sender, EventArgs e)
        {
            // timer1.Stop();
            if (postTxt.Text != "" && postTxt.Text != "What's on your min....?")
            {

                Console.WriteLine(y);
                Panel pp = new Panel();
                PictureBox pic = new PictureBox();
                TextBox Textb = new TextBox();
                Label text1 = new Label();
                Bunifu.Framework.UI.BunifuFlatButton Like_btn = new Bunifu.Framework.UI.BunifuFlatButton();
                Textb.ReadOnly = true;
                Textb.ForeColor = System.Drawing.SystemColors.Window;
                Textb.ForeColor = Color.White;

                // pp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                pp.Controls.Add(Textb);
                pp.Controls.Add(pic);
                pp.Controls.Add(Like_btn);
                pp.Size = new System.Drawing.Size(418, 128);
                pp.BackColor = Color.White;

                pp.Location = new Point(x, y);
                //pp.AutoSize = true;
                panel2.Controls.Add(pp);


                text1.AutoSize = true;
                text1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                text1.Location = new System.Drawing.Point(50, 15);
                text1.Size = new System.Drawing.Size(89, 15);
                text1.TabIndex = 1;
                text1.Text = FirstCharToUpper(settings.FirstName)+" "+ FirstCharToUpper(settings.SecondName);
                text1.ForeColor = Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
                pp.Controls.Add(text1);


                Textb.BorderStyle = System.Windows.Forms.BorderStyle.None;
                Textb.Location = new System.Drawing.Point(62, 39);
                Textb.Multiline = true;
                Textb.Size = new System.Drawing.Size(345, 52);
                Textb.TabIndex = 0;
                Textb.Text = postTxt.Text;

                try
                {

                    string query = "SELECT * FROM UserData WHERE Usr_id = '" + id + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);

                    if (dtbl.Rows.Count == 1)
                    {
                        conn.Open();
                        cmd = new SqlCommand("Select * from UserData where  Usr_id = '" + id + "'", conn);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                long len = reader.GetBytes(10, 0, null, 0, 0);
                                Byte[] buffer = new Byte[len];
                                reader.GetBytes(10, 0, buffer, 0, (int)len);
                                settings.ProfileImage = byteArrayToImage(buffer);

                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error:" + ex.Message);
                    Console.WriteLine("" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }

                pic.Image = settings.ProfileImage;
                pic.Location = new System.Drawing.Point(4, 4);
                pic.Size = new System.Drawing.Size(40, 40);
                pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;


                Like_btn.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
                Like_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(223)))), ((int)(((byte)(226)))));
                Like_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                Like_btn.BorderRadius = 0;
                Like_btn.ButtonText = "Like";
                Like_btn.Cursor = System.Windows.Forms.Cursors.Hand;
                Like_btn.DisabledColor = System.Drawing.Color.Gray;
                Like_btn.ForeColor = System.Drawing.Color.Black;
                Like_btn.Iconcolor = System.Drawing.Color.Transparent;
                Like_btn.Iconimage = global::WindowsFormsApp1.Properties.Resources.thumbs_up_hand_symbol__1_;
                Like_btn.Iconimage_right = null;
                Like_btn.Iconimage_right_Selected = null;
                Like_btn.Iconimage_Selected = null;
                Like_btn.IconMarginLeft = 0;
                Like_btn.IconMarginRight = 0;
                Like_btn.IconRightVisible = true;
                Like_btn.IconRightZoom = 0D;
                Like_btn.IconVisible = true;
                Like_btn.IconZoom = 50D;
                Like_btn.IsTab = false;
                Like_btn.Location = new System.Drawing.Point(62, 97);
                Like_btn.Normalcolor = System.Drawing.Color.Transparent;
                Like_btn.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                Like_btn.OnHoverTextColor = System.Drawing.Color.Black;
                Like_btn.selected = false;
                Like_btn.Size = new System.Drawing.Size(66, 28);
                Like_btn.Text = "Like";
                Like_btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                Like_btn.Textcolor = System.Drawing.Color.Black;
                Like_btn.TextFont = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                y += (pp.Height + 10);
                //x = 322;
                postTxt.Text = "";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|png|*.png|all files|*.*";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                CoverPic.Image = Image.FromFile(openFileDialog1.FileName);
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE UserData SET CoverImage=@CoverImage WHERE usr_id=@usr_id", conn);
                    cmd.Parameters.AddWithValue("@usr_id", id);
                    photo_arayCover = ImageToByte2(CoverPic.Image);
                    cmd.Parameters.AddWithValue("@CoverImage", photo_arayCover);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Data is updated");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error:" + ex.Message);
                    Console.WriteLine("" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }



        private void postTxt_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "What's on your min....?")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void postTxt_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "What's on your min....?";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DisplayData();
            // requstname = FrRequstList.SelectedItem.ToString();
            FrindLabel.Text = "Friends(" + FrFriendList.Items.Count + ")";
            if (isMyFriendShow == false)
            {
                DisplayDataFr();
                DisplayDataFinish();
            }
           // DisplayFriendCount();
           // DisplayButtonAdd();
          // DisplayButtonAdd();
            // DisplayFriendData();
            //if (timer1.Interval == 10000)
            //{
            //    timer1.Stop();
            //    timer1.Enabled = false;
            //}
        }

        private void aboutFirstName_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBox_Fname.CheckState == CheckState.Checked)
            {
                update_btn.Enabled = true;
                aboutFirstName.Enabled = true;
                aboutBithDay.Enabled = true;
                aboutCity.Enabled = true;
                aboutCountry.Enabled = true;
                aboutEmail.Enabled = true;
                AboutGender.Enabled = true;
                AboutMobile.Enabled = true;
                aboutScondName.Enabled = true;
                aboutWork.Enabled = true;
                timer1.Stop();
            }
            else
            {
                update_btn.Enabled = false;
                aboutFirstName.Enabled = false;
                aboutBithDay.Enabled = false;
                aboutCity.Enabled = false;
                aboutCountry.Enabled = false;
                aboutEmail.Enabled = false;
                AboutGender.Enabled = false;
                AboutMobile.Enabled = false;
                aboutScondName.Enabled = false;
                aboutWork.Enabled = false;

                timer1.Start();
            }

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Status_txt.Enabled == false)
            {
                linkLabel1.Text = "UpdateStatus";
                Status_txt.Enabled = true;
                Status_txt.BorderColorIdle = Color.Black;
                timer1.Stop();
            }
            else
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE UserData SET Status=@Status WHERE usr_id=@usr_id", conn);
                    cmd.Parameters.AddWithValue("@usr_id", id);

                    cmd.Parameters.AddWithValue("@Status", Status_txt.Text);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Data is updated");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error:" + ex.Message);
                    Console.WriteLine("" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                Status_txt.Enabled = false;
                linkLabel1.Text = "ChangeStatus";
                Status_txt.BorderColorIdle = Color.Transparent;
                timer1.Start();
            }
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            if (AboutGender.selectedIndex == 0)
                gender = AboutGender.selectedValue;
            else if (AboutGender.selectedIndex == 1)
                gender = AboutGender.selectedValue;
            if (chkBox_Fname.Checked)
            {

                try
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE UserData SET FirstName=@FirstName,SecondName=@SecondName,Email=@Email,Birthday=@Birthday,Country=@Country,City=@City,Gender=@Gender,WorkAt=@WorkAt,PhoneNumber=@PhoneNumber WHERE usr_id=@usr_id", conn);
                    //cmd = new SqlCommand("INSERT INTO ProfileInfo VALUES (@usr_id,@first_name,@second_name,@Status,@birthday,@country,@city)", conn);
                    cmd.Parameters.AddWithValue("@usr_id", id);
                    cmd.Parameters.AddWithValue("@FirstName", aboutFirstName.Text.ToString());
                    cmd.Parameters.AddWithValue("@Email", aboutEmail.Text.ToString());
                    cmd.Parameters.AddWithValue("@SecondName", aboutScondName.Text.ToString());
                    cmd.Parameters.AddWithValue("@Birthday", aboutBithDay.Value);
                    cmd.Parameters.AddWithValue("@Country", aboutCountry.Text.ToString());
                    cmd.Parameters.AddWithValue("@City", aboutCity.Text.ToString());
                    cmd.Parameters.AddWithValue("@WorkAt", aboutWork.Text.ToString());
                    cmd.Parameters.AddWithValue("@PhoneNumber", AboutMobile.Text.ToString());
                    cmd.Parameters.AddWithValue("@Gender", gender.ToString());
                    cmd.ExecuteNonQuery();
                    timer1.Start();
                    MessageBox.Show("Data is updated");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);
                    Console.WriteLine("" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void Status_txt_OnValueChanged(object sender, EventArgs e)
        {
            //timer1.Stop();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            conn.Close();
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();

            this.Hide();

        }

        private void FrpostTxt_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "What's on your min....?";
                tb.ForeColor = Color.LightGray;
            }
        }

        private void FrpostTxt_GotFocus(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "What's on your min....?")
            {
                tb.Text = "";
                tb.ForeColor = Color.Black;
            }
        }

        private void FrList_Click(object sender, EventArgs e)
        {

            //int id = ((exListBoxItem)FrList.SelectedItem).Id;
            ////if (id != -1)
            ////{
            //    ProfilePanel.Hide();
            //    FrProfilePanel.Show();
            //    DisplayFriendData();
            //    FrpostTxt.GotFocus += FrpostTxt_GotFocus;
            //    FrpostTxt.LostFocus += FrpostTxt_LostFocus;
            // FrProfilePanel.BringToFront();
            //ProfilePanel.SendToBack();
            //n.FrindUsrName = listFriend.SelectedItem.ToString();
            //}
        }
        public void ReadFriend()
        {
            Image img;
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * FROM UserData", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FriendUsrName = reader.GetString(1).ToString();

                        long len = reader.GetBytes(10, 0, null, 0, 0);
                        Byte[] buffer = new Byte[len];
                        reader.GetBytes(10, 0, buffer, 0, (int)len);
                        img = byteArrayToImage(buffer);

                        if (FriendUsrName != settings.UserName)
                        {
                            FrList.Items.Add(new exListBoxItem(i, FriendUsrName, "", img));
                            i++;
                        }

                      //  Console.WriteLine(i);
                        //listFriend.Items.Add(FriendUsrName);
                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error:" + ex.Message);
                Console.WriteLine("" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        void DisplayFriendData()
        {
            string FrName = ((exListBoxItem)FrList.SelectedItem).Title.ToString();
            try
            {
                string query = "SELECT * FROM UserData WHERE Username = '" + FrName + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count == 1)
                {
                    conn.Open();
                    cmd = new SqlCommand("Select * from UserData where  Username = '" + FrName + "'", conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FraboutEmail.Text = reader.GetString(3).ToString();
                            FraboutFirstName.Text = FirstCharToUpper(reader.GetString(4).ToString());
                            FraboutScondName.Text = FirstCharToUpper(reader.GetString(5).ToString());
                            FrStatus_txt.Text = reader.GetString(12).ToString();
                            FraboutWork.Text = reader.GetString(13).ToString();
                            FrAboutMobile.Text = reader.GetString(14).ToString();
                            string BirthDay1 = reader.GetDateTime(6).ToString();
                            string[] filterWord = BirthDay1.Split(' ');
                            FrBirthDayLabel.Text = filterWord[0];
                            FrBirthDay.Value = reader.GetDateTime(6);

                            FrFraboutCity.Text = reader.GetString(8).ToString();
                            FraboutCountry.Text = reader.GetString(7).ToString();
                            FrCityLabel.Text = reader.GetString(8).ToString();
                            FrCounrtyLabel.Text = FrFraboutCity.Text + " City," + reader.GetString(7).ToString(); ;
                            FrWorkAtLabel.Text = FraboutWork.Text;
                            gender = reader.GetString(9).ToString();
                            //  settings.UserName = reader.GetString(1).ToString(); ;
                            if (gender == "Male")
                                FrAboutGender.selectedIndex = 0;
                            else if (gender == "Female")
                                FrAboutGender.selectedIndex = 1;

                            long len1 = reader.GetBytes(11, 0, null, 0, 0);
                            Byte[] buffer1 = new Byte[len1];
                            reader.GetBytes(11, 0, buffer1, 0, (int)len1);
                            FrCoverPic.Image = byteArrayToImage(buffer1);

                            long len = reader.GetBytes(10, 0, null, 0, 0);
                            Byte[] buffer = new Byte[len];
                            reader.GetBytes(10, 0, buffer, 0, (int)len);
                            FriendProfile_pic.Image = byteArrayToImage(buffer);
                            FrpostPic.Image = settings.ProfileImage;

                            FriendFSname.Text = FraboutFirstName.Text + " " + FraboutScondName.Text;

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error:" + ex.Message);
                Console.WriteLine("" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void AddToFriends_btn_Click(object sender, EventArgs e)
        {
            string FrName = ((exListBoxItem)FrList.SelectedItem).Title.ToString();
            Image FrImage = ((exListBoxItem)FrList.SelectedItem).ItemImage;

            try
            {
                // string query = "SELECT * FROM MyFriends WHERE Friendname = '" + FrName + "'and Username= '" + settings.UserName + "'or Friendname= '" + settings.UserName+ "'or isAccepted= '" + -1+"'and isAccepted= '" + 1+ "'and Username = '" + FrName + "'";
                string query = "SELECT * FROM MyFriends WHERE Friendname = '" + FrName + "'and Username = '" + settings.UserName+ "'or Username = '" + FrName + "'or Friendname= '" + settings.UserName + "'and SenderName= '" + settings.UserName+ "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                Console.WriteLine(query);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count < 1)
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO MyFriends(Username,Friendname,SenderName,ReciveName,SenderImage,ReciveImage,isAccepted) VALUES (@Username,@Friendname,@SenderName,@ReciveName,@SenderImage,@ReciveImage,@isAccepted)", conn);
                    cmd.Parameters.AddWithValue("@Username", settings.UserName);
                    cmd.Parameters.AddWithValue("@Friendname", FrName);
                    cmd.Parameters.AddWithValue("@SenderName", settings.UserName);
                    cmd.Parameters.AddWithValue("@ReciveName", FrName);
                    sender_photo= ImageToByte2(settings.ProfileImage);
                    cmd.Parameters.AddWithValue("@SenderImage", sender_photo);
                    reciv_photo = ImageToByte2(FrImage);
                    cmd.Parameters.AddWithValue("@ReciveImage", reciv_photo);
                    cmd.Parameters.AddWithValue("@isAccepted", -1);

                    cmd.ExecuteNonQuery();

                }
                else
                {
                    //AddToFriends_btn.Visible = false;
                    //RequstLabel.Text = "This In Wating...";
                    //RequstLabel.Visible = true;
                    MessageBox.Show("This is in waiting", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
                Console.WriteLine("" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        void DisplayButtonAdd()
        {
            friendcount = 0;
            string FrName = ((exListBoxItem)FrList.SelectedItem).Title.ToString();
            /// string FrName1 = ((exListBoxItem)FrList.Items.ToString()).Title.ToString();
            for (int i = 0; i < FinishList.Count; i++)
            {
                if (FrName == FinishList[i])
                {
                    Console.WriteLine(FrName + " its firend");
                    AddToFriends_btn.Visible = false;
                    RequstLabel.Text = "It's Firend With You";
                    RequstLabel.Visible = true;
                    Message_Btn.Visible = true;
                    break;
                }
                else 
                {
                    Console.WriteLine(FrName + " its not firend");
                    AddToFriends_btn.Visible = true;
                    RequstLabel.Visible = false;
                    Message_Btn.Visible = false;

                }
            }

            }
        void DisplayDataFr()
        {
            FrRequstList.Items.Clear();
            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM MyFriends WHERE ReciveName = '" + settings.UserName + "'and isAccepted= '" + -1 + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    friendname = reader.GetString(2);
                    sendername = reader.GetString(3);

                    long len = reader.GetBytes(5, 0, null, 0, 0);
                    Byte[] buffer = new Byte[len];
                    reader.GetBytes(5, 0, buffer, 0, (int)len);
                    imging = byteArrayToImage(buffer);

                    //long len1 = reader.GetBytes(11, 0, null, 0, 0);
                    //Byte[] buffer1 = new Byte[len1];
                    //reader.GetBytes(11, 0, buffer1, 0, (int)len1);
                    //FrCoverPic.Image = byteArrayToImage(buffer1);

                    if (friendname == settings.UserName)
                    {
                        // Console.WriteLine("adas" + sendername);
                        FrRequstList.Items.Add(new exListBoxItem(z, sendername, "", imging));
                        //   FrRequstList.Items.Add(sendername);
                        //WaitList.Add(sendername);
                        z++;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        private void FrTimeLine_btn_Click(object sender, EventArgs e)
        {
            Frabout_panl.Hide();
            Frtimeline_panel.Show();
            Frbar_Click(sender, e);
        }

        private void FrAbout_btn_Click(object sender, EventArgs e)
        {
            Frabout_panl.Show();
            Frtimeline_panel.Hide();
            Frbar_Click(sender, e);
        }

        private void Frbar_Click(object sender, EventArgs e)
        {
            Frbar.Left = ((Bunifu.Framework.UI.BunifuFlatButton)sender).Left;
        }

        private void FrList_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id = ((exListBoxItem)FrList.SelectedItem).Id;

            //if (id != -1)
            //{
            if (FrList.SelectedIndex != -1 || id != -1)
            {
                ProfilePanel.Hide();
                FrProfilePanel.Show();
                DisplayFriendData();
                DisplayButtonAdd();
                FrpostTxt.GotFocus += FrpostTxt_GotFocus;
                FrpostTxt.LostFocus += FrpostTxt_LostFocus;
            }


            // FrProfilePanel.BringToFront();
            //ProfilePanel.SendToBack();
            //n.FrindUsrName = listFriend.SelectedItem.ToString();
            //}
        }

        private void FrList_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        void DisplayDataFinish()
        {
            FrFriendList.Items.Clear();
            FinishList.Clear();
            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM MyFriends WHERE  isAccepted= '" + 1 + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    string friendname = reader.GetString(2);
                    string sendername = reader.GetString(3);
                    long len = reader.GetBytes(5, 0, null, 0, 0);
                    Byte[] buffer = new Byte[len];
                    reader.GetBytes(5, 0, buffer, 0, (int)len);
                    imging = byteArrayToImage(buffer);

                    long len1 = reader.GetBytes(6, 0, null, 0, 0);
                    Byte[] buffer1 = new Byte[len1];
                    reader.GetBytes(6, 0, buffer1, 0, (int)len1);
                    imging1 = byteArrayToImage(buffer1);
                    if (friendname == settings.UserName)
                    {
                        //FrFriendList.Items.Add(sendername);
                        FrFriendList.Items.Add(new exListBoxItem(z, sendername, "", imging));
                        FinishList.Add(sendername);

                    }
                    else if (sendername == settings.UserName)
                    {
                        //FrFriendList.Items.Add(friendname);
                        FrFriendList.Items.Add(new exListBoxItem(z, friendname, "", imging1));
                        FinishList.Add(friendname);

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void MyFrienQFdsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void exListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            
            about_panl.Hide();
            timeline_panel.Hide();
            MyFirendPanel.Show();
            //timer1.Stop();
            tabb_Click(sender, e);
            isMyFriendShow = true;
        }

        private void FrRequstList_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void FrFriendList_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        
        private void accepted_btn_Click(object sender, EventArgs e)
        {
             try
            {
                requstname = FrRequstList.SelectedItem.ToString();
                Console.WriteLine("requst" + requstname);
                if (requstname != null || requstname != ""||FrRequstList.Items.Count==0)
                {
                    Console.WriteLine("requst" + requstname);
                    //string query = "SELECT * FROM MyFriends WHERE SenderName = '" + FrName + "'and isAccepted= '" + 0 + "'";
                    //SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                    //DataTable dtbl = new DataTable();
                    //sda.Fill(dtbl);

                    //if (dtbl.Rows.Count >1)
                    //{
                    conn.Open();
                    cmd = new SqlCommand("UPDATE MyFriends SET isAccepted=@isAccepted WHERE ReciveName=@ReciveName", conn);
                    // cmd = new SqlCommand("INSERT INTO MyFriends(Username,Friendname,isAccepted) VALUES (@Username,@Friendname,@isAccepted)", conn);
                    cmd.Parameters.AddWithValue("@ReciveName", settings.UserName);
                    cmd.Parameters.AddWithValue("@isAccepted", 1);
                    cmd.ExecuteNonQuery();
                    timer1.Start();
                    // }
                    // else
                    // MessageBox.Show("This is friend with you", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Select Firend First", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                catch (Exception ex)
                {

                //Console.WriteLine("requst" + requstname);
                MessageBox.Show("Select Firend First", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Error:" + ex.Message);
                Console.WriteLine("" + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            


        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            //requstname = ((exListBoxItem)FrList.SelectedItem).Title.ToString();
            string FrName = ((exListBoxItem)FrFriendList.SelectedItem).Title.ToString();
            try
            {
                Console.WriteLine("ccccccc1:" + FinishList.Count);
                //requstname = ((exListBoxItem)FrList.SelectedItem).Title.ToString();
                //Console.WriteLine("requst" + requstname);
                //string query = "SELECT * FROM MyFriends WHERE SenderName = '" + FrName + "'and isAccepted= '" + 0 + "'";
                //SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                //DataTable dtbl = new DataTable();
                //sda.Fill(dtbl);

                //if (dtbl.Rows.Count >1)
                //{

                conn.Open();
                cmd = new SqlCommand("DELETE FROM MyFriends WHERE Friendname = '" + FrName + "'and Username = '" + settings.UserName + "'or Username = '" + FrName + "'or Friendname= '" + settings.UserName + "'and SenderName= '" + settings.UserName + "'and isAccepted='"+1+"'", conn);
                
                   // cmd = new SqlCommand("DELETE FROM MyFriends WHERE (isAccepted=@isAccepted and ReciveName=@ReciveName or Sendername=@Sendername)", conn);
                   //cmd = new SqlCommand("UPDATE MyFriends SET isAccepted=@isAccepted WHERE ReciveName=@ReciveName", conn);
                   // cmd = new SqlCommand("INSERT INTO MyFriends(Username,Friendname,isAccepted) VALUES (@Username,@Friendname,@isAccepted)", conn);
                cmd.Parameters.AddWithValue("@ReciveName", settings.UserName);
                    cmd.Parameters.AddWithValue("@isAccepted", 1);
                    cmd.Parameters.AddWithValue("@Sendername", FrName);
                cmd.ExecuteNonQuery();
                // FinishList.Clear();
                Console.WriteLine("ccccccc2:" + FinishList.Count);
                //if (FinishList.Count != 0|| FinishList.Count != -1)
                //{
                //    for (int i = 0; i < FinishList.Count; i++)
                //    {
                //        if (FrName == FinishList[i])
                //        {
                //            FinishList.Remove(FrName);
                //        }
                //        if (settings.UserName == FinishList[i])
                //        {
                //            FinishList.Remove(settings.UserName);
                //        }
                //    }
                //}

                
                // timer1.Start();
                // }
                // else
                // MessageBox.Show("This is friend with you", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {

                //Console.WriteLine("requst" + requstname);
                MessageBox.Show("Select Firend First", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Error:" + ex.Message);
                Console.WriteLine("" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You Can't Show This User Data", "Oppps", MessageBoxButtons.OK);
        }

        private void Message_Btn_Click(object sender, EventArgs e)
        {
            sck1 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck1.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            localip = GetLocalIP();
            remoteip = GetLocalIP();
            
            CreateEmotions();
            try
            {

                MessangerPanel.Show();
                
                conn.Open();
                FrooName = ((exListBoxItem)FrList.SelectedItem).Title.ToString();
                cmd = new SqlCommand("SELECT * FROM MyFriends WHERE  isAccepted= '" + 1 + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    string FromName = reader.GetString(1);
                    string ToName = reader.GetString(2);

                    long len = reader.GetBytes(5, 0, null, 0, 0);
                    Byte[] buffer = new Byte[len];
                    reader.GetBytes(5, 0, buffer, 0, (int)len);
                    imging3 = byteArrayToImage(buffer);

                    long len1 = reader.GetBytes(6, 0, null, 0, 0);
                    Byte[] buffer1 = new Byte[len1];
                    reader.GetBytes(6, 0, buffer1, 0, (int)len1);
                    imging4 = byteArrayToImage(buffer1);

                    if (FromName == settings.UserName)
                    {
                        Name1 = settings.UserName;
                       // ChatPic.Image = imging3;
                    }
                    else
                    {
                        Name1 = FrooName;
                       // ChatPic.Image = imging4;
                    }
                    if (ToName == settings.UserName)
                    {
                        //ChatPic.Image = imging3;
                        Name2 = settings.UserName;
                    }
                    else
                    {
                        Name2 = FrooName;
                      //  ChatPic.Image = imging4;
                    }
                }

                if ((Name1 == settings.UserName && Name2 == FrooName))
                {
                    localport = 80;
                    remoteport = 81;
                    ChatName.Text = Name2;
                    ChatStatus.Text = "Offline";
                    ChatPic.Image = imging4;
                }

                if ((Name1 == FrooName && Name2 == settings.UserName))
                {
                    localport = 81;
                    remoteport = 80;
                    ChatName.Text = Name1  ;
                    ChatStatus.Text = "Offline";
                    ChatPic.Image = imging3;
                }
               // MessageBox.Show(Name1 + Name2 + localport + remoteport);
                eplocal = new IPEndPoint(IPAddress.Parse(localip), localport);
                sck1.Bind(eplocal);
                epremote = new IPEndPoint(IPAddress.Parse(remoteip), remoteport);
                sck1.Connect(epremote);

                buffer = new byte[1500];
                if (sck1.Connected == true)
                    sck1.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);
                BackBtn.Visible = true;
                ChatName.Visible = true;
                ChatPic.Visible = true;
                ChatStatus.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void panal3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            conn.Close();
            Environment.Exit(0);
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            ChatName.Visible = false;
            ChatPic.Visible = false;
            ChatStatus.Visible = false;
            BackBtn.Visible = false;


            FrProfilePanel.Show();
            MessangerPanel.Hide();

            if (sck1.Connected == true)
            {
                sck1.Close();
                //sck1.Disconnect(true);
            }

        }

        private void buttonsend_Click(object sender, EventArgs e)
        {
            try
            {


                if (!string.IsNullOrEmpty(txtmessage.Text))
                {

                    ASCIIEncoding aencoding = new ASCIIEncoding();
                    byte[] sendingmessage = new byte[1500];
                    sendingmessage = aencoding.GetBytes(txtmessage.Text);
                    if (sck1.Connected == true)
                    {
                        sck1.Send(sendingmessage);
                        addInMessage("Me" + ": " + txtmessage.Text, DateTime.Now.ToShortTimeString());
                    }
                    //if(sck2.Connected == true)
                    //    sck1.Send(sendingmessage);
                    //conn.Open();
                    //cmd = new SqlCommand("INSERT INTO Chat(Sender,Receiver,Message,Time,Seen) VALUES(@Sender,@Receiver,@Message,@Time,@Seen)", conn);
                    //cmd.Parameters.AddWithValue("@Sender", textBox1.Text);
                    //cmd.Parameters.AddWithValue("@Receiver", textBox2.Text);
                    //cmd.Parameters.AddWithValue("@Message", txtmessage.Text);
                    //cmd.Parameters.AddWithValue("@Time", DateTime.Now);
                    //cmd.Parameters.AddWithValue("@Seen", 0);
                    //cmd.ExecuteNonQuery();

                    //Listmessage.Items.Add(textBox1.Text + ":" + txtmessage.Text);
                    txtmessage.Text = "";

                    // addOutMessage("Hello world to you too", "00:00");
                    panel26.VerticalScroll.Value = panel26.VerticalScroll.Maximum; //scroll the chatbox down 
                }
                else
                    MessageBox.Show("Enter the text first");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        private void bunifuImageButton3_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            conn.Close();
            sck1.Close();
            Environment.Exit(0);
        }

        private void txtmessage_TextChanged(object sender, EventArgs e)
        {
            timer1.Stop();

           AddEmotions();
        }

        private void txtmessage_TextChanged_1(object sender, EventArgs e)
        {
            AddEmotions();
        }



        ////////////////////////////////////////////////////////////////////////////////////////

        public void addInMessage(string message, string time)
        {
            var bbl = new bubble(message, time, msgtype.In);
            bbl.Location = bubble1.Location;
            bbl.Size = bubble1.Size;
            bbl.Anchor = bubble1.Anchor;
            bbl.Top = bbl_old.Bottom + 10;
            this.panel26.Controls.Add(bbl);


            panel26.VerticalScroll.Value = panel26.VerticalScroll.Maximum;

            bbl_old = bbl; //safe the last added object
        }

        public void addOutMessage(string message, string time)
        {
            var bbl = new bubble(message, time, msgtype.Out);
            bbl.Location = bubble1.Location;
            bbl.Left += 20; //add intent
            bbl.Size = bubble1.Size;
            bbl.Anchor = bubble1.Anchor;
            bbl.Top = bbl_old.Bottom + 10;
            this.panel26.Controls.Add(bbl);

            bottom.Top = bbl.Bottom + 50;

            bbl_old = bbl; //safe the last added object
        }

        private void MessageCallBack(IAsyncResult aresult)
        {
            //label41.Visible = true;
            try
            {
                if (sck1.Connected == true)
                {

                    byte[] receivedata = new byte[1500];
                    receivedata = (byte[])aresult.AsyncState;
                    ASCIIEncoding aencoding = new ASCIIEncoding();
                    string receivedmessage = aencoding.GetString(receivedata);

                    for (int i = 0; i < receivedata.Length; i++)
                    {
                        sum += receivedata[i];
                    }
                    Console.WriteLine("meddage" + sum);
                    if (sum != 0)
                    {
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                if ((Name1 == settings.UserName && Name2 == FrooName))
                                {
                                    ChatName.Text = Name2;
                                    ChatStatus.Text = "Online";
                                    addOutMessage(Name2 + ": " + receivedmessage, DateTime.Now.ToShortTimeString());
                                }
                                else
                                {
                                    ChatName.Text = Name1;
                                    ChatStatus.Text = "Online";
                                    addOutMessage(Name1 + ": " + receivedmessage, DateTime.Now.ToShortTimeString());
                                }
                            });
                        }
                        else
                        {
                            if ((Name1 == settings.UserName && Name2 == FrooName))
                            {
                                ChatName.Text = Name2;
                                ChatStatus.Text = "Online";
                                addOutMessage(Name2 + ": " + receivedmessage, DateTime.Now.ToShortTimeString());
                            }
                            else
                            {
                                ChatName.Text = Name1;
                                ChatStatus.Text = "Online";
                                addOutMessage(Name1 + ": " + receivedmessage, DateTime.Now.ToShortTimeString());
                            }

                        }
                    }
                    else
                    {
                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke((MethodInvoker)delegate ()
                            {
                                if ((Name1 == settings.UserName && Name2 == FrooName))
                                {
                                    ChatName.Text = Name2;
                                    ChatStatus.Text = "Offline";
                                    addOutMessage(Name2 + ": " + "I'm Offline", DateTime.Now.ToShortTimeString());

                                }
                                else
                                {
                                    ChatName.Text = Name1;

                                    ChatStatus.Text = "Offline";
                                    addOutMessage(Name1 + ": " + "I'm Offline", DateTime.Now.ToShortTimeString());

                                }
                            });
                        }
                        else
                        {
                            if ((Name1 == settings.UserName && Name2 == FrooName))
                            {
                                ChatName.Text = Name2 + ": " + "Offline";
                                addOutMessage(Name2 + ": " + "I'm Offline", DateTime.Now.ToShortTimeString());

                            }
                            else
                            {
                                ChatName.Text = Name1 + ": " + "Offline";
                                addOutMessage(Name1 + ": " + "I'm Offline", DateTime.Now.ToShortTimeString());

                            }
                        }

                    }
                    Console.WriteLine(Name2 + ":" + receivedmessage);
                    //MessageBox.Show(textBox1.Text + ":" + receivedmessage, "op", MessageBoxButtons.OK);
                    // Listmessage.Items.Add(textBox2.Text + ":" + receivedmessage);
                    buffer = new byte[1500];

                    sck1.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);

                    //if (sck2.Connected == true)
                    //    sck1.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epremote, new AsyncCallback(MessageCallBack), buffer);
                    panel26.VerticalScroll.Value = panel26.VerticalScroll.Maximum; //scroll the chatbox down 
                    for (int i = 0; i < receivedata.Length; i++)
                    {
                        receivedata[i] = 0;
                    }
                    sum = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private string GetLocalIP()
        {
            IPHostEntry host;

            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        void CreateEmotions()
        {
            emotions = new Hashtable(6);
           emotions.Add(":-)", Properties.Resources.regular_smile);
            emotions.Add(":)", Properties.Resources.afro);
            //emotions.Add(":-(", Properties.Resources.smile);
            //emotions.Add(":(", Properties.Resources.sad);
            //emotions.Add(":-P", Properties.Resources.laughing);
            //emotions.Add(":P", Properties.Resources.angry);
            //emotions.Add(":-)", My.Resources.smile);
            //emotions.Add(":(", My.Resources.sad);
            //emotions.Add(":*", My.Resources.kiss);
            //emotions.Add("<3", My.Resources.heart);
            //emotions.Add(":<3", My.Resources.lovely);
            //emotions.Add("(Y)", My.Resources.thumbsup);
            //emotions.Add("(y)", My.Resources.thumbsup);
            //emotions.Add(">.<", My.Resources.angry);
            //emotions.Add("-_-", My.Resources.annoyed);
            //emotions.Add(";-)", My.Resources.eyetwink);
            //emotions.Add(";)", My.Resources.eyetwink);
            //emotions.Add(":P", My.Resources.lool);
            //emotions.Add(":p", My.Resources.lool);
            //emotions.Add(":O", My.Resources.oh);
            //emotions.Add(":o", My.Resources.oh);
            //emotions.Add(":|", My.Resources.suspecious);
            //emotions.Add(":D", My.Resources.teeth);
            //emotions.Add(":'(", My.Resources.weep);
            //emotions.Add(":(con)", My.Resources.confused);
            //emotions.Add(":(lov)", My.Resources.lovely);
        }
        void AddEmotions()
        {
            foreach (string emote in emotions.Keys)
            {
                while (txtmessage.Text.Contains(emote))
                {
                    int ind = txtmessage.Text.IndexOf(emote);
                    txtmessage.Select(ind, emote.Length);
                    Clipboard.SetImage((Image)emotions[emote]);
                    txtmessage.Paste();
                }
            }
        }
       
        ////////////////////////////////////////////////////////////////////////////////////////
    }

}
