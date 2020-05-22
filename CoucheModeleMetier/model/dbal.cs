using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace ApplicationTestsCouches.model
{
    class dbal
    {
        //Inspired by : https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
        MySqlConnection _connection;
        bool _connectionState = false;


        //Constructeur

        public dbal()
        {
            Console.WriteLine("CONNECTION DB -> OK");
            string server = "172.31.254.150", db = "base_escape_game", uid = "Clement", password = "Clem";
            Initialize(server, db, uid, password);
        }


        private void Initialize(string server, string db, string uid, string password)
        {
            string connectionString = "SERVER=" + server + ";" +
                                      "DATABASE=" + db + ";" +
                                      "UID=" + uid + ";" +
                                      "PASSWORD=" + password + ";";

            _connection = new MySqlConnection(connectionString);
        }



        public bool OpenConnection()
        {
            try
            {
                _connection.Open();
                _connectionState = true;
                return true;
            }
            catch /*(MySqlException ex)*/
            {
                _connectionState = false;
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
                _connectionState = false;
                return true;
            }
            catch
            {
                _connectionState = true;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">after 'INSERT INTO'</param>
        /// <param name="table">la table concernée</param>
        public void Insert(string query)
        {
            if (this._connectionState == true)
            {
                MySqlCommand cmd = new MySqlCommand("insert into " + query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Update(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = _connection;

                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Delete(string query)
        {
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


        /// <summary>
        /// SELECT
        /// </summary>
        /// <param name="query">after SELECT * FROM</param>
        /// <returns></returns>
        public DataTable Select(string query)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM " + query, _connection);
            //adapter.SelectCommand = new MySqlCommand("SELECT * FROM " + query, _connection);
            DataSet result = new DataSet();
            adapter.Fill(result);
            DataTable table = result.Tables[0];

            return table;
        }

        public void truncateTable(string table)
        {
            if (this.OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand("truncate table " + table, _connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
