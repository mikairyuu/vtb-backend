using System;
using MySql.Data.MySqlClient;

namespace vtb_backend.Models
{
    public class DBManager
    {
        private MySqlConnection _mySqlConnection;
        private MySqlDataAdapter _mySqlDataAdapter;
        private MySqlDataReader _cachedReader;

        public DBManager()
        {
            _mySqlConnection = new MySqlConnection(Environment.GetEnvironmentVariable("mainDB_credentials"));
            _mySqlDataAdapter = new MySqlDataAdapter();
            _mySqlConnection.Open() ;
        }

        public MySqlDataReader GetReader(MySqlCommand cmd)
        {
            cmd.Connection = _mySqlConnection;
            _cachedReader = cmd.ExecuteReader();
            return _cachedReader;
        }

        public void InsertCommand(MySqlCommand cmd)
        {
            cmd.Connection = _mySqlConnection;
            _mySqlDataAdapter.InsertCommand = cmd;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        public void Close()
        {
            _mySqlConnection.Close();
            _cachedReader?.Close();
        }
    }
}