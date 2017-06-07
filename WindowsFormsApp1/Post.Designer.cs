namespace WindowsFormsApp1
{
    partial class Post
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPostTeXT = new System.Windows.Forms.Label();
            this.LblName = new System.Windows.Forms.Label();
            this.LikeBtn = new Bunifu.Framework.UI.BunifuFlatButton();
            this.PostPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PostPic)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPostTeXT
            // 
            this.lblPostTeXT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPostTeXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostTeXT.ForeColor = System.Drawing.Color.Black;
            this.lblPostTeXT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblPostTeXT.Location = new System.Drawing.Point(65, 22);
            this.lblPostTeXT.Name = "lblPostTeXT";
            this.lblPostTeXT.Size = new System.Drawing.Size(350, 69);
            this.lblPostTeXT.TabIndex = 1;
            this.lblPostTeXT.Text = "this is just a place holder";
            this.lblPostTeXT.UseCompatibleTextRendering = true;
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblName.Location = new System.Drawing.Point(62, 4);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(121, 18);
            this.LblName.TabIndex = 2;
            this.LblName.Text = "Ahmed Moorsy";
            // 
            // LikeBtn
            // 
            this.LikeBtn.Activecolor = System.Drawing.Color.Transparent;
            this.LikeBtn.BackColor = System.Drawing.Color.Transparent;
            this.LikeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LikeBtn.BorderRadius = 0;
            this.LikeBtn.ButtonText = "Like";
            this.LikeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LikeBtn.DisabledColor = System.Drawing.Color.Gray;
            this.LikeBtn.Iconcolor = System.Drawing.Color.Transparent;
            this.LikeBtn.Iconimage = global::WindowsFormsApp1.Properties.Resources.thumbs_up_hand_symbol__1_1;
            this.LikeBtn.Iconimage_right = null;
            this.LikeBtn.Iconimage_right_Selected = null;
            this.LikeBtn.Iconimage_Selected = null;
            this.LikeBtn.IconMarginLeft = 0;
            this.LikeBtn.IconMarginRight = 0;
            this.LikeBtn.IconRightVisible = true;
            this.LikeBtn.IconRightZoom = 0D;
            this.LikeBtn.IconVisible = true;
            this.LikeBtn.IconZoom = 50D;
            this.LikeBtn.IsTab = false;
            this.LikeBtn.Location = new System.Drawing.Point(349, 97);
            this.LikeBtn.Name = "LikeBtn";
            this.LikeBtn.Normalcolor = System.Drawing.Color.Transparent;
            this.LikeBtn.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.LikeBtn.OnHoverTextColor = System.Drawing.Color.Black;
            this.LikeBtn.selected = false;
            this.LikeBtn.Size = new System.Drawing.Size(66, 28);
            this.LikeBtn.TabIndex = 3;
            this.LikeBtn.Text = "Like";
            this.LikeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LikeBtn.Textcolor = System.Drawing.Color.Black;
            this.LikeBtn.TextFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // PostPic
            // 
            this.PostPic.Image = global::WindowsFormsApp1.Properties.Resources.Adele_2017_Wallpaper_10969;
            this.PostPic.Location = new System.Drawing.Point(4, 4);
            this.PostPic.Name = "PostPic";
            this.PostPic.Size = new System.Drawing.Size(40, 40);
            this.PostPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PostPic.TabIndex = 0;
            this.PostPic.TabStop = false;
            // 
            // Post
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LikeBtn);
            this.Controls.Add(this.LblName);
            this.Controls.Add(this.lblPostTeXT);
            this.Controls.Add(this.PostPic);
            this.Name = "Post";
            this.Size = new System.Drawing.Size(418, 128);
            this.Resize += new System.EventHandler(this.Post_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PostPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PostPic;
        private System.Windows.Forms.Label lblPostTeXT;
        private System.Windows.Forms.Label LblName;
        private Bunifu.Framework.UI.BunifuFlatButton LikeBtn;
    }
}
