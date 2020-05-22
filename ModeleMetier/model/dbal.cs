using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace ModeleMetier.model
{
    public class dbal
    {
        //Inspired by : https://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
        MySqlConnection _connection;
        bool _connectionState = false;


        //Constructeur

        public dbal()
        {
            //Console.WriteLine("CONNECTION DB -> OK");
            string server = "localhost", db = "base_escape_game", uid = "root", password = "";
            //string server = "172.31.136.20", db = "base_escape_game", uid = "Clement", password = "ClementDussollier";
            
            server = ConfigurationManager.AppSettings["uriDB"];
            db = ConfigurationManager.AppSettings["nameDB"];
            uid = ConfigurationManager.AppSettings["userDB"];
            password = ConfigurationManager.AppSettings["passDB"];
            
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">after 'UPDATE'</param>
        public void Update(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "update " + query;
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
