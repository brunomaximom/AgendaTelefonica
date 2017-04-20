using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda2._0
{
    class Telefone
    {
        private string nroTelefone;
        private int idpessoa;

        public string NroTelefone
        {
            get
            {
                return nroTelefone;
            }

            set
            {
                nroTelefone = value;
            }
        }

        public int Idpessoa
        {
            get
            {
                return idpessoa;
            }

            set
            {
                idpessoa = value;
            }
        }
    }
}
