using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private static string[] config = { "vendor", "hostname", "port", "username", "password", "database", "table", "ip", "idcode" };

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

            cmb_vendor.SelectedIndex = cmb_vendor.FindStringExact("MySQL");

            tb_password.PasswordChar = '*';
            cmb_vendor.DropDownStyle = ComboBoxStyle.DropDownList;

            ReadConfig();
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
            tb_ip.Enabled = false;
            tb_idcode.Enabled = false;

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
            tb_ip.Enabled = true;
            tb_idcode.Enabled = true;

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
                CpowerUse cpu = null;
                CpowerUse cpu2 = null;

                while (true)
                {
                    // start thread get data from server
                    List<TemporaryEntity> row = mysql.Select("SELECT * FROM `" + tb_table.Text + "` WHERE ip = '"+ tb_ip.Text +"' AND `status` = '1'");

                    if (flag_thread != false) break;
                    int i = 0;

                    if (row.Count > 0)
                    {
                        if (row[i].running_text.Contains(';'))
                        {
                            string[] words = row[i].running_text.Split(';');
                            if(words.Count() > 1)
                            {
                                // Window 1
                                TextImage tempImg = new TextImage(words[0]+" ", TextImage.defaultFont, Color.Red, Color.Black);
                                byte mode = 1; // 1 is use network
                                int CardID = 1;
                                int WindowNo = 0;
                                if (cpu == null)
                                {
                                    cpu = new CpowerUse(mode, 600, CardID, WindowNo, row[i].ip, 5200, tb_idcode.Text);
                                }
                                cpu.SendImg(tempImg, 0);

                                // remove output image 1
                                try
                                {
                                    File.Delete(tempImg.path);
                                }catch(Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                                // Window 2
                                TextImage tempImg2 = new TextImage(words[1]+" ", TextImage.defaultFont, Color.Red, Color.Black);
                                byte mode2 = 1; // 1 is use network
                                int CardID2 = 1;
                                int WindowNo2 = 1;
                                if(cpu2 == null)
                                {
                                    cpu2 = new CpowerUse(mode2, 600, CardID2, WindowNo2, row[i].ip, 5200, tb_idcode.Text);
                                }
                                cpu2.SendImg(tempImg2, 0);

                                // remove output image 1
                                try
                                {
                                    File.Delete(tempImg2.path);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                        }

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

        private void WriteConfig()
        {
            string FileName = "config.txt";
            try
            {
                File.WriteAllText(FileName, string.Empty); // clear config data
                File.AppendAllText(FileName, config[0]+ ":"+cmb_vendor.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[1] + ":" + tb_hostname.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[2] + ":" + tb_port.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[3] + ":" + tb_username.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[4] + ":" + tb_password.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[5] + ":" + tb_database.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[6] + ":" + tb_table.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[7] + ":" + tb_ip.Text + Environment.NewLine);
                File.AppendAllText(FileName, config[8] + ":" + tb_idcode.Text + Environment.NewLine);
            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ReadConfig()
        {
            string FileName = "config.txt";
            try
            {
                // get file from config
                string StrConfig = File.ReadAllText(FileName, Encoding.UTF8);
                string[] lines = StrConfig.Split(
                    new[] { Environment.NewLine },
                    StringSplitOptions.None
                );
                foreach(string value in lines)
                {
                    string[] keyval = value.Split(':');
                    if(keyval.Length > 1)
                    {
                        // vendor
                        if (config[0] == keyval[0])
                        {
                            cmb_vendor.SelectedIndex = cmb_vendor.FindStringExact(keyval[1]);
                        }

                        // hostname
                        if (config[1] == keyval[0])
                        {
                            tb_hostname.Text = keyval[1];
                        }

                        // port
                        if (config[2] == keyval[0])
                        {
                            tb_port.Text = keyval[1];
                        }

                        // username
                        if (config[3] == keyval[0])
                        {
                            tb_username.Text = keyval[1];
                        }

                        // password
                        if (config[4] == keyval[0])
                        {
                            tb_password.Text = keyval[1];
                        }

                        // database
                        if (config[5] == keyval[0])
                        {
                            tb_database.Text = keyval[1];
                        }

                        // table
                        if (config[6] == keyval[0])
                        {
                            tb_table.Text = keyval[1];
                        }

                        // ip
                        if (config[7] == keyval[0])
                        {
                            tb_ip.Text = keyval[1];
                        }

                        // idcode
                        if (config[8] == keyval[0])
                        {
                            tb_idcode.Text = keyval[1];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            WriteConfig();

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
                            // do something
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
