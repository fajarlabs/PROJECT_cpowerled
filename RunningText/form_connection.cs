using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunningText
{
    public partial class form_connection : Form
    {
        public form_connection()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComboboxItem item1 = new ComboboxItem();
            item1.Text = "MySQL";
            item1.Value = 1;
            ComboboxItem item2 = new ComboboxItem();
            item2.Text = "PostgreSQL";
            item2.Value = 2;
            cmb_vendor.Items.Add(item1);
            cmb_vendor.Items.Add(item2);

            tb_password.PasswordChar = '*';
            cmb_vendor.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_database.Text = "";
            tb_hostname.Text = "";
            tb_username.Text = "";
            tb_password.Text = "";
            tb_port.Text = "";
            tb_table.Text = "";
        }

        private void tb_port_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            CpowerUse cpu = new CpowerUse();

            //cpu.sendTextToCom("COM3", 1, 115200, "12345678");
            //cpu.sendTextToNetwork("10.1.12.105", "255.255.255.255", "87654321");

            // connect to database
            if (cmb_vendor.SelectedItem != null)
            {
                ComboboxItem select_item = (ComboboxItem)cmb_vendor.SelectedItem;

                // connect to mysql
                if (select_item.Value == 1)
                {
                    MysqlDBConnect mysql = new MysqlDBConnect(tb_hostname.Text,Int32.Parse(tb_port.Text),tb_database.Text,tb_username.Text,tb_password.Text);
                    if (mysql.OpenConnection() == true)
                    {
                        MessageBox.Show("Connected mysql!");
                    }
                }

                // connect to postgresql
                if (select_item.Value == 2)
                {
                    PostgreSQLDBConnect postgresql = new PostgreSQLDBConnect(tb_hostname.Text, Int32.Parse(tb_port.Text), tb_database.Text, tb_username.Text, tb_password.Text);
                    if(postgresql.OpenConnection() == true)
                    {
                        MessageBox.Show("Connected postgresql!");
                    }
                }
            }
        }
    }
}
