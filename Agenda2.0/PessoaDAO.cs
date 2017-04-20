using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Agenda2._0
{
    class PessoaDAO
    {
        public void AddPessoa(int iduser, string nome, string logradouro, int numero, string bairro, string cidade, string estado, string anotacoes)
        {
            string qry = string.Format("insert into pessoa (idUsuario, nomePessoa, logradouro, numero, bairro, cidade, estado, anotacoes)"+
                                        "values ({0},'{1}','{2}',{3},'{4}','{5}','{6}','{7}');", 
                                        iduser, nome, logradouro, numero, bairro, cidade, estado, anotacoes);

            Banco.GetInstance().ExecutarSQL(qry);
        }

        public void RemoverPessoa(int idPessoa)
        {
            string query = string.Format("delete from pessoa where idPessoa = {0};", idPessoa);

            Banco.GetInstance().ExecutarSQL(query);
        }

        public void AtualizarPessoa(int idPessoa, string nome, string logradouro, int numero, string bairro, string cidade, string estado, string anotacoes)
        {
            string qry = string.Format("update pessoa set (nomePessoa, logradouro, numero, bairro, cidade, estado, anotacoes)" +
                            "values ({0},'{1}','{2}',{3},'{4}','{5}','{6}') " +
                            "where idPessoa = {7};", nome, logradouro, numero, bairro, cidade, estado, anotacoes, idPessoa);

        }

        public List<Pessoa> ListByName(string nome, int idGrupo, int idUser)
        {
            MySqlConnection conn = Banco.GetInstance().Conectar();
            string query;

            if(idGrupo == 0)
                query = string.Format("select idPessoa, nomePessoa, logradouro, numero, bairro, cidade, estado, anotacoes from pessoa where nomepessoa like '%{0}%' and idUsuario = {1} order by nomePessoa;", nome, idUser);
            else
                query = string.Format("select idPessoa, nomePessoa, logradouro, numero, bairro, cidade, estado, anotacoes from pessoa where nomepessoa like '%{0}%' and idPessoa in (select idPessoa from PessoaGrupo where idGrupo in (select idgrupo from grupo)) and idUsuario = {2} order by nomePessoa;", nome, idGrupo, idUser);

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            MySqlCommand comm = new MySqlCommand(query, conn);

            MySqlDataReader dr = comm.ExecuteReader();

            Pessoa pessoa;
            List<Pessoa> pessoas = new List<Pessoa>();
            
            while(dr.Read())
            {
                pessoa = new Pessoa();
                pessoa.IdPessoa = dr.GetInt32("IdPessoa");
                pessoa.NomePessoa = dr.GetString("nomePessoa");
                pessoa.Logradouro = dr.GetString("logradouro");
                pessoa.Numero = dr.GetInt32("numero");
                pessoa.Bairro = dr.GetString("bairro");
                pessoa.Cidade = dr.GetString("cidade");
                pessoa.Estado = dr.GetString("estado");
                if (dr.IsDBNull(7))
                    pessoa.Anotacoes = "";
                else
                    pessoa.Anotacoes = dr.GetString("anotacoes");
                pessoas.Add(pessoa);
            }

            conn.Close();

            return pessoas;
        }

        public List<Pessoa> ListAll(int idGrupo, int idUser)
        {
            MySqlConnection conn = Banco.GetInstance().Conectar();
            
            string query;

            if (idGrupo == 0)
                query = string.Format("select idPessoa, nomePessoa, logradouro, numero, bairro, cidade, estado, anotacoes from pessoa where idUsuario = {0} order by nomePessoa;", idUser);
            else
                query = string.Format("select idPessoa, nomePessoa, logradouro, numero, bairro, cidade, estado, anotacoes from pessoa where idPessoa in (select idPessoa from PessoaGrupo where idGrupo = {0}) and idUsuario = {1} order by nomePessoa;", idGrupo, idUser);

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            MySqlCommand comm = new MySqlCommand(query, conn);
            
            MySqlDataReader dr = comm.ExecuteReader();

            //Pessoa pessoa;
            List<Pessoa> pessoas = new List<Pessoa>();

            while (dr.Read())
            {
                Pessoa pessoa = new Pessoa();
                pessoa.IdPessoa = dr.GetInt32("IdPessoa");
                pessoa.NomePessoa = dr.GetString("nomePessoa");
                pessoa.Logradouro = dr.GetString("logradouro");
                pessoa.Numero = dr.GetInt32("numero");
                pessoa.Bairro = dr.GetString("bairro");
                pessoa.Cidade = dr.GetString("cidade");
                pessoa.Estado = dr.GetString("estado");
                if (dr.IsDBNull(7))
                    pessoa.Anotacoes = "";
                else
                    pessoa.Anotacoes = dr.GetString("anotacoes");

                pessoas.Add(pessoa);
            }

            conn.Close();

            return pessoas;
        }

        public int GetNextId()
        {
            MySqlConnection conn = Banco.GetInstance().Conectar();

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            string query = "select (max(idpessoa)+1) from pessoa";

            MySqlCommand comm = new MySqlCommand(query, conn);

            MySqlDataReader dr = comm.ExecuteReader();

            int nextId;

            dr.Read();
            nextId = dr.GetInt32(0);

            conn.Close();

            return nextId;
        }
    }
}
