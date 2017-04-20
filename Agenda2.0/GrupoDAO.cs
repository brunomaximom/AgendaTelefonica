using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Agenda2._0
{
    class GrupoDAO
    {
        public List<Grupo> ListAll(int idUsuario)
        {
            MySqlConnection conn = Banco.GetInstance().Conectar();

            string query = string.Format("select idgrupo, nomegrupo from grupo where idUsuario = {0};", idUsuario);

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            MySqlCommand comm = new MySqlCommand(query, conn);

            MySqlDataReader dr = comm.ExecuteReader();

            List<Grupo> grupos = new List<Grupo>();
            Grupo grupo;

            while(dr.Read())
            {
                grupo = new Grupo();
                grupo.IdGrupo = dr.GetInt16("idGrupo");
                grupo.NomeGrupo = dr.GetString("nomeGrupo");
                grupos.Add(grupo);
            }

            conn.Close();

            return grupos;
        }


    }
}
