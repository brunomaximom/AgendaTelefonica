using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Agenda2._0
{
    class TelefoneDAO
    {
        public List<Telefone> ListAll(int idPessoa)
        {
            MySqlConnection conn = Banco.GetInstance().Conectar();
            
            string query = string.Format("select nroTelefone, idPessoa from telefone where idPessoa = {0};", idPessoa);
            
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            MySqlCommand comm = new MySqlCommand(query, conn);

            MySqlDataReader dr = comm.ExecuteReader();

            Telefone telefone;

            List<Telefone> telefones = new List<Telefone>();

            while(dr.Read())
            {
                telefone = new Telefone();
                telefone.NroTelefone = dr.GetString("nroTelefone");
                telefone.Idpessoa = dr.GetInt32("idPessoa");
                telefones.Add(telefone);
            }

            conn.Close();

            return telefones;
        }

        public void Delete(List<Telefone> telefones, int idPessoa)
        {
            MySqlConnection conn = Banco.GetInstance().Conectar();
            
            foreach(Telefone telefone in telefones)
            {
                string query = string.Format("delete from telefone where nroTelefone = {0} and idPessoa = {1};", telefone, idPessoa);

                Banco.GetInstance().ExecutarSQL(query);
            }
        }
    }
}
