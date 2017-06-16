using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Artigos
{
    public partial class Cadastrar : Form
    {
        private Conexao conn;
        private Conexao calzone;
        public static SqlConnection ConnectOpen;
        public bool logado = false;
        public int id = 0;
        public bool vazio()
        {
            if (this.txtSenha.Text == "" || this.txtUsuario.Text == "")
            {
                return true;
            }
            else return false;
        }
        public Cadastrar()
        {
            InitializeComponent();
            conn = new Conexao();
            ConnectOpen = conn.ConnectToDatabase();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (btnCadastrar.Text == "Alterar")
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" update ideias set ");
                sql.Append(" descricao = @descricao, ");
                sql.Append(" titulo = @titulo ");
                //Não esqueçam de dar um espaço no final 
                sql.Append(" where id = @id");

                SqlCommand command = null;


                command = new SqlCommand(sql.ToString(), ConnectOpen);
                command.Parameters.Add(new SqlParameter("@descricao", txtSenha.Text));
                command.Parameters.Add(new SqlParameter("@titulo", txtUsuario.Text));
                command.Parameters.Add(new SqlParameter("@id", id));
                command.ExecuteNonQuery();

                MessageBox.Show("Alterado com sucesso!");
                LimparTela();
            }
            else if (!vazio())
            {
                //incluir o using System.Text
                StringBuilder sql = new StringBuilder();
                sql.Append("Insert into ideias (titulo, descricao) ");
                sql.Append("Values (@titulo, @descricao)");
                SqlCommand command = null;

                try
                {
                    command = new SqlCommand(sql.ToString(), ConnectOpen);
                    command.Parameters.Add(new SqlParameter("@titulo", txtUsuario.Text));
                    command.Parameters.Add(new SqlParameter("@descricao", txtSenha.Text));
                    command.ExecuteNonQuery();

                    MessageBox.Show("Ideia Registrada!");
                    LimparTela();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao cadastrar" + ex);
                    throw;
                }
            }

            else MessageBox.Show("Preencha todos os campos solicitados!");
        }


        private void LimparTela()
        {
            btnCadastrar.Text = "Salvar";
            btnExcluir.Visible = false;
            txtSenha.Text = "";
            txtUsuario.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //calzone = new Conexao();
            //ConnectOpen = calzone.ConnectToDatabase();

            var listarUsu = new Listar();
            listarUsu.ShowDialog();

            //Verificar se foi selecionado algum item
            if (listarUsu.titulo == "")
                return;

            var conn=ConnectOpen;
            //Buscar usuário selecionado
            string sql = "Select * from ideias where titulo = '" + listarUsu.titulo + "'";


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);

            //Linha 0, coluna 0
            txtUsuario.Text = dt.Rows[0][0].ToString();

            //Linha 0, coluna 1
            txtSenha.Text = dt.Rows[0][1].ToString();

            id = listarUsu.id;

            //Trocar o text do cadastra para alterar
            btnCadastrar.Text = "Alterar";

            //Alterar a visualização do excluir
            btnExcluir.Visible = true;

        }

        private void Cadastrar_Load(object sender, EventArgs e)
        {
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var conn = ConnectOpen;

            //Confirmar exclusão
            DialogResult result = MessageBox.Show("Deseja REALMENTE excluir?", "Delete",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            //Caso o usuário dê ok, a exclusão prossegue
            if (!result.Equals(DialogResult.OK))
                return; //caso cancele, o código abaixo não será excutado.

            //Buscar usuário selecionado
            string sql = "Delete from ideias where id = @id";

            SqlCommand command = null;
            command = new SqlCommand(sql.ToString(), ConnectOpen);
            command.Parameters.Add(new SqlParameter("@id", id));
            command.ExecuteNonQuery();
            LimparTela();
            MessageBox.Show("Excluído com sucesso!");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
