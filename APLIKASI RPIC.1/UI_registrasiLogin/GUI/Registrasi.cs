using System;
using System.Windows.Forms;
using UI_registrasiLogin.Managers;
using UI_registrasiLogin.Models;

namespace UI_registrasiLogin
{
    public partial class Registrasi : Form
    {
        private UserManager<User> userMgr;

        public Registrasi()
        {
            InitializeComponent();
            userMgr = new UserManager<User>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            string role = Pelanggan.Checked || Admin.Checked ? "Pelanggan" : "Admin";

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                User newUser = new User
                {
                    Username = username,
                    Password = password,
                    Role = role
                };

                if (userMgr.RegisterUser(newUser))
                {
                    MessageBox.Show("User has been registered.");
                }
                else
                {
                    MessageBox.Show("Username already exists. Please choose another username.");
                }
            }
            else
            {
                MessageBox.Show("Please fill all fields.");
            }
            clearInput();
        }

        private void clearInput()
        {
            textBox1.Clear();
            textBox2.Clear();
            Pelanggan.Checked = false;
            Admin.Checked = false;

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login lgn = new Login();
            lgn.Show();
            this.Hide();
        }
    }
}
