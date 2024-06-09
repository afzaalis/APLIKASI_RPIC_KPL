using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI_registrasiLogin.Managers;
using UI_registrasiLogin.Models;

namespace UI_registrasiLogin.GUI
{
    public partial class LoginP : Form
    {
        private UserManager<User> userMgr;
        public LoginP()
        {
            InitializeComponent();
            userMgr = new UserManager<User>();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (userMgr.AuthenticateUser(username, password))
            {
                MessageBox.Show("Login as Pelanggan successful.");
                PelangganReserve pR = new PelangganReserve();

                this.Close();
                pR.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }

        }
    }
}
