using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{

    public partial class Form2 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        SqlCommand cmd;
        Settings settings = new Settings();
        SqlConnection conn = new SqlConnection(
                "Data Source=DESKTOP-AJ8DB4G;" +
                "Initial Catalog=Users;" +
                "Integrated Security=SSPI;");
        public Form2()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuMaterialTextbox2_OnValueChanged(object sender, EventArgs e)
        {
            password_txt.isPassword = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public int _id
        {
            get { return settings.Id; }
        }
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Visible = false;
            new Registerr().ShowDialog();
            this.Close();

        }

        private void sigin_btn_Click(object sender, EventArgs e)
        {
            if (!(usrname_txt.Text == "" || password_txt.Text == ""))
            {
                try
                {
                    string query = "SELECT * FROM UserData WHERE Username = '" + usrname_txt.Text + "'and Password = '" + password_txt.Text + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                    DataTable dtbl = new DataTable();
                    sda.Fill(dtbl);

                    if (dtbl.Rows.Count == 1)
                    {
                        conn.Open();
                        cmd = new SqlCommand("Select Usr_id from [UserData] where  Username = '" + usrname_txt.Text + "'and Password = '" + password_txt.Text + "'", conn);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                settings.Id = reader.GetInt32(0);
                                Console.WriteLine(String.Format("{0}", settings.Id));
                            }
                        }

                        this.Visible = false;
                        Main f = new Main();
                        f.Idd = _id;
                        Profile m = new Profile();
                        m._idd = _id;
                          f.ShowDialog();
                       // m.ShowDialog();
                        this.Close();
                    }
                    else
                        MessageBox.Show("This User is Exists....!", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else
                MessageBox.Show("Enter The Data First !", "Erorr!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
