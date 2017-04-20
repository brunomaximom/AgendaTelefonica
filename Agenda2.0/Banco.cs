using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Agenda2._0
{
    class Banco
    {
        private static MySqlConnection conexao;
        private static Banco instance;
        private const string URL = "Server=127.0.0.1;Database=agenda;Uid=root;Pwd=0000;";

        public Banco()
        {
            conexao = new MySqlConnection(URL);
        }

        public static Banco GetInstance()
        {
            if (instance == null)
                instance = new Banco();

            return instance;
        }
        public MySqlConnection Conectar()
        {
            return conexao;
        }

        public void ExecutarSQL(string qry)
        {
            if (conexao.State != System.Data.ConnectionState.Open)
                conexao.Open();

            MySqlCommand comm = new MySqlCommand(qry, conexao);
            comm.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
