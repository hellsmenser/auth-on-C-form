using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace authorization
{
    public partial class registrationForm : Form
    {
        private bool myEmail(TextBox field)
        {
            var mail = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (mail.IsMatch(field.Text))
                return true;
            else
                MessageBox.Show("Email написан не верно");
                return false;
        }
        private bool myNameIs(TextBox field)
        {
            var firstLetterLat = new Regex(@"^[A-Z][a-zA-Z]*$");
            var firstLetterKir = new Regex(@"^[А-Я][а-яА-Я]*$");

            if (firstLetterKir.IsMatch(field.Text) || firstLetterLat.IsMatch(field.Text))
                return true;
            else
                MessageBox.Show("Имя/Фамилия написаны не правильно");
                return false;
        }
        private bool myPasswordIs()
        { // Функция проверки корректности ввода пароля
            bool flagPassword = true; // априори считаем - пароль введен правильно
                                      // тип определит компилятор C#
            var flagNumber = new Regex(@"(?=.*[0-9])");
            var flagUpChar = new Regex(@"(?=.*[A-Z])");
            var flagLowChar = new Regex(@"(?=.*[a-z])");
            var flagSpecSymbl = new Regex(@"(?=.*[@#$&])");
            var flagIncorectChar = new Regex(@"[^0-9a-zA-Z@#$&]");
            // Обработка пароля
            if (passwordBox.Text == String.Empty)
            {
                MessageBox.Show("Заполните поле Пароль");
                return false;
            }
            else
            {
                if (passwordBox.Text.Length < 6)
                {
                    MessageBox.Show("Размер должен быть от 6 символов");
                    flagPassword = false;
                }
                if (!flagNumber.IsMatch(passwordBox.Text))
                {
                    MessageBox.Show("Нет цифр");
                    flagPassword = false;
                }
                if (!flagUpChar.IsMatch(passwordBox.Text))
                {
                    MessageBox.Show("Нет заглавных букв");
                    flagPassword = false;
                }
                if (!flagLowChar.IsMatch(passwordBox.Text))
                {
                    MessageBox.Show("Нет строчных букв");
                    flagPassword = false;
                }
                if (!flagSpecSymbl.IsMatch(passwordBox.Text))
                {
                    MessageBox.Show("Нет специальных символов");
                    flagPassword = false;
                }
                if (flagIncorectChar.IsMatch(passwordBox.Text))
                {
                    MessageBox.Show("Есть недопустимые символы");
                    flagPassword = false;
                }
                return flagPassword;
            }
        }
        public registrationForm()
        {
            InitializeComponent();
        }

        private void OKbutton_Click(object sender, EventArgs e)
        {
            string FN = FNamebox.Text.Trim();
            string LN = LNameBox.Text.Trim();
            string lg = loginBox.Text.Trim();
            string ps = passwordBox.Text.Trim();
            string em = EmailBox.Text.Trim();

            string myConStr = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                @"Data Source=|DataDirectory|\Avtorization.accdb;";
            string myQuery = "INSERT INTO users ([FirstName], [LastName], [Login], [Password], [Email])" +
                " VALUES (@Fn, @Ln, @lg, @ps, @em)";


            OleDbConnection con = new OleDbConnection(myConStr);

            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = myQuery;

            cmd.Parameters.Add("@Fn", OleDbType.VarChar).Value = FN;
            cmd.Parameters.Add("@Ln", OleDbType.VarChar).Value = LN;
            cmd.Parameters.Add("@lg", OleDbType.VarChar).Value = lg;
            cmd.Parameters.Add("@ps", OleDbType.VarChar).Value = ps;
            cmd.Parameters.Add("@em", OleDbType.VarChar).Value = em;

            if (myNameIs(FNamebox) && myNameIs(LNameBox) && myEmail(EmailBox) && myPasswordIs())
            {
                con.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Спасибо за регистрацию");
                }
                else
                    MessageBox.Show("Что-то пошло не так. Регистрация не произошла");


                con.Close();

            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
