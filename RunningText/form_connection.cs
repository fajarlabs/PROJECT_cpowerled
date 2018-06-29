using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunningText
{
    public partial class form_connection : Form
    {
        private ThreadStart childref;
        private Thread thread;
        private static bool flag_thread = false;

        //cpu.sendTextToCom("COM3", 1, 115200, "12345678");
        //cpu.sendTextToNetwork("10.1.12.105", "255.255.255.255", "006 007");

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

        private void disabled_all_element()
        {
            tb_database.Enabled = false;
            tb_hostname.Enabled = false;
            tb_username.Enabled = false;
            tb_password.Enabled = false;
            tb_port.Enabled = false;
            tb_table.Enabled = false;
            btn_clear.Enabled = false;
            cmb_vendor.Enabled = false;

            btn_connect.Text = "Disconnect";
            btn_connect.BackColor = Color.Red;
        }

        private void enabled_all_element()
        {
            tb_database.Enabled = true;
            tb_hostname.Enabled = true;
            tb_username.Enabled = true;
            tb_password.Enabled = true;
            tb_port.Enabled = true;
            tb_table.Enabled = true;
            btn_clear.Enabled = true;
            cmb_vendor.Enabled = true;

            btn_connect.Text = "Connect";
            btn_connect.BackColor = Color.Green;
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

        private void CallToChildThread()
        {
            try
            {
                MysqlDBConnect mysql = new MysqlDBConnect(tb_hostname.Text, Int32.Parse(tb_port.Text), tb_database.Text, tb_username.Text, tb_password.Text);

                while (true)
                {
                    // start thread get data from server
                    List<TemporaryEntity> row = mysql.Select("SELECT * FROM `" + tb_table.Text + "` WHERE ip = '10.1.12.105' AND `status` = '1'");

                    Console.WriteLine("run");

                    if (flag_thread != false) break;
                    int i = 0;

                    if (row.Count > 0)
                    {
                        CpowerUse cpu = new CpowerUse(1, 600, 1, row[i].ip, 5200, "255.255.255.255");
                        cpu.SendTextToNetwork(row[i].running_text);

                        mysql.Update("UPDATE `" + tb_table.Text + "` SET `status` = '0' WHERE `id` = '" + row[i].id + "'");
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                enabled_all_element();
            }
            finally
            {
                Console.WriteLine("Couldn't catch the Thread Exception");
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            // cardid 1
            TextImage tempImg = new TextImage("001", TextImage.defaultFont, Color.Red, Color.Black);
            CpowerUse cpu = new CpowerUse(1, 600, 1, "10.1.12.105", 5200, "255.255.255.255");
            cpu.SendImg(tempImg, 0);

            // card id 2
            /*
            TextImage tempImg2 = new TextImage("002", TextImage.defaultFont, Color.Red, Color.Black);
            CpowerUse cpu2 = new CpowerUse(1, 600, 1, "10.1.12.105", 5200, "255.255.255.255");
            cpu2.SendImg(tempImg2, 0);
            */
            return;

            if (btn_connect.Text.Equals("Connect"))
            {
                if (flag_thread == true) flag_thread = false;

                this.disabled_all_element();

                // connect to database
                if (cmb_vendor.SelectedItem != null)
                {
                    ComboboxItem select_item = (ComboboxItem)cmb_vendor.SelectedItem;

                    // connect to mysql
                    if (select_item.Value == 1)
                    {
                        childref = new ThreadStart(CallToChildThread);
                        thread = new Thread(childref);
                        thread.Start();
                    }

                    // connect to postgresql
                    if (select_item.Value == 2)
                    {
                        PostgreSQLDBConnect postgresql = new PostgreSQLDBConnect(tb_hostname.Text, Int32.Parse(tb_port.Text), tb_database.Text, tb_username.Text, tb_password.Text);
                        if (postgresql.OpenConnection() == true)
                        {
                            MessageBox.Show("Connected postgresql!");
                        }
                    }
                }
            } else
            {
                flag_thread = true;
                this.enabled_all_element();
            }
        }
    }
}
