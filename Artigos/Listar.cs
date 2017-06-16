using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Artigos
{
    public partial class Listar : Form
    {
         public string titulo = "";
        Conexao conn;
        public SqlConnection ConnectOpen;
        public int id = 0;
      

        public Listar()
        {
            InitializeComponent();
            conn = new Conexao();
            ConnectOpen = conn.ConnectToDatabase();
        }

        private void ListarUsuario_Load(object sender, EventArgs e)
        {
            

            var conn =ConnectOpen;
            //Buscar todos usuários cadastrados
            string sql = "Select * from ideias order by id";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            
            if(dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //Recuperar a linha selecionadas.
             titulo = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            id = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            //Fechar a tela
            Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
