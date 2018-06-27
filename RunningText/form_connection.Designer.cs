namespace RunningText
{
    partial class form_connection
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_vendor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_hostname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.tb_table = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_database = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_connect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vendor Database";
            // 
            // cmb_vendor
            // 
            this.cmb_vendor.FormattingEnabled = true;
            this.cmb_vendor.Location = new System.Drawing.Point(16, 30);
            this.cmb_vendor.Name = "cmb_vendor";
            this.cmb_vendor.Size = new System.Drawing.Size(263, 21);
            this.cmb_vendor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hostname";
            // 
            // tb_hostname
            // 
            this.tb_hostname.Location = new System.Drawing.Point(16, 72);
            this.tb_hostname.Name = "tb_hostname";
            this.tb_hostname.Size = new System.Drawing.Size(168, 20);
            this.tb_hostname.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Port";
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(190, 72);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(89, 20);
            this.tb_port.TabIndex = 5;
            this.tb_port.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_port_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Username";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(16, 112);
            this.tb_username.Name = "tb_username";
            this.tb_username.Size = new System.Drawing.Size(125, 20);
            this.tb_username.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Password";
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(147, 112);
            this.tb_password.Name = "tb_password";
            this.tb_password.Size = new System.Drawing.Size(132, 20);
            this.tb_password.TabIndex = 9;
            // 
            // tb_table
            // 
            this.tb_table.Location = new System.Drawing.Point(147, 151);
            this.tb_table.Name = "tb_table";
            this.tb_table.Size = new System.Drawing.Size(132, 20);
            this.tb_table.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Table";
            // 
            // tb_database
            // 
            this.tb_database.Location = new System.Drawing.Point(16, 151);
            this.tb_database.Name = "tb_database";
            this.tb_database.Size = new System.Drawing.Size(125, 20);
            this.tb_database.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Database";
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Maroon;
            this.btn_clear.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_clear.Location = new System.Drawing.Point(16, 193);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(125, 26);
            this.btn_clear.TabIndex = 14;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_connect
            // 
            this.btn_connect.BackColor = System.Drawing.Color.Green;
            this.btn_connect.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_connect.Location = new System.Drawing.Point(147, 193);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(132, 26);
            this.btn_connect.TabIndex = 15;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = false;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // form_connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 235);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.tb_table);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_database);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_username);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_hostname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_vendor);
            this.Controls.Add(this.label1);
            this.Name = "form_connection";
            this.Text = "Running Text Apps";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_vendor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_hostname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.TextBox tb_table;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_database;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_connect;
    }
}

