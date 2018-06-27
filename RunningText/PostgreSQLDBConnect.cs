using Npgsql;
using System;
using System.Windows.Forms;

namespace RunningText
{
    class PostgreSQLDBConnect
    {
        private NpgsqlConnection connection;
        private string server;
        private int port;
        private string database;
        private string username;
        private string password;


        //Constructor
        public PostgreSQLDBConnect(string server, int port, string database, string username, string password)
        {
            this.server = server;
            this.port = port;
            this.database = database;
            this.username = username;
            this.password = password;

            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;
            connectionString = "Server=" + this.server + ";Port=" + this.port + ";User Id=" + this.username + ";Password=" + this.password + ";Database=" + this.database;

            connection = new NpgsqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to server. "+ex.Message);
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Cannot connect to server. " + ex.Message);
                return false;
            }
        }
    }
}
