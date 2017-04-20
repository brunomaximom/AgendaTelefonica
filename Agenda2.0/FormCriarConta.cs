using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agenda2._0
{
    public partial class FormCriarConta : Form
    {
        public FormCriarConta()
        {
            InitializeComponent();
            lblMensagem.Text = "";
        }

        private void btnCriarConta_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "" || txtSenha.Text == "" || txtConfirmarSenha.Text == "")
            {
                txtLogin.Text = "";
                txtSenha.Text = "";
                txtConfirmarSenha.Text = "";
                txtLogin.Focus();
                lblMensagem.Text = "Todos os campos são obrigatórios";
                return;
            }
            else if (txtLogin.Text.Length < 4)
            {
                lblMensagem.Text = "O usuário precisa ter pelo menos 4 caracteres";
                txtLogin.Focus();
                return;
            }
            else if (txtSenha.Text.Length < 4)
            {
                lblMensagem.Text = "A senha precisa ter pelo menos 4 caracteres";
                txtSenha.Focus();
                return;
            }
            else if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                lblMensagem.Text = "Senhas não conferem";
                txtSenha.Text = "";
                txtConfirmarSenha.Text = "";
                txtSenha.Focus();
                return;
            }
            else
            {
                UserDAO Usuario = new UserDAO();
                if (Usuario.AddUser(txtLogin.Text, txtSenha.Text))
                {
                    lblMensagem.Text = "";                    
                    MessageBox.Show("Conta criada com sucesso!");
                    this.Close();
                }
                else
                {
                    lblMensagem.Text = "Usuário já existe!";
                }
            }            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
