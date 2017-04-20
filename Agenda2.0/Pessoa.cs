using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda2._0
{
    class Pessoa
    {
        private int idUsuario;
        private int idPessoa;
        private string nomePessoa;
        private string logradouro;
        private int numero;
        private string bairro;
        private string cidade;
        private string estado;
        private string anotacoes;

        public int IdUsuario
        {
            get
            {
                return idUsuario;
            }

            set
            {
                idUsuario = value;
            }
        }

        public int IdPessoa
        {
            get
            {
                return idPessoa;
            }

            set
            {
                idPessoa = value;
            }
        }

        public string NomePessoa
        {
            get
            {
                return nomePessoa;
            }

            set
            {
                nomePessoa = value;
            }
        }

        public string Logradouro
        {
            get
            {
                return logradouro;
            }

            set
            {
                logradouro = value;
            }
        }

        public int Numero
        {
            get
            {
                return numero;
            }

            set
            {
                numero = value;
            }
        }

        public string Bairro
        {
            get
            {
                return bairro;
            }

            set
            {
                bairro = value;
            }
        }

        public string Cidade
        {
            get
            {
                return cidade;
            }

            set
            {
                cidade = value;
            }
        }

        public string Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }

        public string Anotacoes
        {
            get
            {
                return anotacoes;
            }

            set
            {
                anotacoes = value;
            }
        }
    }
}
