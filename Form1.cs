using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace authorization
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            // Авторизация

            string loginUser = loginBox.Text.Trim();
            string passwordUser = passwordBox.Text.Trim();
            string myConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source=|DataDirectory|\Avtorization.accdb;";
            OleDbConnection con = new OleDbConnection(myConStr);
            OleDbCommand myQuery = new OleDbCommand("SELECT * FROM users " +
                "WHERE " +
                "Login = @login AND " +
                "Password = @password", con);

            myQuery.Parameters.Add("@login", OleDbType.VarChar).Value = loginUser;
            myQuery.Parameters.Add("@Password", OleDbType.VarChar).Value = passwordUser;


            OleDbDataAdapter myAdapt = new OleDbDataAdapter();
            myAdapt.SelectCommand = myQuery;
            DataTable myTab = new DataTable();
            myAdapt.Fill(myTab);
            if (myTab.Rows.Count == 1)
            {
                MessageBox.Show("Вы прошли Авторизацию!");
                nameLabel.Text = Convert.ToString(myTab.Rows[0][1]);
                enterButton.Enabled = true; // доступна
            }
            else
            {
                MessageBox.Show("Авторизация не прошла!");
            }
        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            registrationForm form2 = new registrationForm();

            form2.Show();
        }
    }
}
