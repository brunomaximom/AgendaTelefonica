using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Agenda2._0
{
    public partial class FormLogin : Form
    {

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public FormLogin()
        {
            InitializeComponent();
            lblMensagem.Text = "";
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            UserDAO Usuario = new UserDAO();
            int resposta = Usuario.Logar(txtLogin.Text, txtSenha.Text);

            if (resposta == -1)
                lblMensagem.Text = "Senha incorreta";
            if (resposta == 0)
                lblMensagem.Text = "Usuário não encontrado";
            if (resposta > 0)
            {
                lblMensagem.Text = "";
                id = resposta;
                this.Close();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormCriarConta criarConta = new FormCriarConta();
            criarConta.ShowDialog();
            this.Show();
        }
    }
}
