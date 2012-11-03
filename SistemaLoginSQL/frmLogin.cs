using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemaLoginSQL
{
    public partial class frmLogin : Form
    {
        //minha conexao com o banco de dados
        SqlConnection conexao = new SqlConnection();
        private string strSQL = string.Empty;
        //string de conexao
        private string strConn = "Data Source=(local); Initial Catalog=Controle; Integrated Security=SSPI";

        //variavel para controlar se esta logado ou nao, a principio ela começara sem logar.
        public bool logado = false;
        
        public frmLogin()
        {
            InitializeComponent();
        }

        private void logar()
        {
            conexao = new SqlConnection(strConn);
            string login, senha;

            try
            {
                login = textBox_Login.Text;
                senha = textBox_Senha.Text;
                strSQL = "SELECT COUNT(idUsuario) FROM Usuarios WHERE Login = @L AND Senha = @S";
                SqlCommand comando = new SqlCommand(strSQL, conexao);
                comando.Parameters.Add("@L", SqlDbType.VarChar).Value = login;
                comando.Parameters.Add("@S", SqlDbType.VarChar).Value = senha;
                //abrindo conexão
                conexao.Open();

                int v = (int)comando.ExecuteScalar();
                if (v > 0)
                {
                    logado = true;
                    this.Dispose();
                }
                else
                {
                    logado = false;
                    MessageBox.Show("Erro");
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        private void button_Entrar_Click(object sender, EventArgs e)
        {
            if (textBox_Login.Text == "" || textBox_Senha.Text == "")
            {
                if (textBox_Login.Text == "" && textBox_Senha.Text == "")
                {
                    errorProvider1.SetError(textBox_Login, "*Campo obrigatório, por favor preencha");
                    errorProvider1.SetError(textBox_Senha, "*Campo obrigatório, por favor preencha");

                }
                else
                {
                    errorProvider1.SetError(textBox_Login, "");
                    errorProvider1.SetError(textBox_Senha, "");
                }
                if (textBox_Senha.Text == "")
                     errorProvider1.SetError(textBox_Senha, "*Campo obrigatório, por favor preencha");
                 else
                     errorProvider1.SetError(textBox_Senha, "");
                if (textBox_Login.Text == "")
                    errorProvider1.SetError(textBox_Login, "*Campo obrigatório, por favor preencha");
                else
                    errorProvider1.SetError(textBox_Login, "");
            }
            else
            {
                errorProvider1.SetError(textBox_Senha, "");
                logar();
            }
        }
    }
}
