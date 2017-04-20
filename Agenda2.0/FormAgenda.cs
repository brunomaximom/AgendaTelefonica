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
    public partial class FormAgenda : Form
    {
        private Banco banco = new Banco();

        private int idUser;

        public int Id
        {
            get { return idUser; }
            set { idUser = value; }
        }

        public FormAgenda()
        {
            InitializeComponent();
        }

        private void FormAgenda_Load(object sender, EventArgs e)
        {
            //criando as colunas da tabela de grupos
            dgvGrupos.Columns.Add("idGrupo", "idGrupo");
            dgvGrupos.Columns[0].Visible = false;
            dgvGrupos.Columns.Add("nomeGrupo", "nomeGrupo");//cria a coluna nomeGrupo na dgv
            dgvGrupos.Rows.Add(0, "Todos");//adiciona a linha Todos para exibir todos os contatos ao ser clicada            

            //criando as colunas da tabela de contatos
            dgvContatos.Columns.Add("idPessoa", "idPessoa");
            dgvContatos.Columns[0].Visible = false;
            dgvContatos.Columns.Add("nomePessoa", "nomePessoa");

            GrupoDAO grupo = new GrupoDAO();
            List<Grupo> grupos = new List<Grupo>();
            grupos = grupo.ListAll(idUser);

            foreach (Grupo g in grupos)
            {
                dgvGrupos.Rows.Add(g.IdGrupo, g.NomeGrupo);
            }

            dgvGrupos_CellClick(null, null);
        }

        private void SetCoresPreto()
        {
            txtNome.ForeColor = System.Drawing.Color.Black;
            txtLogradouro.ForeColor = System.Drawing.Color.Black;
            txtNumero.ForeColor = System.Drawing.Color.Black;
            txtCidade.ForeColor = System.Drawing.Color.Black;
            txtEstado.ForeColor = System.Drawing.Color.Black;
            txtBairro.ForeColor = System.Drawing.Color.Black;
        }

        //essa função altera as cores para cinza e adiciona os textos de exemplo
        private void SetCoresCinza()
        {
            txtNome.ForeColor = System.Drawing.Color.LightSteelBlue;
            txtLogradouro.ForeColor = System.Drawing.Color.LightSteelBlue;
            txtNumero.ForeColor = System.Drawing.Color.LightSteelBlue;
            txtCidade.ForeColor = System.Drawing.Color.LightSteelBlue;
            txtEstado.ForeColor = System.Drawing.Color.LightSteelBlue;
            txtBairro.ForeColor = System.Drawing.Color.LightSteelBlue;

            txtNome.Text = "Ex: João Batista";
            txtLogradouro.Text = "Ex: Av. São Carlos";
            txtNumero.Mask = "";
            txtNumero.Text = "Ex: 2121";
            txtCidade.Text = "Ex: São Carlos";
            txtEstado.Mask = "";
            txtEstado.Text = "Ex: SP";
            txtBairro.Text = "Ex: Centro";
            txtTelefone.Text = "";
            txtAnotacoes.Text = "";
            cbbTelefones.Items.Clear();
        }

        private void txtNome_Enter(object sender, EventArgs e)
        {
            if(txtNome.ForeColor!=System.Drawing.Color.Black)
            {
                txtNome.ForeColor = System.Drawing.Color.Black;
                txtNome.SelectionStart = 0;
                txtNome.SelectionLength = txtNome.Text.Length;
            }

        }

        private void txtRua_Enter(object sender, EventArgs e)
        {
            if (txtLogradouro.ForeColor != System.Drawing.Color.Black)
            {
                txtLogradouro.ForeColor = System.Drawing.Color.Black;
                txtLogradouro.SelectionStart = 0;
                txtLogradouro.SelectionLength = txtLogradouro.Text.Length;
            }
        }

        private void txtNumero_Enter(object sender, EventArgs e)
        {
            if (txtNumero.ForeColor != System.Drawing.Color.Black)
            {
                txtNumero.ForeColor = System.Drawing.Color.Black;
                //txtNumero.Text = "";
                //txtNumero.Mask = "00000";
                txtNumero.SelectionStart = 0;
                txtNumero.SelectionLength = 0;
            }
        }

        private void txtCidade_Enter(object sender, EventArgs e)
        {
            if (txtCidade.ForeColor != System.Drawing.Color.Black)
            {
                txtCidade.ForeColor = System.Drawing.Color.Black;
                txtCidade.SelectionStart = 0;
                txtCidade.SelectionLength = txtCidade.Text.Length;
            }
        }

        private void txtEstado_Enter(object sender, EventArgs e)
        {
            if (txtEstado.ForeColor != System.Drawing.Color.Black)
            {
                txtEstado.ForeColor = System.Drawing.Color.Black;
                txtEstado.SelectionStart = 0;
                txtEstado.SelectionLength = txtEstado.Text.Length;
                //txtEstado.Mask = ">LL";
                //txtEstado.Text = "";
            }
        }

        private void txtBairro_Enter(object sender, EventArgs e)
        {
            if (txtBairro.ForeColor != System.Drawing.Color.Black)
            {
                txtBairro.ForeColor = System.Drawing.Color.Black;
                txtBairro.SelectionStart = 0;
                txtBairro.SelectionLength = txtBairro.Text.Length;
            }
        }

        private void btnAddContatos_Click(object sender, EventArgs e)
        {
            PessoaDAO pessoaDao = new PessoaDAO();
            txtId.Text = pessoaDao.GetNextId().ToString();

            SetCoresCinza();

            dgvContatos.ClearSelection();
            txtNome.Focus();
            
            btnSalvar.Text = "Adicionar";
        }

        

        private void dgvGrupos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //preenchendo a tabela de contatos
            dgvContatos.Rows.Clear();//limpa as linhas da datagridview para poder atualizar com os novos valores

            PessoaDAO pessoaDao = new PessoaDAO();

            List<Pessoa> pessoas = pessoaDao.ListAll(int.Parse(dgvGrupos.Rows[dgvGrupos.CurrentRow.Index].Cells[0].Value.ToString()), idUser);
            
            foreach (Pessoa pessoa in pessoas)
            {
                dgvContatos.Rows.Add(pessoa.IdPessoa, pessoa.NomePessoa);
            }

            if (dgvContatos.Rows.Count > 0)
                dgvContatos_CellClick(null, null);
            else
            {
                btnAddContatos_Click(null, null);
                txtNome.Focus();
            }
                
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            int idGrupo = int.Parse(dgvGrupos.Rows[dgvGrupos.CurrentRow.Index].Cells[0].Value.ToString());
            string nome = txtPesquisa.Text;

            PessoaDAO pessoaDao = new PessoaDAO(); ;
            List<Pessoa> pessoas;

            if (nome == null)
                pessoas = pessoaDao.ListAll(idGrupo, idUser);

            else
                pessoas = pessoaDao.ListByName(nome, idGrupo, idUser);

            dgvContatos.Rows.Clear();

            foreach (Pessoa pessoa in pessoas)
            {
                dgvContatos.Rows.Add(pessoa.IdPessoa, pessoa.NomePessoa);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(btnSalvar.Text == "Adicionar")
            {
                PessoaDAO pessoaDao = new PessoaDAO();
                pessoaDao.AddPessoa(idUser, txtNome.Text, txtLogradouro.Text, int.Parse(txtNumero.Text),
                                  txtBairro.Text, txtCidade.Text, txtEstado.Text, txtAnotacoes.Text);
                dgvGrupos_CellClick(null, null);
            }
            if(btnSalvar.Text == "Salvar")
            {
                PessoaDAO pessoaDao = new PessoaDAO();
                pessoaDao.AtualizarPessoa(int.Parse(txtId.Text), txtNome.Text, txtLogradouro.Text, int.Parse(txtNumero.Text),
                                  txtBairro.Text, txtCidade.Text, txtEstado.Text, txtAnotacoes.Text);
                dgvGrupos_CellClick(null, null);
            }
        }

        private void btnAddNumero_Click(object sender, EventArgs e)
        {
            if(txtTelefone.Text != "")
                cbbTelefones.Items.Add(txtTelefone.Text);

            txtTelefone.Text = "";
        }

        private void dgvContatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PessoaDAO contatos = new PessoaDAO();

            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas = contatos.ListAll(int.Parse(dgvGrupos.Rows[dgvGrupos.CurrentRow.Index].Cells[0].Value.ToString()), idUser);
            
            if (dgvContatos.Rows.Count > 0)
            {

                SetCoresPreto();

                txtId.Text = pessoas[dgvContatos.CurrentRow.Index].IdPessoa.ToString();
                txtNome.Text = pessoas[dgvContatos.CurrentRow.Index].NomePessoa;
                txtLogradouro.Text = pessoas[dgvContatos.CurrentRow.Index].Logradouro;
                txtCidade.Text = pessoas[dgvContatos.CurrentRow.Index].Cidade;
                txtEstado.Text = pessoas[dgvContatos.CurrentRow.Index].Estado;
                txtNumero.Text = pessoas[dgvContatos.CurrentRow.Index].Numero.ToString();
                txtBairro.Text = pessoas[dgvContatos.CurrentRow.Index].Bairro;
                txtAnotacoes.Text = pessoas[dgvContatos.CurrentRow.Index].Anotacoes;

                TelefoneDAO telefoneDao = new TelefoneDAO();

                List<Telefone> telefones = telefoneDao.ListAll(int.Parse(txtId.Text));

                cbbTelefones.Items.Clear();

                foreach(Telefone telefone in telefones)
                {
                    cbbTelefones.Items.Add(telefone.NroTelefone.ToString());
                }

                if (cbbTelefones.Items.Count > 0)
                    cbbTelefones.SelectedIndex = 0;
            }
            else
            {
                btnAddContatos_Click(null, null);
                txtNome.Focus();
            }

            btnSalvar.Text = "Salvar";
        }

        private void txtTelefone_Enter(object sender, EventArgs e)
        {
            //se não houver texto ao entrar na textbox, o cursor é movido para o começo da caixa
            if (txtTelefone.Text == "")
            {
                txtTelefone.SelectionStart = 0;
                txtTelefone.SelectionLength = txtNome.Text.Length;
            }
        }

        private void txtTelefone_Click(object sender, EventArgs e)
        {
            //ao clicar na caixa de texto chama-se o metodo txtTelefone_Enter
            txtTelefone_Enter(sender, e);
        }
        
        private void btnRmvNumero_Click(object sender, EventArgs e)
        {
            if(cbbTelefones.Items.Count>0)
            {
                cbbTelefones.Items.Remove(cbbTelefones.SelectedItem);
                cbbTelefones.SelectedIndex = 0;
            }
        }

        private void btnRmvContatos_Click(object sender, EventArgs e)
        {
            PessoaDAO pessoaDao = new PessoaDAO();

            pessoaDao.RemoverPessoa(int.Parse(txtId.Text));

            dgvGrupos_CellClick(null, null);
        }

        private void txtNumero_Click(object sender, EventArgs e)
        {
            txtNumero_Enter(null, null);
        }

        private void txtEstado_Click(object sender, EventArgs e)
        {
            txtEstado_Enter(null, null);
        }
        
        private void txtNumero_TextChanged(object sender, EventArgs e)
        {
            txtNumero.Mask = "00000";
        }
        
        private void txtEstado_TextChanged(object sender, EventArgs e)
        {
            txtEstado.Mask = ">LL";
        }
    }
}
