namespace KeBlowFusePatchGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmMotherboard = new System.Windows.Forms.ComboBox();
            this.cmImageType = new System.Windows.Forms.ComboBox();
            this.cmConsoleType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCB = new System.Windows.Forms.TextBox();
            this.txtCPUKey = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtUpdateSeq = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(13, 263);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(82, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Motherboard:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(92, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Image Type:";
            // 
            // cmMotherboard
            // 
            this.cmMotherboard.FormattingEnabled = true;
            this.cmMotherboard.Items.AddRange(new object[] {
            "Xenon",
            "Zephyr",
            "Falcon",
            "Jasper",
            "Trinity",
            "Corona",
            "CoronaWB",
            "Corona4GB"});
            this.cmMotherboard.Location = new System.Drawing.Point(179, 16);
            this.cmMotherboard.Name = "cmMotherboard";
            this.cmMotherboard.Size = new System.Drawing.Size(121, 23);
            this.cmMotherboard.TabIndex = 5;
            // 
            // cmImageType
            // 
            this.cmImageType.FormattingEnabled = true;
            this.cmImageType.Items.AddRange(new object[] {
            "JTAG",
            "Glitch",
            "Glitch2",
            "Glitch2m",
            "DevGL"});
            this.cmImageType.Location = new System.Drawing.Point(179, 50);
            this.cmImageType.Name = "cmImageType";
            this.cmImageType.Size = new System.Drawing.Size(121, 23);
            this.cmImageType.TabIndex = 6;
            // 
            // cmConsoleType
            // 
            this.cmConsoleType.FormattingEnabled = true;
            this.cmConsoleType.Items.AddRange(new object[] {
            "Phat Retail",
            "Slim Retail",
            "Development Kit"});
            this.cmConsoleType.Location = new System.Drawing.Point(179, 120);
            this.cmConsoleType.Name = "cmConsoleType";
            this.cmConsoleType.Size = new System.Drawing.Size(121, 23);
            this.cmConsoleType.TabIndex = 8;
            this.cmConsoleType.SelectedIndexChanged += new System.EventHandler(this.cmConsoleType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(13, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fuse Line 1 (Console Type):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(44, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Fuse Line 2 (CB LDV):";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(179, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 23);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "C0FFFFFFFFFFFFFF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(97, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Fuse Line 0:";
            // 
            // txtCB
            // 
            this.txtCB.Location = new System.Drawing.Point(179, 156);
            this.txtCB.Name = "txtCB";
            this.txtCB.Size = new System.Drawing.Size(121, 23);
            this.txtCB.TabIndex = 13;
            this.txtCB.Text = "0";
            // 
            // txtCPUKey
            // 
            this.txtCPUKey.Location = new System.Drawing.Point(179, 191);
            this.txtCPUKey.Name = "txtCPUKey";
            this.txtCPUKey.Size = new System.Drawing.Size(256, 23);
            this.txtCPUKey.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(107, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 15;
            this.label6.Text = "CPU Key:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label7.Location = new System.Drawing.Point(71, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "Update Sequence:";
            // 
            // txtUpdateSeq
            // 
            this.txtUpdateSeq.Location = new System.Drawing.Point(179, 225);
            this.txtUpdateSeq.Name = "txtUpdateSeq";
            this.txtUpdateSeq.Size = new System.Drawing.Size(121, 23);
            this.txtUpdateSeq.TabIndex = 16;
            this.txtUpdateSeq.Text = "0";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(111, 263);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 18;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.chkAuto.Location = new System.Drawing.Point(192, 266);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(88, 19);
            this.chkAuto.TabIndex = 19;
            this.chkAuto.Text = "Auto-Apply";
            this.chkAuto.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::KeBlowFusePatchGenerator.Resource1.Logo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(450, 301);
            this.Controls.Add(this.chkAuto);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtUpdateSeq);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCPUKey);
            this.Controls.Add(this.txtCB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmConsoleType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmImageType);
            this.Controls.Add(this.cmMotherboard);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmMotherboard;
        private System.Windows.Forms.ComboBox cmImageType;
        private System.Windows.Forms.ComboBox cmConsoleType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCB;
        private System.Windows.Forms.TextBox txtCPUKey;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtUpdateSeq;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckBox chkAuto;
    }
}
